using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using AutoMapper;
using W2W.ModelKBT.Entities;
using WayWealth.Infrastructure.Auth;
using WayWealth.ViewModels.Sign;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using System.Text;
using Common.Web;
using WayWealth.Helpers;
using System.Configuration;
using JWT;
using System.Text.RegularExpressions;
using Google.Authenticator;

namespace WayWealth.Controllers
{
    public class SignController : BaseController
    {
        [HttpGet]
        public ActionResult In(string returnUrl)
        {
            if (User != null)
                return Redirect(Url.Action("index", "home", new { area = "lk" }));

            return View();
        }

        [HttpGet]
        public ActionResult TwoFactorAuthenticate()
        {
            if (User != null)
                return Redirect(Url.Action("index", "home", new { area = "lk" }));

            if (Session["GoogleAuthSuccessPartnerId"] == null)
                return RedirectToAction("in", "sign");

            uint id_user = (uint) Session["GoogleAuthSuccessPartnerId"];
            var retUser = DataService.GetPartner(id_user);
            if (retUser == null || !(retUser.GoogleAuthEnabled ?? false))
                return RedirectToAction("in", "sign");

            return View();
        }

        [HttpPost]
        public ActionResult TwoFactorAuthenticate(TwoFactorAuthenticateView model, string returnUrl)
        {
            if (User != null)
                return Redirect(Url.Action("index", "home", new { area = "lk" }));

            if (Session["GoogleAuthSuccessPartnerId"] == null)
                return RedirectToAction("in", "sign");

            uint id_user = (uint)Session["GoogleAuthSuccessPartnerId"];
            
            var retUser = DataService.GetPartner(id_user);
            if (retUser == null || !(retUser.GoogleAuthEnabled ?? false))
                return RedirectToAction("in", "sign");

            bool IsPersistent = (bool)Session["GoogleAuthIsPersistent"];

            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
            bool isValid = TwoFacAuth.ValidateTwoFactorPIN(retUser.GoogleAuthKey, model.CodeDigit);

            if (ModelState.IsValid)
            {
                    try
                    {
                    if (isValid && AuthenticationService.LoginFrom2Auth(retUser, IsPersistent))
                        {
                        Session["GoogleIsValidTwoFactorAuthentication"] = true;
                        Session.Remove("GoogleAuthSuccessPartnerId");
                        Session.Remove("GoogleAuthIsPersistent");

                        return Redirect(returnUrl ?? Url.Action("index", "home", new { area = "lk" }));
                    } else
                        {
                        throw new Exception(Resources.Resource.GoogleAuthIncorrectCode);
                        }
                    }
                    catch (Exception exc)
                    {
                        ModelState.AddModelError(string.Empty, exc.Message);
                    }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult In(InView model, string returnUrl)
        {
            if (User != null)
                return Redirect(Url.Action("index", "home", new { area = "lk" }));

            if (!CheckCaptcha())
                return View(model);

            if (ModelState.IsValid)
            {
                try
                {
                    var retUser = AuthenticationService.CheckLogin(model.Login, model.Password);

                    if (retUser != null)
                    {
                        
                        if ((retUser.GoogleAuthEnabled ?? false) && (Session["GoogleIsValidTwoFactorAuthentication"] == null || !(bool)Session["GoogleIsValidTwoFactorAuthentication"]) )
                        {
                            Session["GoogleAuthSuccessPartnerId"] = retUser.id_object;
                            Session["GoogleAuthIsPersistent"] = model.IsPersistent;
                            return RedirectToAction("TwoFactorAuthenticate", "sign");
                        }
                        
                        AuthenticationService.Login(model.Login, model.Password, model.IsPersistent);

                        return Redirect(returnUrl ?? Url.Action("index", "home", new { area = "lk" }));


                    }
                    else
                    {
                        throw new Exception(Resources.Resource.ErrWrongLoginOrPassword);
                    }
                }
                catch (Exception exc)
                {
                    ModelState.AddModelError(string.Empty, exc.Message);
                }
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult Up(string inviter)
        {

            // Если не указан в ссылке Inviter тогда пробуем найти его в куки
            if (string.IsNullOrWhiteSpace(inviter))
            {
                if (this.User != null)
                    inviter = this.User.PersonalId.ToString();
                else
                {
                    var cookie = Request.Cookies["Inviter"];
                    if (cookie != null)
                        inviter = cookie.Value;
                }
            }

            // Если Inviter Обнаружен
            if (!string.IsNullOrWhiteSpace(inviter))
            {
                var inv = this.DataService.GetPartner(inviter);
                if (inv == null)
                    inviter = null;
                else
                {
                    TempData["inviterName"] = inv.ObjectName;
                    var cookie = new HttpCookie("Inviter");
                    cookie.Value = inviter;
                    cookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                }
            }
            TempData["inviter"] = inviter;


            return View();
        }
        [HttpPost]
        public ActionResult Up(UpView model, string inviter)
        {

            if (!CheckCaptcha())
                return View(model);

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Password != model.ConfirmPassword)
                        throw new Exception(Resources.Resource.ErrPasswordsDoNotMatch);

                    if (this.DataService.GetPartnerByEmail(model.Email) != null)
                        throw new Exception(Resources.Resource.ErrEmailIsAlreadyRegistred);

                    if (!string.IsNullOrWhiteSpace(model.Login) &&
                        this.DataService.GetPartner(model.Login.Trim()) != null)
                        throw new Exception(Resources.Resource.ErrLoginIsAlreadyRegistred);

                    uint inviterId = 0;
                    if (!string.IsNullOrWhiteSpace(inviter))
                    {
                        model.InviterTxt = inviter;
                    }

                    if (!string.IsNullOrWhiteSpace(model.InviterTxt))
                    {
                        var inv = this.DataService.GetPartner(model.InviterTxt);
                        if (inv == null)
                            throw new Exception(Resources.Resource.ErrInviterNotFound);
                        inviterId = inv.id_object;
                    }

                    var login = model.Login;
                    //if (string.IsNullOrWhiteSpace(login))
                    //    login = model.Email;

                   uint id_client = DataService.CreatePartner(
                        inviterId: inviterId,
                        firstName: model.FirstName,
                        lastName: model.LastName,
                        middleName: model.MiddleName,
                        email: model.Email,
                        password: model.Password,
                        login: login);


                    var partner = this.DataService.GetPartner(id_client);

                    // формирование ссылки
                    string link = CreateUrl2(model.Email);

                    this.MailService.SendMessage(
                      to: model.Email,
                      subject: Resources.Resource.SignUpText,
                      body: string.Format(Resources.Resource.LetterSignUp, 
                      link, 
                      link, 
                      model.Login,
                      $"{model.FirstName} {model.MiddleName} {model.LastName}",
                      model.Password,
                      model.InviterTxt));

                    //email message 
                    //StringBuilder message = new StringBuilder();
                    //var culture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                    //if (culture == "ru-RU")
                    //{
                    //    message.Append("<h3>Здравствуйте!</h3>");
                    //    message.Append($"<p>Для завершения регистрации перейдите по ссылке <a href=\"{link}\">{link}</a></p>");
                    //    message.Append("<p>Ссылка действительна в течении 24 часов.</p>");
                    //}
                    //else
                    //{
                    //    message.Append("<h3>Hello!</h3>");
                    //    message.Append($"<p>To complete the registration, click on the link <a href=\"{link}\">{link}</a></p>");
                    //    message.Append("<p>The link is valid for 24 hours..</p>");
                    //}

                    //this.MailService.SendMessage(model.Email, Resources.Resource.SignUpText, message.ToString());

                    // отправка письма
                    Session.AddNotification(new Notification(true, Resources.Resource.SignUpText,
                       Resources.Resource.SignUpInformationSent));

                    //AuthenticationService.Login(login, model.Password, false);
                    return RedirectToAction("index", "home", new { area="lk" });
                }
                catch (Exception exc)
                {
                    //Session.AddNotification(new Notification(false, exc.Message));
                    ModelState.AddModelError(string.Empty, exc.Message);
                }
            }
            return View(model);
        }



        [HttpGet]
        public ActionResult Restore()
        {
            if (User != null)
                return Redirect(Url.Action("index", "home", new { area = "lk" }));

            return View();
        }
        [HttpPost]
        public ActionResult Restore(RestoreView model)
        {
            if (User != null && (Session["GoogleIsValidTwoFactorAuthentication"] != null && (bool)Session["GoogleIsValidTwoFactorAuthentication"]))
                return Redirect(Url.Action("index", "home", new { area = "lk" }));

            if (!CheckCaptcha())
                return View(model);

            if (ModelState.IsValid)
            {
                try
                {
                    var identity = model.Login.Trim();
                    var partner = this.DataService.GetPartner(identity);
                    if (partner == null) 
                        partner = this.DataService.GetPartnerByEmail(identity);

                    if (partner == null)
                        throw new Exception(Resources.Resource.ErrUserIsNotFound);

                    // сгенерировать пароль
                    var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$";
                    var newpass = RandomString(6);
                    while (!Regex.IsMatch(newpass, pattern))
                        newpass = RandomString(6);

                    string link = CreateUrl(partner.Email, newpass);

                    this.MailService.SendMessage(
                       to: partner.Email,
                       subject: Resources.Resource.AccessRecovery,
                       body: string.Format(Resources.Resource.LetterResetAccess, link, link, partner.Login, newpass));

                    ////todo: email message 
                    //StringBuilder message = new StringBuilder();
                    //var culture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                    //if(culture == "ru-RU")
                    //{
                    //    message.Append("<h3>Здравствуйте!</h3>");
                    //    message.Append("<p>Вы запросили восстановление доступа.</p>");
                    //    message.Append($"<p>Ваш логин: {partner.Login}. Ваш новый пароль: {newpass}</p>");
                    //    message.Append($"<p>Для сброса предыдущего пароля перейдите по ссылке <a href=\"{link}\">{link}</a></p>");
                    //    message.Append("<p>Ссылка действительна в течении 24 часов.</p>");
                    //    message.Append("<p>Если вы не хотите сбрасывать пароль то просто проигнорируйте это сообщение.</p>");
                    //}
                    //else
                    //{
                    //    message.Append("<h3>Hello!</h3>");
                    //    message.Append("<p>You have requested access recovery.</p>");
                    //    message.Append($"<p>Your login:{partner.Login}. Your new password: {newpass}</p>");
                    //    message.Append($"<p>To reset the previous password, click on the link <a href=\"{link}\">{link}</a></p>");
                    //    message.Append("<p>The link is valid for 24 hours.</p>");
                    //    message.Append("<p>If you do not want to reset your password, then just ignore this message.</p>");
                    //}

                    //this.MailService.SendMessage(partner.Email, Resources.Resource.AccessRecovery, message.ToString());

                    Session.AddNotification(new Notification(true, Resources.Resource.AccessRecovery,
                        Resources.Resource.AccessRecoveryInformationSent));
                    return RedirectToAction("in");
                }
                catch (Exception exc)
                {
                    //Session.AddNotification(new Notification(false, Resources.Resource.AccessRecovery, exc.Message));
                    ModelState.AddModelError(string.Empty, exc.Message);
                }
            }
            return View(model);
        }


        public ActionResult Confirm(string t)
        {
            if (User != null)
                return Redirect(Url.Action("index", "home", new { area = "lk" }));

            try
            {
                JwtHelper jwt = new JwtHelper();
                var secret = ConfigurationManager.AppSettings["jwt:secret"];
                var payload = jwt.Decode<Dictionary<string, object>>(t, secret);

                var email = payload["email"].ToString();
                var partner = DataService.GetPartnerByEmail(email);
                if (partner == null)
                    throw new Exception(Resources.Resource.ErrUserIsNotFound);

                // сохранение в базе
                DataService.UpdatePartnerEmailConfirmation(id_client: partner.id_object, emailConfirmation: true);

                Session.AddNotification(new Notification(true, Resources.Resource.ConfirmationEmail, 
                    Resources.Resource.EmailIsConfirmed));

                var login = string.IsNullOrWhiteSpace(partner.Login) ? partner.Email : partner.Login;

                this.AuthenticationService.Login(login, partner.Password, false);
                return RedirectToAction("index", "home", new { area = "lk" });
            }
            catch (TokenExpiredException)
            {
                Session.AddNotification(new Notification(false, Resources.Resource.ConfirmationEmail,
                    Resources.Resource.ErrLinkExpired));
                return RedirectToAction("in", "sign");
            }
            catch (SignatureVerificationException)
            {
                Session.AddNotification(new Notification(false, Resources.Resource.ConfirmationEmail,
                    Resources.Resource.ErrLinkIsDamaged));
                return RedirectToAction("in", "sign");
            }
            catch (Exception exc)
            {
                Session.AddNotification(new Notification(false, Resources.Resource.ConfirmationEmail,
                    exc.Message));
                return RedirectToAction("in", "sign");
            }
        }
        public ActionResult Reset(string t)
        {
            if (User != null)
                return Redirect(Url.Action("index", "home", new { area = "lk" }));

            try
            {
                JwtHelper jwt = new JwtHelper();
                var secret = ConfigurationManager.AppSettings["jwt:secret"];
                var payload = jwt.Decode<Dictionary<string, object>>(t, secret);

                var email = payload["email"].ToString();
                var pass = payload["password"].ToString();
                var partner = DataService.GetPartnerByEmail(email);
                if (partner == null)
                    throw new Exception(Resources.Resource.ErrUserIsNotFound);

                // сохранение в базе
                DataService.UpdatePartnerPassword(id_client: partner.id_object, password: pass);

                Session.AddNotification(new Notification(true, Resources.Resource.AccessRecovery, Resources.Resource.PasswordСhanged));
                return RedirectToAction("index", "home", new { area = "lk" });
            }
            catch (TokenExpiredException)
            {
                Session.AddNotification(new Notification(false, Resources.Resource.AccessRecovery,
                    Resources.Resource.ErrLinkExpired));
                return RedirectToAction("in", "sign");
            }
            catch (SignatureVerificationException)
            {
                Session.AddNotification(new Notification(false, Resources.Resource.AccessRecovery,
                    Resources.Resource.ErrLinkIsDamaged));
                return RedirectToAction("in", "sign");
            }
            catch (Exception exc)
            {
                Session.AddNotification(new Notification(false, Resources.Resource.AccessRecovery,
                    exc.Message));
                return RedirectToAction("in", "sign");
            }
        }


        public ActionResult Out()
        {
            AuthenticationService.LogOut();
            Session.Remove("GoogleAuthSuccessPartnerId");
            Session.Remove("GoogleIsValidTwoFactorAuthentication");
            Session.Remove("GoogleUserUniqueKey");
            Session.Remove("GoogleAuthIsPersistent");
            return Redirect(Request.UrlReferrer.ToString());
        }

        bool CheckCaptcha()
        {
            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();
            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError(string.Empty, Resources.Resource.CaptchaIsEmpty);
                return false;
            }
            RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            if (recaptchaResult != RecaptchaVerificationResult.Success)
            {
                ModelState.AddModelError(string.Empty, Resources.Resource.CaptchaIsIncorrect);
                return false;
            }
            return true;
        }
    }
}
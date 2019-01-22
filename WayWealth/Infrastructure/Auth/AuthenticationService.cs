using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Web.Auth;
using Ninject;
using AutoMapper;
using System.Web.Security;
using Newtonsoft.Json;
using W2W.ModelKBT;
using WayWealth.Infrastructure.Mail;
using WayWealth.Areas.lk.Helpers;


namespace WayWealth.Infrastructure.Auth
{
    public class AuthenticationService 
    {
        [Inject]
        public  IDataService DataService { get; set; }


        public bool Login(string username, string password, bool isPersistent)
        {
            var retUser = DataService.Login(username, password);

            if (retUser != null)
            {
                if (!(retUser.IsEmailConfirmed ?? false))
                    throw new Exception(Resources.Resource.ConfirmYourEmail);

                var user = Mapper.Map<UserSerializeModel>(retUser);
                CreateCookie(user, isPersistent);

                var mail = new MailService();
                DateTime dateTime = DateTime.UtcNow.Date;

                mail.SendMessage(
                       to: retUser.Email,
                       subject: Resources.Resource.LoginNotify,
                       body: string.Format(Resources.Resource.LoginNotifyLetter, retUser.Login, DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"), PagingHelpers.UserIP(), PagingHelpers.UserBrowser(), PagingHelpers.UserCountry()));

                return true;
            }
            return false;
        }

        public bool LoginFrom2Auth(W2W.ModelKBT.Entities.Partner retUser, bool isPersistent)
        {
            if (retUser != null)
            {
                var user = Mapper.Map<UserSerializeModel>(retUser);
                CreateCookie(user, isPersistent);

                var mail = new MailService();
                DateTime dateTime = DateTime.UtcNow.Date;

                mail.SendMessage(
                       to: retUser.Email,
                       subject: Resources.Resource.LoginNotify,
                       body: string.Format(Resources.Resource.LoginNotifyLetter, retUser.Login, DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"), PagingHelpers.UserIP(), PagingHelpers.UserBrowser(), PagingHelpers.UserCountry()));

                return true;
            }
            return false;
        }

        public W2W.ModelKBT.Entities.Partner CheckLogin(string username, string password)
        {
            var retUser = DataService.Login(username, password);

            if (retUser != null)
            { 
                if (!(retUser.IsEmailConfirmed ?? false))
                    return null;

                return retUser;
            }
            return null;
        }

        public bool Login(uint id_user, bool isPersistent)
        {
            throw new NotImplementedException();
        }

        private void CreateCookie(UserSerializeModel user, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                user.id_object.ToString(),
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                isPersistent,
                JsonConvert.SerializeObject(user),
                FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            HttpContext.Current.Response.Cookies.Set(authCookie);
        }

        public void UpdateCookies(uint userId)
        {
            var retUser = this.DataService.GetPartner(userId);
            var user = Mapper.Map<UserSerializeModel>(retUser);

            var authCookie = FormsAuthentication.GetAuthCookie(user.id_object.ToString(), true);
            var ticket = FormsAuthentication.Decrypt(authCookie.Value);


            var newticket = new FormsAuthenticationTicket(ticket.Version,
                ticket.Name,
                ticket.IssueDate,
                ticket.Expiration,
                true,
                JsonConvert.SerializeObject(user),
                ticket.CookiePath);

            // Encrypt the ticket and store it in the cookie
            authCookie.Value = FormsAuthentication.Encrypt(newticket);
            authCookie.Expires = newticket.Expiration.AddHours(24);
            HttpContext.Current.Response.Cookies.Set(authCookie);
        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }
    }
}
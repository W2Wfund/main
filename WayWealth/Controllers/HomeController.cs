using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using W2W.ModelKBT.Entities;
using WayWealth.Areas.lk.ViewModels;
using WayWealth.Areas.lk.ViewModels.Home;

using KBTech.Lib;
using KBTech.Lib.Client;
using KBTech.Lib.Client.Payment;
using KBTech.Lib.Model;
using KBTech.Lib.Query;
using KBTech.Lib.Query.Enums;
using AutoMapper;
using WayWealth.Infrastructure.Auth;
using System.Net;
using System.IO;
using WayWealth.Helpers;

namespace WayWealth.Controllers
{
    public class HomeController : BaseController
    {
        //public ActionResult Index() => View();
        public ActionResult Index() {
            ViewBag.Items = DataService.GetInvestPrograms();
            return View();
        }
        public ActionResult About()
        {
            if (Request.Cookies["lang"] != null && Request.Cookies["lang"].Value == "en")
                return View("AboutEn");
            return View();
        }
        public ActionResult Investments()
        {
            if (Request.Cookies["lang"] != null && Request.Cookies["lang"].Value == "en")
                return View("InvestmentsEn");
            return View();
        }
        public ActionResult Partnership()
        {
            if (Request.Cookies["lang"] != null && Request.Cookies["lang"].Value == "en")
                return View("PartnershipEn");
            return View();
        }
        public ActionResult Faq()
        {
            if (Request.Cookies["lang"] != null && Request.Cookies["lang"].Value == "en")
                return View("FaqEn");
            return View();
        }
        public ActionResult Disabled()
        {
            if (Request.Cookies["lang"] != null && Request.Cookies["lang"].Value == "en")
                return View("DisabledEn");
            return View();
        }

        public ActionResult News(int? id, int page = 1)
        {
            var news = DataService.GetNews();
            ViewBag.Items = GetPage<Article>(news, page, 10);
            ViewBag.PageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = 10,
                TotalItems = news.Count()
            };
            if (id != null)
            {
                var Article = DataService.GetArticle((int)id);
               
                if (Article != null)
                {
                    ViewBag.Article = Article;
                    return View("NewsOpen");
                }
            }
            
            return View();
        }
        public ActionResult Reviews(int page = 1)
        {
            var reviews = DataService.GetReviews();
            ViewBag.Items = GetPage<Partner>(reviews, page, 10);
            ViewBag.PageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = 10,
                TotalItems = reviews.Count()
            };

            return View();
        }
        
        [ChildActionOnly]
        public PartialViewResult UserInfo()
        {
            if (User == null)
                return PartialView("_UserInfoNotAuth");

            return PartialView("_UserInfoAuth", User);
        }

        [ChildActionOnly]
        public PartialViewResult UserBalances()
        {
            if (User == null)
                return null;

            var partner = this.DataService.GetPartner(this.User.id_object);
            if (partner == null)
                return null;


            if (((partner.BalanceInner ?? 0) != User.BalanceInner) ||
                ((partner.BalanceInvestments ?? 0) != User.BalanceInvestments) ||
                (partner.BalancePercents != User.BalancePercents) ||
                ((partner.BalanceRewards ?? 0) != User.BalanceRewards))
            {
                this.AuthenticationService.UpdateCookies(User.id_object);
                var user = base.DataService.GetPartner(User.id_object);
                var usm = Mapper.Map<UserSerializeModel>(user);
                HttpContext.User = new CustomPrincipal(usm);
            }

            return PartialView("_UserBalances", User);
        }
        
        public PartialViewResult Calc(string program)
        {
            ViewBag.Program = program;
            ViewBag.IsAllowCreateNewPlace = base.IsAllowCreateNewPlace();
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _News() => PartialView(DataService.GetNews());

        [ChildActionOnly]
        public PartialViewResult _Reviews() => PartialView(DataService.GetReviews());

        public ActionResult ChangeCulture(string lang)
        {
            if(Request.UrlReferrer == null)
            {
                return Redirect(Url.Action("index"));
            }

            // Список культур
            var cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            // Сохраняем выбранную культуру в куки
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                DateTime nu = DateTime.Now;
                cookie.Expires = nu.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.AbsolutePath ?? Url.Action("index"));
        }


        //public ActionResult Test()
        //{
        //    using (var client = new WebDataClient())
        //    {
        //        var query = new QueryItem();
        //        query.AddType("Платеж", Level.All);
        //        query.AddProperty("СтатьяДвиженияДенежныхСредств");
        //        query.AddProperty("СсылкаНаДоговор");
        //        query.AddProperty("СсылкаНаДоговор/Название");
        //        query.AddProperty("СуммаПлатежа");
        //        query.AddProperty("СсылкаНаКонтрагента");
        //        query.AddProperty("СсылкаНаКонтрагента/Название");
        //        query.AddConditionItem(KBTech.Lib.Query.Enums.Union.None, "СтатьяДвиженияДенежныхСредств",
        //            KBTech.Lib.Query.Enums.Operator.Equal, "Открытие накопит программы Camulative");

        //        var items = client.Search(query).ResultItems();
        //        foreach (var item in items)
        //        {
        //            client.SetObjectValue(
        //                item.Value<uint>("id_object"),
        //                "СтатьяДвиженияДенежныхСредств",
        //                $"Открытие накопит программы");

        //            //client.SetObjectValue(
        //            //    item.Value<uint>("id_object"),
        //            //    "Название",
        //            ////$"Вознаграждение со структуры за {item.Value<string>("СсылкаНаДоговор/Название")}");
        //            //$"Перевод партнеру");
        //        }
        //    }
        //    return new HttpStatusCodeResult(200);
        //}

        public ActionResult Img(string id)
        {
            try
            {
                string dir = Server.MapPath("~/Content/cache");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var path = Path.Combine(dir, id);
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.WriteAllBytes(path, DataService.ReadFile(id));
                }
                string mime = (new MimeHelper()).GetMimeType(Path.GetExtension(path));
                return base.File(path, mime);
            }
            catch (Exception exc)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }
    }
}
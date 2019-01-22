using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using Newtonsoft.Json;
using W2W.ModelKBT.Entities;
using WayWealth.App_Start;
using WayWealth.Areas.lk.ViewModels.Home;
using WayWealth.Infrastructure.Auth;
using WayWealth.Jobs;

namespace WayWealth
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Partner, UserSerializeModel>()
                    .ForMember(x => x.BalanceInner, a => a.MapFrom(b => b.BalanceInner ?? 0))
                    .ForMember(x => x.BalanceInvestments, a => a.MapFrom(b => b.BalanceInvestments ?? 0))
                    .ForMember(x => x.BalancePercents, a => a.MapFrom(b => b.BalancePercents ?? 0))
                    .ForMember(x => x.BalanceRewards, a => a.MapFrom(b => b.BalanceRewards ?? 0))
                    .ForMember(x => x.PersonalId, a => a.MapFrom(b => b.PersonalId ?? 0));

                cfg.CreateMap<Partner, SettingsView>();

                cfg.CreateMap<MarketingPlace, MarketingPlaceView>();
                cfg.CreateMap<Partner, MarketingPlaceView>()
                    .ForMember(x => x.PartnerId, a => a.MapFrom(b => b.id_object))
                    .ForMember(x => x.PartnerLastName, a => a.MapFrom(b => b.LastName))
                    .ForMember(x => x.PartnerFirstName, a => a.MapFrom(b => b.FirstName))
                    .ForMember(x => x.PartnerPersonalId, a => a.MapFrom(b => b.PersonalId ?? 0))
                    .ForMember(x => x.PartnerLogin, a => a.MapFrom(b => b.Login))
                    .ForMember(x => x.PartnerBalanceInvestments, a => a.MapFrom(b => b.BalanceInvestments ?? 0m))
                    .ForMember(x => x.PartnerInviterId, a => a.MapFrom(b => b.InviterId));
            });

            TaskSheduler.Start();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var model = JsonConvert.DeserializeObject<UserSerializeModel>(authTicket.UserData);
                if ( model != null && model.id_object > 0 )
                    HttpContext.Current.User = new CustomPrincipal(model);
                else
                    FormsAuthentication.SignOut();
            }
        }


        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            


            //var culture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;

            //CultureInfo ci = new CultureInfo("de-DE");
            //Thread.CurrentThread.CurrentUICulture = ci;
            //Thread.CurrentThread.CurrentCulture = ci;


            string cultureName = null;
            // Получаем куки из контекста, которые могут содержать установленную культуру
            HttpCookie cultureCookie = HttpContext.Current.Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = "ru";

            // Список культур
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(cultureName))
            {
                cultureName = "ru";
            }


            //filterContext.Controller.ViewBag.CurrentCulture = cl
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
    }
}

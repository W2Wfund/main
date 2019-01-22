using System.Web.Mvc;

namespace WayWealth.Areas.lk
{
    public class lkAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "lk";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "lk_default_mini",
                "lk/{action}/{id}",
                new { action = "index", controller = "home", id = UrlParameter.Optional },
                namespaces: new[] { "WayWealth.Areas.lk.Controllers" }
            );

            context.MapRoute(
                "lk_default",
                "lk/{controller}/{action}/{id}",
                new { action = "index", controller = "home", id = UrlParameter.Optional },
                namespaces: new[] { "WayWealth.Areas.lk.Controllers" }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WayWealth
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "img",
               url: "img/{id}",
               defaults: new { controller = "home", action = "img" },
               namespaces: new[] { "WayWealth.Controllers" }
           );



            routes.MapRoute(
                name: "reg",
                url: "reg/{inviter}",
                defaults: new {lang = "ru", controller = "sign", action = "up"},
                constraints: new {lang = @"[a-z]{2}"},
                namespaces: new[] {"WayWealth.Controllers"}
            );

            routes.MapRoute(
               name: "default_mini",
               url: "{action}",
               defaults: new { controller = "home", action = "index" },
               namespaces: new[] { "WayWealth.Controllers" }
           );

            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional },
                namespaces: new[] { "WayWealth.Controllers" }
            );
        }
    }
}

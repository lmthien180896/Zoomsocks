using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Zoomsocks.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional },
                namespaces: new[] { "Zoomsocks.WebUI.Controllers" }
            );

            routes.MapRoute(
                name: "About Us",
                url: "about-us",
                defaults: new { controller = "Home", action = "AboutUs", id = UrlParameter.Optional },
                namespaces: new[] { "Zoomsocks.WebUI.Controllers" }
            );
        }
    }
}

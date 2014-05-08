using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BillsApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Add new configured route
            routes.MapRoute(
                name: "BillByName",
                url: "Bill/{billname}",
                defaults: new { controller = "Bill", action = "DetailsByName", billname = UrlParameter.Optional },
                constraints: new { billname = "[A-Za-z]+" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

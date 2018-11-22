using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Survey_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: null,
                url: "Home/Index/Page{page}",
                defaults: new { Controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: null,
                url: "Home/AuthorSurveys/Page{page}/Author{authorID}",
                defaults: new { Controller = "Home", action = "AuthorSurveys" }
            );
            routes.MapRoute(
                name: null,
                url: "Home/FilledSurveys/Page{page}",
                defaults: new { Controller = "Home", action = "FilledSurveys" }
            );
            routes.MapRoute(
                name: null,
                url: "Home/MySurveys/Page{page}",
                defaults: new { Controller = "Home", action = "MySurveys" }
            );

            routes.MapRoute(
                name: null,
                url: "Account/Index/Page{page}",
                defaults: new { Controller = "Account", action = "Index" }
            );
            routes.MapRoute(
                name: null,
                url: "Account/Followed/Page{page}",
                defaults: new { Controller = "Account", action = "Followed" }
            );
            routes.MapRoute(
                name: null,
                url: "Account/Following/Page{page}",
                defaults: new { Controller = "Account", action = "Following" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Authentication", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}

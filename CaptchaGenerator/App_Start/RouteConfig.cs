using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CaptchaGenerator.Controllers;

namespace CaptchaGenerator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Muss oben stehen und web.config-eintrag haben!
  routes.MapRoute(
               name: "Boo",
               url: "Captcha/image.png",
               defaults: new { controller = "Captcha", action = "Foo" }
           );

  

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute( // todo reihenfolge?
                name: "CreateCaptcha",
                url: "Captcha/Create/{id}",
                defaults: new { controller = "Create", action = "Index", id = UrlParameter.Optional } // todo
            );

          
        }
    }
}

using System.Web.Mvc;
using System.Web.Routing;

namespace CaptchaGenerator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Muss oben stehen und web.config-eintrag haben!
            routes.MapRoute(
                name: "CaptchaImageSource",
                url: "Captcha/{id}.png",
                defaults: new { controller = "Captcha", action = "Image", id = 0 }
            );

            routes.MapRoute(
                name: "CreateCaptcha",
                url: "Create",
                defaults: new { controller = "Captcha", action = "Create" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
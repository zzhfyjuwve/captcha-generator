using System.Web.Mvc;
using System.Web.Routing;

namespace CaptchaGenerator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Muss ganz oben stehen (wegen des Matchings) und einen Web.config-Eintrag haben
            // (damit der Punkt keine Probleme verursacht).
            routes.MapRoute(
                name: "CaptchaImage",
                url: "captcha/{id}.png",
                defaults: new { controller = "Captcha", action = "Image", id = 0 }
            );

            routes.MapRoute(
                name: "CreateCaptcha",
                url: "create",
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
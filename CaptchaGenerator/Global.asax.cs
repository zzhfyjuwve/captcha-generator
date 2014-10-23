using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CaptchaGenerator
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GetAuthorizationToken();
        }

        public void GetAuthorizationToken()
        {
            var authorization = Application["Authorization"] as AuthenticationHeaderValue;

            if (authorization != null)
            {
                return;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CaptchaGenerator.WebApi.HttpClient.BaseAddress"]);

                var formUrlEncodedContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", ConfigurationManager.AppSettings["CaptchaGenerator.WebApi.Account.GrantType"] },
                    { "username", ConfigurationManager.AppSettings["CaptchaGenerator.WebApi.Account.Username"] },
                    { "password", ConfigurationManager.AppSettings["CaptchaGenerator.WebApi.Account.Password"] }
                });

                HttpResponseMessage response = client.PostAsync("/Token", formUrlEncodedContent).Result;

                var result = response.Content.ReadAsAsync<IDictionary<string, string>>().Result;

                authorization = new AuthenticationHeaderValue(result["token_type"], result["access_token"]);

                Application["Authorization"] = authorization;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace CaptchaGenerator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //alt
            //ViewBag.Message = "Your application description page.";

            //return View();
            //alt

            // http://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client
            // http://www.asp.net/web-api/overview/security/individual-accounts-in-web-api

            //using (var client = new HttpClient())
            //{
            //    // TODO - Send HTTP requests

            //    client.BaseAddress = Request.Url;
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    var gizmo = new RegisterBindingModel
            //    {
            //        Email = "user@example.com",
            //        Password = "P_ssw0rd",
            //        ConfirmPassword = "P_ssw0rd"
            //    };

            //    // New code:
            //    HttpResponseMessage response = client.PostAsJsonAsync("api/Account/Register", gizmo).Result;

            //}

            /*

            http://localhost:39065/Help/Api/POST-api-Account-Register

            POSTMAN:

            http://localhost:39065/api/Account/Register

            {
              "Email": "user@example.com",
              "Password": "P_ssw0rd",
              "ConfirmPassword": "P_ssw0rd"
            }

             */

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:39065/");//Request.Url;

                var formUrlEncodedContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"username","user@example.com"},
                    {"password","P_ssw0rd"}
                });

                HttpResponseMessage response = client.PostAsync("/Token", formUrlEncodedContent).Result;

                dynamic result = response.Content.ReadAsAsync<dynamic>().Result;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)result.access_token);

                response = client.GetAsync("values").Result;

                string[] product = response.Content.ReadAsAsync<string[]>().Result;
            }

            return View();
        }
    }
}
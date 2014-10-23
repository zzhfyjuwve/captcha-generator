using CaptchaGenerator.Models.Captcha;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace CaptchaGenerator.Controllers
{
    public class CaptchaController : Controller
    {
        // GET: Captcha
        public ActionResult Index()
        {
            return View();
        }

        // GET: Captcha/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Reset()
        {
            Session.Clear();

            return RedirectToAction("Create");
        }

        // GET: Captcha/Create
        public ActionResult Create()
        {
            var model = new CaptchaViewModel
            {
                Settings = Session["CaptchaSettings"] as CaptchaSettings ?? new CaptchaSettings()
            };

            return View(model);
        }

        // POST: Captcha/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CaptchaSettings settings)
        {
            Session["CaptchaSettings"] = settings;

            TempData["ShowCaptcha"] = true;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:39065/");

                client.DefaultRequestHeaders.Authorization = Authorization();

                var result = client.PostAsJsonAsync("api/values", settings).Result;

                CaptchaResult captchaResult = result.Content.ReadAsAsync<CaptchaResult>().Result;

                Session["CaptchaResult"] = captchaResult;
            }

            return RedirectToAction("Create");
        }

        public ActionResult Image()
        {
            return new FileContentResult(((CaptchaResult)Session["CaptchaResult"]).Image, "image/png");
        }

        // GET: Captcha/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Captcha/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Captcha/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Captcha/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // nicht aufrufbar, weil privat
        private AuthenticationHeaderValue Authorization()
        {
            var authorization = Session["Authorization"] as AuthenticationHeaderValue;

            if (authorization != null)
            {
                return authorization;
            }

            using (var client = new HttpClient())
            {
                // todo: in web.config
                client.BaseAddress = new Uri("http://localhost:39065/");

                var formUrlEncodedContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    // todo: in web.config
                    {"grant_type", "password"},
                    {"username","user@example.com"},
                    {"password","P_ssw0rd"}
                });

                HttpResponseMessage response = client.PostAsync("/Token", formUrlEncodedContent).Result;

                var result = response.Content.ReadAsAsync<IDictionary<string, string>>().Result;

                authorization = new AuthenticationHeaderValue(result["token_type"], result["access_token"]);

                Session["Authorization"] = authorization;

                return authorization;
            }
        }
    }
}
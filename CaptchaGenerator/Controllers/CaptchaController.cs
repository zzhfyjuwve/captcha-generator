using CaptchaGenerator.Models.Captcha;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Create(CaptchaSettings settings)
        {
            Session["CaptchaSettings"] = settings;

            TempData["ShowCaptcha"] = true;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CaptchaGenerator.WebApi.HttpClient.BaseAddress"]);

                client.DefaultRequestHeaders.Authorization = (AuthenticationHeaderValue)HttpContext.Application["Authorization"];

                CaptchaResult captchaResult = await client.PostAsJsonAsync("api/captcha", settings).
                    Result.Content.ReadAsAsync<CaptchaResult>();

                Session["CaptchaResult"] = captchaResult;
            }

            return RedirectToAction("Create");
        }

        public ActionResult Image()
        {
            return new FileContentResult(((CaptchaResult)Session["CaptchaResult"]).Image, "image/png");
        }

        public JsonResult IsCorrect(int solution)
        {
            return Json(((CaptchaResult)Session["CaptchaResult"]).Solution == solution, JsonRequestBehavior.AllowGet);
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
    }
}
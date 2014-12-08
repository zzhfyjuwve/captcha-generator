using AutoMapper;
using CaptchaEntities.Captcha;
using CaptchaGenerator.Models;
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
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Reset()
        {
            Session.Clear();

            return RedirectToAction("Create");
        }

        // GET: Captcha/Create
        public ActionResult Create()
        {
            var captchaViewModel = Session["CaptchaViewModel"] as CaptchaViewModel;

            if (captchaViewModel == null)
            {
                captchaViewModel = new CaptchaViewModel { Settings = new CaptchaSettings() };
                Session["CaptchaViewModel"] = captchaViewModel;
            }

            return View(captchaViewModel);
        }

        // POST: Captcha/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CaptchaSettings settings)
        {
            var captchaViewModel = Session["CaptchaViewModel"] as CaptchaViewModel;

            if (captchaViewModel == null)
            {
                throw new NullReferenceException();
            }

            captchaViewModel.Settings = settings;

            TempData["ShowCaptcha"] = true;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CaptchaGenerator.WebApi.HttpClient.BaseAddress"]);

                client.DefaultRequestHeaders.Authorization = (AuthenticationHeaderValue)HttpContext.Application["Authorization"];

                var httpResponseMessage =  client.PostAsJsonAsync("api/captcha", settings).Result;

                httpResponseMessage.EnsureSuccessStatusCode();

                // Beispiel für einen asynchronen Aufruf.
                var captchaEntity = await httpResponseMessage.Content.ReadAsAsync<CaptchaEntity>();

                // Erst speichern, sonst wird die ID nicht automatisch erzeugt.
                db.CaptchaEntities.Add(captchaEntity);
                await db.SaveChangesAsync();

                Session["CaptchaViewModel"] = Mapper.Map(captchaEntity, captchaViewModel);
            }

            return RedirectToAction("Create");
        }

        public ActionResult Image()
        {
            return new FileContentResult(((CaptchaViewModel)Session["CaptchaViewModel"]).Image, "image/png");
        }

        public JsonResult IsCorrect(int solution)
        {
            return Json(((CaptchaViewModel)Session["CaptchaViewModel"]).Solution == solution, JsonRequestBehavior.AllowGet);
        }
    }
}

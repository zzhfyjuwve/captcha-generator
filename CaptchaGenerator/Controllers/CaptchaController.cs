using CaptchaGenerator.Models.Captcha;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
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
            var an = new Random();
            int a = an.Next(100);
            int b = an.Next(100);
            Session["CaptchaTerm"] = string.Format("{0} + {1}", a, b);
            Session["CaptchaSolution"] = a + b;

            TempData["ShowCaptcha"] = true;

            return RedirectToAction("Create");
        }

        public ActionResult Foo()
        {
            CaptchaSettings model = Session["CaptchaSettings"] as CaptchaSettings;

            string text = Session["CaptchaTerm"] as string;

            using (var font = new Font(model.FontName, model.FontSize, FontStyle.Bold))
            {
                Size size;

                using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
                {
                    size = graphics.MeasureString(text, font).ToSize();
                }

                using (var bitmap = new Bitmap(size.Width + font.Height, size.Height + font.Height))
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.FromName(model.Color.ToString()));

                    graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    graphics.DrawString(text, font, Brushes.Black, graphics.VisibleClipBounds, new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                    bitmap.Save(Response.OutputStream, ImageFormat.Png);
                }
            }

            return new ContentResult { ContentType = "image/png" };
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
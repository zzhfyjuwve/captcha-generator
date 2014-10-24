using CaptchaEntities.Captcha;
using CaptchaGenerator.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CaptchaGenerator.Controllers
{
    public class GalleryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gallery
        public async Task<ActionResult> Index()
        {
            return View(await db.CaptchaEntities.ToListAsync());
        }

        // GET: Gallery/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaptchaEntity captchaEntity = await db.CaptchaEntities.FindAsync(id);
            if (captchaEntity == null)
            {
                return HttpNotFound();
            }
            return View(captchaEntity);
        }

        // GET: Gallery/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaptchaEntity captchaEntity = await db.CaptchaEntities.FindAsync(id);
            if (captchaEntity == null)
            {
                return HttpNotFound();
            }
            return View(captchaEntity);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CaptchaEntity captchaEntity = await db.CaptchaEntities.FindAsync(id);
            db.CaptchaEntities.Remove(captchaEntity);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
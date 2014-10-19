using System.Drawing.Text;
using System.Linq;
using System.Web.Mvc;

namespace CaptchaGenerator.Models.Captcha
{
    public class CaptchaViewModel
    {
        public SelectList FontNames
        {
            get
            {
                return new SelectList(new InstalledFontCollection().Families.Select(family => family.Name));
            }
        }

        public CaptchaSettings Settings { get; set; }
    }
}
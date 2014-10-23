using CaptchaGenerator.Localization;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [Remote("IsCorrect", "Captcha",
            ErrorMessageResourceName = "Solution", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Solution", ResourceType = typeof(Resources))]
        public string Solution { get; set; }
    }
}
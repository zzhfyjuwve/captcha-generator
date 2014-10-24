using CaptchaEntities.Captcha;
using CaptchaGenerator.Localization;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Text;
using System.Linq;
using System.Web.Mvc;

namespace CaptchaGenerator.Models.Captcha
{
    public class CaptchaViewModel : CaptchaEntity
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
        // Wir geben zusätzlich die ID mit, damit der Ajax-Aufruf auf keinen Fall gecached wird...
        [Remote("IsCorrect", "Captcha",
            ErrorMessageResourceName = "WrongSolution",
            ErrorMessageResourceType = typeof(Messages),
            AdditionalFields = "Id")]
        [Display(Name = "Solution", ResourceType = typeof(Resources))]
        public new int Solution { get; set; }
    }
}
using CaptchaEntities.Captcha.Enums;
using CaptchaGenerator.Localization;
using System.ComponentModel.DataAnnotations;

namespace CaptchaGenerator.Models.Captcha
{
    public class CaptchaSettings
    {
        public CaptchaSettings()
        {
            FontName = "Consolas";
            FontSize = 12;
        }

        [Display(Name = "FontName", ResourceType = typeof(Resources))]
        public string FontName { get; set; }

        [Display(Name = "FontSize", ResourceType = typeof(Resources))]
        [Range(6, 60)]
        public int FontSize { get; set; }

        [Display(Name = "Color", ResourceType = typeof(Resources))]
        public Color Color { get; set; }
    }
}
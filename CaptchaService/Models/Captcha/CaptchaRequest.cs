using CaptchaEntities.Captcha.Enums;

namespace CaptchaService.Models.Captcha
{
    public class CaptchaRequest
    {
        public string FontName { get; set; }

        public int FontSize { get; set; }

        public Color Color { get; set; }
    }
}
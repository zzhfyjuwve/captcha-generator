namespace CaptchaGenerator.Models.Captcha
{
    public class CaptchaResult
    {
        public byte[] Image { get; set; }

        public string Term { get; set; }

        public int Solution { get; set; }
    }
}
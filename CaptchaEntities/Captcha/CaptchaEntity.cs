namespace CaptchaEntities.Captcha
{
    public class CaptchaEntity
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public int Solution { get; set; }
    }
}
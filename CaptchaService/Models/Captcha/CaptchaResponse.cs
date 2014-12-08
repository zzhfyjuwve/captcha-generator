using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace CaptchaService.Models.Captcha
{
    public class CaptchaResponse : CaptchaEntities.Captcha.CaptchaEntity
    {
        public CaptchaResponse(CaptchaRequest request)
        {
            var a = new Random().Next(100);
            var b = new Random(a).Next(100);

            var term = string.Format("{0} + {1}", a, b);
            Solution = a + b;

            using (var font = new Font(request.FontName, request.FontSize, FontStyle.Bold))
            {
                var stringFormat = StringFormat.GenericTypographic;
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                Size size;
                using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
                {
                    size = graphics.MeasureString(term, font, new PointF(), stringFormat).ToSize();
                }
                using (var bitmap = new Bitmap(size.Width + font.Height, size.Height + font.Height))
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.FromName(request.Color.ToString()));
                    graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    graphics.DrawString(term, font, Brushes.Black, graphics.VisibleClipBounds, stringFormat);
                    using (var memoryStream = new MemoryStream())
                    {
                        bitmap.Save(memoryStream, ImageFormat.Png);
                        Image = memoryStream.ToArray();
                    }
                }
            }
        }
    }
}

using CaptchaService.Models.Captcha;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Web.Http;

namespace CaptchaService.Controllers
{
    [Authorize]
    [RoutePrefix("values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [Route]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public CaptchaResponse Post(CaptchaRequest captchaRequest)
        {
            // http://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/

            // todo: wohin die businessmethoden?

            var random = new Random();

            int a = random.Next(100);
            int b = random.Next(100);

            string term = string.Format("{0} + {1}", a, b);
            int solution = a + b;

            using (var font = new Font(captchaRequest.FontName, captchaRequest.FontSize, FontStyle.Bold))
            {
                StringFormat stringFormat = StringFormat.GenericTypographic;
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                Size size;

                using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
                {
                    size = graphics.MeasureString(term, font, new PointF(), stringFormat).ToSize();
                }

                using (var bitmap = new Bitmap(size.Width + font.Height, size.Height + font.Height))
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.FromName(captchaRequest.Color.ToString()));
                    graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    graphics.DrawString(term, font, Brushes.Black, graphics.VisibleClipBounds, stringFormat);
                    using (var memoryStream = new MemoryStream())
                    {
                        bitmap.Save(memoryStream, ImageFormat.Png);

                        //memoryStream.Close();

                        return new CaptchaResponse
                        {
                            Image = memoryStream.ToArray(),
                            Term = term,
                            Solution = solution
                        };
                    }
                }
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
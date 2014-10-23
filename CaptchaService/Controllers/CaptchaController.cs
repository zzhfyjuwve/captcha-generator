using CaptchaService.Models.Captcha;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Web.Http;

namespace CaptchaService.Controllers
{
    [Authorize]
    [RoutePrefix("captcha")]
    public class CaptchaController : ApiController
    {
        // GET api/captcha
        [Route]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/captcha/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/captcha
        public CaptchaResponse Post(CaptchaRequest request)
        {
           return new CaptchaResponse(request);
        }

        // PUT api/captcha/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/captcha/5
        public void Delete(int id)
        {
        }
    }
}
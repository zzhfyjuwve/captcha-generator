using AutoMapper;
using CaptchaEntities.Captcha;
using CaptchaGenerator.Models.Captcha;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CaptchaGenerator.Startup))]
namespace CaptchaGenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            Mapper.CreateMap<CaptchaEntity, CaptchaViewModel>().ForMember(
                captchaViewModel => captchaViewModel.FontNames,
                expression => expression.Ignore()).ForMember(
                captchaViewModel => captchaViewModel.Settings,
                expression => expression.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}

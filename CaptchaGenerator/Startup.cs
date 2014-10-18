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
        }
    }
}

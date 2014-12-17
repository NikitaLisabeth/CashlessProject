using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nmct.ba.cashlessproject.WebApp.Startup))]
namespace nmct.ba.cashlessproject.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

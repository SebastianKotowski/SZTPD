using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SZPD.Startup))]
namespace SZPD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

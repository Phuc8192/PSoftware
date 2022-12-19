using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PSoftware.Startup))]
namespace PSoftware
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

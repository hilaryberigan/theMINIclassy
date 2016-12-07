using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(theMINIclassy.Startup))]
namespace theMINIclassy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

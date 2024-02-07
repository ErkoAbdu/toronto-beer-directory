using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TorontoBeerDirectory.Startup))]
namespace TorontoBeerDirectory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

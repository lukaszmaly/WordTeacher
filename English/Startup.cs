using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(English.Startup))]
namespace English
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

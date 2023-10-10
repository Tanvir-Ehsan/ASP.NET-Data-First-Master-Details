using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(finalmvcfirst.Startup))]
namespace finalmvcfirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

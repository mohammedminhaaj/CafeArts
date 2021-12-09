using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CafeArts.Startup))]
namespace CafeArts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

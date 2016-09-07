using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YouGo.Startup))]
namespace YouGo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

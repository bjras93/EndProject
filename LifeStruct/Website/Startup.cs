using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LifeStruct.Startup))]
namespace LifeStruct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

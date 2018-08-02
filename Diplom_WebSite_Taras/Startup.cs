using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Diplom_WebSite_Taras.Startup))]
namespace Diplom_WebSite_Taras
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

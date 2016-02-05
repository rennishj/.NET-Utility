using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RJ.MVC.Startup))]
namespace RJ.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

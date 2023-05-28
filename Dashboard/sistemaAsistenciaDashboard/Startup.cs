using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sistemaAsistenciaDashboard.Startup))]
namespace sistemaAsistenciaDashboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

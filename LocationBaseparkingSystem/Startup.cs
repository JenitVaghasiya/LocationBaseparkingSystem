using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocationBaseparkingSystem.Startup))]
namespace LocationBaseparkingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BikeStore.Startup))]
namespace BikeStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

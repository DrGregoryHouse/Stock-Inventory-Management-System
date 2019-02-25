using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockInventorySystem.Startup))]
namespace StockInventorySystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

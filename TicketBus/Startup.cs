using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicketBus.Startup))]
namespace TicketBus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

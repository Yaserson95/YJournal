using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YJournal.Startup))]
namespace YJournal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(projectory.Startup))]
namespace projectory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

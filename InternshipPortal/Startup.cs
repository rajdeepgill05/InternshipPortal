using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InternshipPortal.Startup))]
namespace InternshipPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

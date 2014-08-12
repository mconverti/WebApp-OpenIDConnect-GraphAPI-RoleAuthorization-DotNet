[assembly: Microsoft.Owin.OwinStartup(typeof(WebAppGraphAPIRoleAuthorization.Startup))]

namespace WebAppGraphAPIRoleAuthorization
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}

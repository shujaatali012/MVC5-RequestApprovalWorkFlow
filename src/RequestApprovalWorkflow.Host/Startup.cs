using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RequestApprovalWorkflow.Host.Startup))]
namespace RequestApprovalWorkflow.Host
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

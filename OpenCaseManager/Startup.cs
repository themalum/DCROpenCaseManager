using Microsoft.Owin;
using Owin;

namespace OpenCaseManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

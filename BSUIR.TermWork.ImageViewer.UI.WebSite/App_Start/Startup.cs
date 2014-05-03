using Microsoft.AspNet.SignalR;

using Owin;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration { EnableJavaScriptProxies = true };

            app.MapSignalR("/messaging", hubConfiguration);
        }
    }
}
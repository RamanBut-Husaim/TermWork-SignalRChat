using Owin;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
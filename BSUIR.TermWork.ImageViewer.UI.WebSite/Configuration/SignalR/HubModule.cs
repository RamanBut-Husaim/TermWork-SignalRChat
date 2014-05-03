using System.Reflection;

using Autofac;
using Autofac.Integration.SignalR;

using BSUIR.TermWork.ImageViewer.UI.WebSite.Hubs;

using Module = Autofac.Module;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.SignalR
{
    public sealed class HubModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
        }
    }
}
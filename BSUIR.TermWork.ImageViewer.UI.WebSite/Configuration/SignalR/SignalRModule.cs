using Autofac;

using BSUIR.TermWork.ImageViewer.UI.Infrastructure.IdProvider;

using Microsoft.AspNet.SignalR;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.SignalR
{
    public sealed class SignalRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomUserIdProvider>().As<IUserIdProvider>().ExternallyOwned();
        }
    }
}
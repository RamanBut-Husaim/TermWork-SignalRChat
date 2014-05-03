using Autofac;

using BSUIR.TermWork.ImageViewer.Services;
using BSUIR.TermWork.ImageViewer.Services.Contracts;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.SignalR
{
    public sealed class ServiceModule : Module
    {
        #region Overrides of Module

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MembershipService>()
                   .As<IMembershipService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<FriendshipService>()
                   .As<IFriendshipService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<MessageService>().As<IMessageService>().InstancePerLifetimeScope();
            base.Load(builder);
        }

        #endregion
    }
}
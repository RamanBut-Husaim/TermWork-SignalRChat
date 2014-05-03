using Autofac;

using BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.SignalR
{
    public sealed class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserAccountMapper>()
                   .As<IUserAccountMapper>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<FriendshipMapper>().As<IFriendshipMapper>().InstancePerLifetimeScope();
            builder.RegisterType<ChatMapper>().As<IChatMapper>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
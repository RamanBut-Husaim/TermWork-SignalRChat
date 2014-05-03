using Autofac;
using Autofac.Integration.Mvc;

using BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.Mvc
{
    public sealed class MapperModule : Module
    {
        #region Overrides of Module

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserAccountMapper>()
                   .As<IUserAccountMapper>()
                   .InstancePerHttpRequest();
            builder.RegisterType<AlbumMapper>().As<IAlbumMapper>().InstancePerHttpRequest();
            builder.RegisterType<ImageMapper>().As<IImageMapper>().InstancePerHttpRequest();
            builder.RegisterType<CommentMapper>().As<ICommentMapper>().InstancePerHttpRequest();
            builder.RegisterType<SubscriptionMapper>()
                   .As<ISubscriptionMapper>()
                   .InstancePerHttpRequest();
            builder.RegisterType<SearchMapper>().As<ISearchMapper>().InstancePerHttpRequest();
            builder.RegisterType<FriendshipMapper>()
                   .As<IFriendshipMapper>()
                   .InstancePerHttpRequest();
            builder.RegisterType<ChatMapper>().As<IChatMapper>().InstancePerHttpRequest();
            base.Load(builder);
        }

        #endregion
    }
}
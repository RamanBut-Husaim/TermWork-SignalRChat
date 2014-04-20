using Autofac;
using Autofac.Integration.Mvc;

using BSUIR.TermWork.ImageViewer.Services;
using BSUIR.TermWork.ImageViewer.Services.Contracts;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration
{
    public sealed class ServiceModule : Module
    {
        #region Overrides of Module

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MembershipService>()
                   .As<IMembershipService>()
                   .InstancePerHttpRequest();
            builder.RegisterType<ImageAlbumService>()
                   .As<IImageAlbumService>()
                   .InstancePerHttpRequest();
            builder.RegisterType<ImageResizingService>()
                   .As<IImageResizingService>()
                   .InstancePerHttpRequest();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerHttpRequest();
            builder.RegisterType<SubscriptionService>()
                   .As<ISubscriptionService>()
                   .InstancePerHttpRequest();
            builder.RegisterType<SearchService>().As<ISearchService>().InstancePerHttpRequest();
            base.Load(builder);
        }

        #endregion
    }
}
using Autofac;

using BSUIR.TermWork.ImageViewer.Data;
using BSUIR.TermWork.ImageViewer.Data.EF;
using BSUIR.TermWork.ImageViewer.Data.EF.Repositories.Factory;
using BSUIR.TermWork.ImageViewer.Data.Repositories.Factory;
using BSUIR.TermWork.ImageViewer.Shared;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.SignalR
{
    public sealed class RepositoryModule : Module
    {
        #region Overrides of Module

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterInstance((IRepositoryFactory)RepositoryFactory.Instance)
                   .ExternallyOwned();
            builder.RegisterType<ImageViewerContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<HashGenerator>().As<IHashGenerator>().InstancePerLifetimeScope();
            base.Load(builder);
        }

        #endregion
    }
}
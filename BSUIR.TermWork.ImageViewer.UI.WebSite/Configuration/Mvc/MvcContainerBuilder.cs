using Autofac;
using Autofac.Integration.Mvc;

using BSUIR.TermWork.ImageViewer.UI.Infrastructure;
using BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.SignalR;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.Mvc
{
    public class MvcContainerBuilder
    {
        private readonly ContainerBuilder _containerBuilder;

        public MvcContainerBuilder()
        {
            this._containerBuilder = new ContainerBuilder();
        }

        public ContainerBuilder ContainerBuilder
        {
            get { return this._containerBuilder; }
        }

        public void RegisterComponents()
        {
            this._containerBuilder.RegisterModule(new ValidatorModule());
            this._containerBuilder.RegisterModule(new RepositoryModule());
            this._containerBuilder.RegisterModule(new ServiceModule());
            this._containerBuilder.RegisterModule(new MvcModule());
            this._containerBuilder.RegisterModule(new MapperModule());
            this._containerBuilder.RegisterModule(new SignalRModule());
            this._containerBuilder.RegisterModule(new HubModule());

            this._containerBuilder.RegisterType<ImageConverter>()
                .As<IImageConverter>()
                .InstancePerHttpRequest();
        }
    }
}
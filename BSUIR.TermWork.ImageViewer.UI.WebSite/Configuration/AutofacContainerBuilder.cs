using Autofac;
using Autofac.Integration.Mvc;

using BSUIR.TermWork.ImageViewer.UI.Infrastructure;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration
{
    public class AutofacContainerBuilder
    {
        private readonly ContainerBuilder _containerBuilder;

        public AutofacContainerBuilder()
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

            this._containerBuilder.RegisterType<ImageConverter>()
                .As<IImageConverter>()
                .InstancePerHttpRequest();
        }
    }
}
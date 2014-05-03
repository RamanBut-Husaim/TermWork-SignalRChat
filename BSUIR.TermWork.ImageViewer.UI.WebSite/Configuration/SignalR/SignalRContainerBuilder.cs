using Autofac;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.SignalR
{
    public sealed class SignalRContainerBuilder
    {
        private readonly ContainerBuilder _containerBuilder;

        public SignalRContainerBuilder()
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
            this._containerBuilder.RegisterModule(new MapperModule());
            this._containerBuilder.RegisterModule(new SignalRModule());
            this._containerBuilder.RegisterModule(new HubModule());
        }
    }
}
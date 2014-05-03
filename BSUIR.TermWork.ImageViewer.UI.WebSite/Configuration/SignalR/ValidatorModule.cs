using Autofac;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;
using BSUIR.TermWork.ImageViewer.Services.Validators;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.SignalR
{
    public sealed class ValidatorModule : Module
    {
        #region Overrides of Module

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance((IValidatorFactory)ValidatorFactory.Instance).ExternallyOwned();
            builder.Register(c => c.Resolve<IValidatorFactory>().BuildEntityValidator<User>());
            builder.Register(c => c.Resolve<IValidatorFactory>().BuildEntityValidator<Message>());
            base.Load(builder);
        }

        #endregion
    }
}
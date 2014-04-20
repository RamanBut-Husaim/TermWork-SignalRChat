using System.Reflection;

using Autofac;
using Autofac.Integration.Mvc;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration
{
    public class MvcModule : Autofac.Module
    {
        #region Overrides of Module

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            base.Load(builder);
        }

        #endregion
    }
}
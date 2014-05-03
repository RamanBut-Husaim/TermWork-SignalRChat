// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//   
// </copyright>
// <summary>
//   The mvc application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Autofac.Integration.Mvc;

using BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.Mvc;
using BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration.SignalR;

using Microsoft.AspNet.SignalR;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite
{
    /// <summary>
    /// The mvc application.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        #region Methods

        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            AppDomain.CurrentDomain.SetData(
                "DataDirectory", 
                @"E:\BSUIR\_Fourth_Year\7-th_term\TermWork\BSUIR.TermWork.ImageViewer");
            var mvcContainerBuilder = new MvcContainerBuilder();
            mvcContainerBuilder.RegisterComponents();
            var f = mvcContainerBuilder.ContainerBuilder.Build();
            var dependencyResolver =
                new AutofacDependencyResolver(f);
            DependencyResolver.SetResolver(dependencyResolver);

            var signalRContainerBuilder = new SignalRContainerBuilder();
            var signalRDependencyResolver =
                new Autofac.Integration.SignalR.AutofacDependencyResolver(
                    f);
            GlobalHost.DependencyResolver = signalRDependencyResolver;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        #endregion
    }
}
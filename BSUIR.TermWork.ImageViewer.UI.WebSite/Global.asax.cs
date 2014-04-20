using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite
{
    using Autofac.Integration.Mvc;

    using BSUIR.TermWork.ImageViewer.UI.WebSite.Configuration;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", @"E:\BSUIR\_Fourth_Year\7-th_term\TermWork\BSUIR.TermWork.ImageViewer");
            var containerBuilder = new AutofacContainerBuilder();
            containerBuilder.RegisterComponents();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder.ContainerBuilder.Build()));


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

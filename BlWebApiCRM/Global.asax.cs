using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.WebApi;
using BlCRM.Interface;
using BlCRM;
using DalMain.Manager;
using DalMain.Interface;
using BlReportsEngine.Interface;
using BlReportsEngine;
using BlInvoice.Interface;
using BlInvoice;
using BlOptimalPackage.Interface;
using BlLogin.Interface;
using BlLogin;
using BlSimulator.Interface;
using BlOptimalPackage;

namespace BlWebApiCRM
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:

            container.Register<IDalCRMWrite, CRMManagerWrite>(Lifestyle.Scoped);
            container.Register<IDalCRMRead, CRMManagerRead>(Lifestyle.Scoped);
            container.Register<IDalBIReportRead, BlReportManagerRead>(Lifestyle.Scoped);
            container.Register<IDalSimulatorWrite, SimulatorManagerWrite>(Lifestyle.Scoped);
            container.Register<IDalSimulatorRead, SimulatorManagerRead>(Lifestyle.Scoped);
            container.Register<IDalOptimalPackage, OptimalPackageManagerRead>(Lifestyle.Scoped);
            container.Register<IDalLoginRead, LoginManagerRead>(Lifestyle.Scoped);
            container.Register<IDalInvoiceRead, InvoiceManagerRead>(Lifestyle.Scoped);

            container.Register<IBlClient, BlClient>(Lifestyle.Scoped);
            container.Register<IBlReport, BlReport>(Lifestyle.Scoped);
            container.Register<IBlLineAndPackage, BlLineAndPackage>(Lifestyle.Scoped);
            container.Register<IBlLoginEmployee, BlLoginEmployee>(Lifestyle.Scoped);
            container.Register<IBlInvoiceCalculation, BlInvoiceCalculation>(Lifestyle.Scoped);
            container.Register<IBlLoginMain, BlLoginMain>(Lifestyle.Scoped);
            container.Register<IBlOptimalPackage, BlOptimalPackage.BlOptimalPackage>(Lifestyle.Scoped);
            container.Register<IBlSimulator, BlSimulator.BlSimulator>(Lifestyle.Scoped);
            container.Register<IBlLoginClient, BlLoginClient>(Lifestyle.Scoped);
            container.Register<IBlLoginManager, BlLoginManager>(Lifestyle.Scoped);
            container.Register<IApiControllerBase, ApiControllerBase>(Lifestyle.Scoped);




            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();


            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

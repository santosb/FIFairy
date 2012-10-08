using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace FIfairy
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "DownloadPrePatEmailFile", // Route name
            //    "DownloadPrePatEmailFile", // URL with parameters
            //    new { controller = "ReleaseDetails", action = "DownloadPrePatEmailFile" } // Parameter defaults
            //    );

            routes.MapRoute("CreateRelease", "CreateRelease",
                            new { Controller = "Release", action = "Create" });

            //routes.MapRoute("Release", "Release/{releaseNumber}",
            //                new {Controller = "Release", action = "Index"});

            routes.MapRoute(
                "ReleaseByDate", // Route name
                "ReleasesByDate/{year}/{month}/{day}", // URL with parameters
                new {controller = "Release", action = "ReleaseByDate"} // Parameter defaults
                );

            //routes.MapRoute(
            //    "Release", // Route name
            //    "{controller}/{action}", // URL with parameters
            //    new { controller = "Release", action = "Index"} // Parameter defaults
            //    );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            ConfigureIoc();
        }

        private void ConfigureIoc()
        {
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory(ObjectFactory.Container));
            var _wiringDefinition = new WiringDefinition();
            _wiringDefinition.Configure(ObjectFactory.Container);
        }
    }
}
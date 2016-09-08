using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MediatR;
using SimpleCQRS;

namespace CQRSGui
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            ServiceLocator.Mediator = new Mediator((t) =>
            {
                var type = typeof(InventoryCommandHandlers).Assembly
                    .GetTypes()
                    .First(t.IsAssignableFrom);
                return Activator.CreateInstance(type, ServiceLocator.InventoryRepository);
            }, (t) =>
            {
                return typeof(InventoryCommandHandlers).Assembly
                    .GetTypes()
                    .Where(t.IsAssignableFrom)
                    .Select(Activator.CreateInstance);
            });
            
            var storage = new EventStore((e) => { ServiceLocator.Mediator.Publish(e); });
            ServiceLocator.InventoryRepository = new Repository<InventoryItem>(storage);
        }
    }
}
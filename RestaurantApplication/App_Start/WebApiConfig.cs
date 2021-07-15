using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace RestaurantApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            #region IoC for APIB
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<RestaurantApplication.Utility.IOCModule>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            #endregion
        }
    }
}

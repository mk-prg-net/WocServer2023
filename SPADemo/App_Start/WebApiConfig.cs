using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SPADemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            GlobalConfiguration.Configuration.Formatters.Add(new Formater.HtmlWebApiFormater());
            GlobalConfiguration.Configuration.Formatters.Add(new Formater.NamingContainerFormater());

            // Tracing konfigurieren. 
            // https://learn.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/tracing-in-aspnet-web-api

            var traceWriter = config.EnableSystemDiagnosticsTracing();

            // Alles Meldungstypen werden ausführlich protokolliert
            traceWriter.IsVerbose = true;
            traceWriter.MinimumLevel = System.Web.Http.Tracing.TraceLevel.Debug;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

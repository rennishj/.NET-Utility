using Newtonsoft.Json.Serialization;
using RJ.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RJ.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
         
            // Web API routes            
            config.MapHttpAttributeRoutes();
            CamelCaseJson(config);
            config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void CamelCaseJson(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }

        private static void ConfigureOAuthBearerTokens()
        { 
            //new Startup().ConfigureAuth()
        }

    }
}

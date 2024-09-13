using JichangeApi.Models.JWTAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JichangeApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {


            // Register your custom JWT Authentication filter
            config.Filters.Add(new JwtAuthenticationFilter());


            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            /*config.Routes.MapHttpRoute(
            name: "ControllerAndActionOnly",
            routeTemplate: "api/{controller}/{action}",
            defaults: new { },
            constraints: new { action = @"^[a-zA-Z]+([\s][a-zA-Z]+)*$" });

            config.Routes.MapHttpRoute(
                name: "DefaultApiAll",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            /*config.Routes.MapHttpRoute(
                name: "Angular",
                routeTemplate: "#/**",
                defaults: new { controller = "Home", action = "Index" }
            );*/
        }
    }
}

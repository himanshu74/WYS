﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WYS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "checkUser",
               routeTemplate: "api/users/check/{param}",
               defaults: new { controller = "User", param = RouteParameter.Optional, action = "CheckUsername" }
           );
           

            config.Routes.MapHttpRoute(
               name: "login",
               routeTemplate: "api/users/login/{param}",
               defaults: new { controller = "Login", param = RouteParameter.Optional , action = "Login"}
           );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

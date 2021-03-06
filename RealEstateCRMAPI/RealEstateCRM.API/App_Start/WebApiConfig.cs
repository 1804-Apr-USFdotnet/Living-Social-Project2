﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RealEstateCRM.API
{
    public static class WebApiConfig
    {
        public static string AuthenticationType = "ApplicationCookie";
        public static string CookieName = "ApplicationCookie";

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
            = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling
            = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.Filters.Add(new AuthorizeAttribute());
            config.EnableCors();

            //// adds authorization to EVERYTHING
            config.Filters.Add(new AuthorizeAttribute());

           



    

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

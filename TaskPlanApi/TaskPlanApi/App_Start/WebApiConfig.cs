using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace TaskPlanApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {          
           
           /* var cors = new EnableCorsAttribute("*","*","*");
            config.EnableCors(cors);*/
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

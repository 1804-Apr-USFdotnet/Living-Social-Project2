using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(RealEstateCRM.API.Startup))]

namespace RealEstateCRM.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

             
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie"
            });

            app.UseWebApi(config);

            RouteTable.Routes.MapRoute(
                name: "MvcRoute",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "home",
                    action = "index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
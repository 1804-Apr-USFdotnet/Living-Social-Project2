using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Cors;
using System.Web.Http.Cors;

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
                AuthenticationType = WebApiConfig.AuthenticationType,
                CookieName = WebApiConfig.CookieName
            });

            app.UseWebApi(config);

            



        }
    }
}
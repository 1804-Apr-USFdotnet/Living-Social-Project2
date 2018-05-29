using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RealEstateCRMConsumer.Controllers
{
    public abstract class AServiceController : Controller
    {
        //protected static readonly HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseCookies = false });

        protected static readonly HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseCookies = false })
            {
                Timeout = TimeSpan.FromMinutes(5)

            };

    private static readonly Uri serviceUri = new Uri("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/");
        private static readonly string cookieName = "ApplicationCookie";

        protected HttpRequestMessage CreateRequestToService(HttpMethod method, string uri)
        {
            var apiRequest = new HttpRequestMessage(method, new Uri(serviceUri, uri));

            string cookieValue = Request.Cookies[cookieName]?.Value ?? ""; // ?. operator new in C# 7

            apiRequest.Headers.Add("Cookie", new CookieHeaderValue(cookieName, cookieValue).ToString());

            return apiRequest;
        }

        protected bool PassCookiesToClient(HttpResponseMessage apiResponse)
        {
            if (apiResponse.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                foreach (string value in values)
                {
                    Response.Headers.Add("Set-Cookie", value);
                }
                return true;
            }
            return false;
        }
    }
}
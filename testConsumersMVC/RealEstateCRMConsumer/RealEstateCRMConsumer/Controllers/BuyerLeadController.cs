using RealEstateCRMConsumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

using System.Web.Mvc;
using Newtonsoft.Json;

namespace RealEstateCRMConsumer.Controllers
{
    public class BuyerLeadController : Controller
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // GET: BuyerLead
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/BuyerLeads/");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }


            var buyerlead = await response.Content.ReadAsAsync<IEnumerable<BuyerLead>>();

            return View(buyerlead);
        }


    }
}
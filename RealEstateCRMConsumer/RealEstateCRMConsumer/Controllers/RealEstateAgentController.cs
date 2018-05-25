using RealEstateCRMConsumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealEstateCRMConsumer.Controllers
{
    public class RealEstateAgentController : Controller
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // GET: RealEstateAgent
        public async Task<ActionResult> Index()
        {

            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/RealEstateAgents/");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var realestateagents = await response.Content.ReadAsAsync<IEnumerable<RealEstateAgent>>();

            return View(realestateagents);
        }

        // GET: RealEstateAgent/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/RealEstateAgents/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataRealEstateAgent = response.Content.ReadAsAsync<RealEstateAgent>().Result;

            return View(responseDataRealEstateAgent);
        }

        // GET: RealEstateAgent/Create
        public ActionResult Create()
        {
            return View(new RealEstateAgent());
        }

        // POST: RealEstateAgent/Create
        [HttpPost]
        public async Task<ActionResult> Create(RealEstateAgent realEstateAgent)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:57955/api/RealEstateAgents/", realEstateAgent);

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        // GET: RealEstateAgent/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/RealEstateAgents/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataRealEstateAgent = response.Content.ReadAsAsync<RealEstateAgent>().Result;

            return View(responseDataRealEstateAgent);
        }

        // POST: RealEstateAgent/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, RealEstateAgent realEstateAgentToEdit)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync("http://localhost:57955/api/RealEstateAgents/" + id, realEstateAgentToEdit);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        // GET: RealEstateAgent/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/RealEstateAgents/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataRealEstateAgent = response.Content.ReadAsAsync<RealEstateAgent>().Result;

            return View(responseDataRealEstateAgent);
        }

        // POST: RealEstateAgent/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync("http://localhost:57955/api/RealEstateAgents/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}

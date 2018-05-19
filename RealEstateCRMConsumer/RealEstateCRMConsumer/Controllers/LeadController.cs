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
    public class LeadController : Controller
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // GET: Lead
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Leads/");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var lead = await response.Content.ReadAsAsync<IEnumerable<Lead>>();

            return View(lead);
        }

        // GET: Lead/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Leads/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataLead = response.Content.ReadAsAsync<Lead>().Result;

            return View(responseDataLead);
        }

        // GET: Lead/Create
        public ActionResult Create()
        {
            ViewBag.LeadType = new SelectList("Buyer", "Seller");

            return View(new Lead());
        }

        // POST: Lead/Create
        [HttpPost]
        public async Task<ActionResult> Create(Lead lead)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:57955/api/Leads/", lead);

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        // GET: Lead/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Leads/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataLead = response.Content.ReadAsAsync<Lead>().Result;

            return View(responseDataLead);
        }

        // POST: Lead/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Lead leadToEdit)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync("http://localhost:57955/api/Leads/" + id, leadToEdit);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        // GET: Lead/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Leads/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataLead = response.Content.ReadAsAsync<Lead>().Result;

            return View(responseDataLead);
        }

        // POST: Lead/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Lead leadToDelete)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync("http://localhost:57955/api/Leads/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}

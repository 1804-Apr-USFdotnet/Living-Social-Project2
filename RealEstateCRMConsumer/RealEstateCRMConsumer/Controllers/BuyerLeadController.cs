﻿using RealEstateCRMConsumer.Models;
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
            HttpResponseMessage response = await httpClient.GetAsync("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/api/BuyerLeads/");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }


            var buyerlead = await response.Content.ReadAsAsync<IEnumerable<BuyerLead>>();

            return View(buyerlead);
        }

        public ActionResult Create()
        {
            return View(new BuyerLead());
        }

        //POST
        [HttpPost]
        public async Task<ActionResult> Create(BuyerLead buyerLead)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/api/BuyerLeads/", buyerLead);

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/api/BuyerLeads/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataBuyerLead = response.Content.ReadAsAsync<BuyerLead>().Result;

            return View(responseDataBuyerLead);
        }

        //PUT
        [HttpPost]
        public async Task<ActionResult> Edit (int id, BuyerLead buyerLeadToEdit)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/api/BuyerLeads/" + id, buyerLeadToEdit);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/api/BuyerLeads/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataBuyerLead = response.Content.ReadAsAsync<BuyerLead>().Result;

            //var buyerLeadToDelete = JsonConvert.DeserializeObject<BuyerLead>(responseData);

            return View(responseDataBuyerLead);
        }

        //DELETE
        [HttpPost]
        public async Task<ActionResult> Delete(int id, BuyerLead buyerLeadToDelete)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/api/BuyerLeads/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}
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
using System.Net.Http.Formatting;

namespace RealEstateCRMConsumer.Controllers
{
    public class LeadController : AServiceController
    {

        // GET: Lead
        public async Task<ActionResult> Index()
        {
            HttpRequestMessage userRequest = CreateRequestToService(HttpMethod.Get, "api/Users/currentuser");
            HttpResponseMessage userResponse = await httpClient.SendAsync(userRequest);
            var curUser = userResponse.Content.ReadAsAsync<DataTransfer>().Result;


            // store user in session
            Session["role"] = curUser.roles[0];
            if(Session["role"] as string == "User")
            {
                HttpRequestMessage usersRequest = CreateRequestToService(HttpMethod.Get, "api/Users");
                HttpResponseMessage usersResponse = await httpClient.SendAsync(usersRequest);
                var users = await usersResponse.Content.ReadAsAsync<IEnumerable<User>>();
                Session["user_id"] = users.First(u => u.Email == curUser.userName).UserId;
            }
            else if(Session["role"] as string == "Agent")
            {
                HttpRequestMessage agentsRequest = CreateRequestToService(HttpMethod.Get, "api/Agents");
                HttpResponseMessage agentsResponse = await httpClient.SendAsync(agentsRequest);
                var agents = await agentsResponse.Content.ReadAsAsync<IEnumerable<RealEstateAgent>>();
                Session["agent_id"] = agents.First(a => a.Email == curUser.userName);
            }
            // make request for leads 
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/leads");
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

       
            var lead = await response.Content.ReadAsAsync<IEnumerable<Lead>>();
            
            return View(lead);
        }

        // GET: Lead/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/leads/" +id);
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);
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

            return View(new Lead());
        }

        // POST: Lead/Create
        [HttpPost]
        public async Task<ActionResult> Create(Lead lead)
        {
            

            HttpRequestMessage leadRequest = CreateRequestToService(HttpMethod.Post, "api/Leads");
            leadRequest.Content = new ObjectContent<Lead>(lead, new JsonMediaTypeFormatter());
            HttpResponseMessage leadResponse = await httpClient.SendAsync(leadRequest);
            PassCookiesToClient(leadResponse);


            return RedirectToAction("Index");
        }

        // GET: Lead/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            

            HttpRequestMessage leadRequest = CreateRequestToService(HttpMethod.Get, "api/Leads/" +id);
            HttpResponseMessage leadResponse = await httpClient.SendAsync(leadRequest);
            if (!leadResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataLead = leadResponse.Content.ReadAsAsync<Lead>().Result;

            return View(responseDataLead);
        }

        // POST: Lead/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Lead leadToEdit)
        {
            HttpRequestMessage leadRequest = CreateRequestToService(HttpMethod.Put, "api/Leads/"+id);
            leadRequest.Content = new ObjectContent<Lead>(leadToEdit, new JsonMediaTypeFormatter());
            HttpResponseMessage leadResponse = await httpClient.SendAsync(leadRequest);

            if (!leadResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        // GET: Lead/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpRequestMessage leadRequest = CreateRequestToService(HttpMethod.Get, "api/Leads/" + id);
            HttpResponseMessage leadResponse = await httpClient.SendAsync(leadRequest);
            if (!leadResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataLead = leadResponse.Content.ReadAsAsync<Lead>().Result;

            return View(responseDataLead);
        }

        // POST: Lead/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Lead leadToDelete)
        {
            HttpRequestMessage leadRequest = CreateRequestToService(HttpMethod.Delete, "api/Leads/" + id);
            leadRequest.Content = new ObjectContent<Lead>(leadToDelete, new JsonMediaTypeFormatter());
            HttpResponseMessage leadResponse = await httpClient.SendAsync(leadRequest);
            if (!leadResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}

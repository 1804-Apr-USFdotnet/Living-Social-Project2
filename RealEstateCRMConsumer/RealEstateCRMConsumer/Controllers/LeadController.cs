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
using System.Net.Http.Formatting;

namespace RealEstateCRMConsumer.Controllers
{
    public class LeadController : AServiceController
    {

        // GET: Lead
        public async Task<ActionResult> Index()
        {
            // make request for leads 
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/leads");
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            // make request for user
            HttpRequestMessage userRequest = CreateRequestToService(HttpMethod.Get, "api/Users/currentUser");
            HttpResponseMessage userResponse = await httpClient.SendAsync(userRequest);
            PassCookiesToClient(response);

            HttpRequestMessage userListRequest = CreateRequestToService(HttpMethod.Get, "api/Users");
            HttpResponseMessage userListResponse = await httpClient.SendAsync(userListRequest);
            PassCookiesToClient(userListResponse);
            if (!response.IsSuccessStatusCode || !userResponse.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            // get leads from lead response
            var lead = await response.Content.ReadAsAsync<IEnumerable<Lead>>();
            // get user info from user response
            var user = await userResponse.Content.ReadAsAsync<DataTransfer>();
            var listUsers = await userListResponse.Content.ReadAsAsync<IEnumerable<User>>();
            // check if role is user 
            if (user.roles[0].ToLower() == "user")
            {
                // find leads where user email is the passed in user email 
                User singleUser = listUsers.First(ul => ul.Email == user.userName);

                //IEnumerable<User> userLeads = user.Where(u => u.Email == userName);
                IEnumerable<Lead> userLeads = lead.Where(u => u.UserId == singleUser.UserId); 
                
                return View(userLeads);
            }

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
           // ViewBag.LeadType = new SelectList("Buyer", "Seller");

            return View(new Lead());
        }

        // POST: Lead/Create
        [HttpPost]
        public async Task<ActionResult> Create(Lead lead)
        {
            // get current user
            //HttpRequestMessage userRequest = CreateRequestToService(HttpMethod.Get, "api/Users/currentUser");
            //HttpResponseMessage userResponse = await httpClient.SendAsync(userRequest);
            //PassCookiesToClient(userResponse);
            //var curUser = await userResponse.Content.ReadAsAsync<DataTransfer>();

            //// get all users in db
            //HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/Users");
            //HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            //PassCookiesToClient(response);

            //if (!response.IsSuccessStatusCode)
            //{
            //    TempData["error"] = response.ReasonPhrase;
            //    return View("Error");
            //}
            //// set returned data to variable 
            //var users = await response.Content.ReadAsAsync<IEnumerable<User>>();
            //User user = users.First(u => u.Email == curUser.userName);

            //// add user id to lead
            //lead.UserId = user.UserId;

            HttpRequestMessage leadRequest = CreateRequestToService(HttpMethod.Post, "api/Leads");
            leadRequest.Content = new ObjectContent<Lead>(lead, new JsonMediaTypeFormatter());
            HttpResponseMessage leadResponse = await httpClient.SendAsync(leadRequest);
            PassCookiesToClient(leadResponse);


            return RedirectToAction("Index");
        }

        // GET: Lead/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            // get cur user 
            

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

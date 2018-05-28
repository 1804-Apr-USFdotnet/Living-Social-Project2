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
            HttpRequestMessage userRequest = CreateRequestToService(HttpMethod.Get, "api/Users/currentuser");
            HttpResponseMessage userResponse = await httpClient.SendAsync(userRequest);
            var curUser = userResponse.Content.ReadAsAsync<DataTransfer>().Result;


            // store user in session
            Session["role"] = curUser.roles[0];
            if (Session["role"] as string == "User")
            {
                HttpRequestMessage usersRequest = CreateRequestToService(HttpMethod.Get, "api/Users");
                HttpResponseMessage usersResponse = await httpClient.SendAsync(usersRequest);
                var users = await usersResponse.Content.ReadAsAsync<IEnumerable<User>>();
                Session["user_id"] = users.First(u => u.Email == curUser.userName).UserId;
            }
            else if (Session["role"] as string == "Agent")
            {
                HttpRequestMessage agentsRequest = CreateRequestToService(HttpMethod.Get, "api/RealEstateAgents");
                HttpResponseMessage agentsResponse = await httpClient.SendAsync(agentsRequest);
                var agents = await agentsResponse.Content.ReadAsAsync<IEnumerable<RealEstateAgent>>();
                Session["agent_id"] = agents.First(a => a.Email == curUser.userName).RealEstateAgentId;
            }
            // make request for leads 
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/leads");
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);


            var lead = await response.Content.ReadAsAsync<IEnumerable<Lead>>();

            return View(lead);
        }


        public async Task<ActionResult> ViewFavorites()
        {
            HttpRequestMessage userRequest = CreateRequestToService(HttpMethod.Get, "api/Users/currentuser");
            HttpResponseMessage userResponse = await httpClient.SendAsync(userRequest);
            var curUser = userResponse.Content.ReadAsAsync<DataTransfer>().Result;

            HttpRequestMessage agentsRequest = CreateRequestToService(HttpMethod.Get, "api/RealEstateAgents");
            HttpResponseMessage agentsResponse = await httpClient.SendAsync(agentsRequest);
            var agents = await agentsResponse.Content.ReadAsAsync<IEnumerable<RealEstateAgent>>();


            HttpRequestMessage request = CreateRequestToService(HttpMethod.Get, "api/Leads/Favorites");
            HttpResponseMessage response = await httpClient.SendAsync(request);
            PassCookiesToClient(response);

            int id = agents.First(a => a.Email == curUser.userName).RealEstateAgentId;


            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }

         
            var leads = await response.Content.ReadAsAsync<IEnumerable<Lead>>();
            var myleads = leads.Where(lead => lead.RealEstateAgentId == id);

            return View(myleads);
        }



        // GET: Lead/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/leads/" + id);
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataLead = response.Content.ReadAsAsync<Lead>().Result;

            return View(responseDataLead);
        }

        // GET: Lead/FavDetails/5
        public async Task<ActionResult> FavDetails(int id)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/leads/" + id);
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseDataFavLead = response.Content.ReadAsAsync<Lead>().Result;

            return View(responseDataFavLead);
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


            HttpRequestMessage leadRequest = CreateRequestToService(HttpMethod.Post, "api/Leads/x");
            leadRequest.Content = new ObjectContent<Lead>(lead, new JsonMediaTypeFormatter());
            HttpResponseMessage leadResponse = await httpClient.SendAsync(leadRequest);
            PassCookiesToClient(leadResponse);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Favorite(int id)
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
      


        [HttpPost]
        public async Task<ActionResult> Favorite(int id, Lead leadToFav)
        {
            //var httpClient = new HttpClient();

            //httpClient = new HttpClient(new HttpClientHandler() { UseCookies = false })
            //{
            //    Timeout = TimeSpan.FromMinutes(5)
            //};

            HttpRequestMessage leadRequest = CreateRequestToService(HttpMethod.Get, "api/Leads/" + id);
            HttpResponseMessage leadResponse = await httpClient.SendAsync(leadRequest);
            if (!leadResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }
            leadToFav = leadResponse.Content.ReadAsAsync<Lead>().Result;

            //get current real estate agent account
            HttpRequestMessage userRequest = CreateRequestToService(HttpMethod.Get, "api/Users/currentuser");
            HttpResponseMessage userResponse = await httpClient.SendAsync(userRequest);
            var curUser = userResponse.Content.ReadAsAsync<DataTransfer>().Result;

            //get list of agents
            HttpRequestMessage agentsRequest = CreateRequestToService(HttpMethod.Get, "api/RealEstateAgents");
            HttpResponseMessage agentsResponse = await httpClient.SendAsync(agentsRequest);
            var agents = await agentsResponse.Content.ReadAsAsync<IEnumerable<RealEstateAgent>>();

            int agentid = agents.Where(a => a.Email == curUser.userName).First().RealEstateAgentId;
            RealEstateAgent currentAgent = agents.Where(agent => agent.RealEstateAgentId == agentid).First();
            //leadToFav.RealEstateAgent = currentAgent;
            leadToFav.RealEstateAgentId = currentAgent.RealEstateAgentId;

            HttpRequestMessage favRequest = CreateRequestToService(HttpMethod.Put, "api/Leads/checkout/" + id);
            favRequest.Content = new ObjectContent<Lead>(leadToFav, new JsonMediaTypeFormatter());
            HttpResponseMessage favResponse = await httpClient.SendAsync(favRequest);
            
            if (!favResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UnFavorite(int id)
        {

            HttpRequestMessage leadRequest = CreateRequestToService(HttpMethod.Get, "api/leads/" + id);
            HttpResponseMessage leadResponse = await httpClient.SendAsync(leadRequest);
            PassCookiesToClient(leadResponse);

            var responseDataLead = leadResponse.Content.ReadAsAsync<Lead>().Result;


            HttpRequestMessage unFavRequest = CreateRequestToService(HttpMethod.Put, "api/Leads/return/" + id);
            unFavRequest.Content = new ObjectContent<Lead>(responseDataLead, new JsonMediaTypeFormatter());
            HttpResponseMessage unFavResponse = await httpClient.SendAsync(unFavRequest);

            if (!unFavResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("ViewFavorites");
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

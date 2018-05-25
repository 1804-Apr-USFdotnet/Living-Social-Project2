using RealEstateCRMConsumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealEstateCRMConsumer.Controllers
{
    public class RealEstateAgentController : AServiceController
    {   //not used
       // private static readonly HttpClient httpClient = new HttpClient(); 

        // GET: RealEstateAgent
        public async Task<ActionResult> Index()
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/RealEstateAgents");
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }

            var realestateagents = await response.Content.ReadAsAsync<IEnumerable<RealEstateAgent>>();

            return View(realestateagents);
        }

        // GET: RealEstateAgent/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/RealEstateAgents/{id}");
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
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
            
            HttpRequestMessage apiEmailRequest = CreateRequestToService(HttpMethod.Post, $"api/RealEstateAgents/emailcheck"); 
            apiEmailRequest.Content = new ObjectContent<RealEstateAgent>(realEstateAgent, new JsonMediaTypeFormatter());
            HttpResponseMessage emailResponse = await httpClient.SendAsync(apiEmailRequest);

            PassCookiesToClient(emailResponse);

            if (!emailResponse.IsSuccessStatusCode)
            {
                ViewBag.message = emailResponse.ReasonPhrase;
                return View("Create");
            }

          
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, $"api/RealEstateAgents");
            apiRequest.Content = new ObjectContent<RealEstateAgent>(realEstateAgent, new JsonMediaTypeFormatter());
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);

            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }

            Account account = new Account()
            {
                Email = realEstateAgent.Email,
                Password = realEstateAgent.Password
            };

            TempData["role"] = "agent";
            TempData["account"] = account;

            return RedirectToAction("Register", "Account");
        }

        // GET: RealEstateAgent/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
        
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/RealEstateAgents/{id}");
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            var responseDataRealEstateAgent = response.Content.ReadAsAsync<RealEstateAgent>().Result;
            ViewBag.realEstateAgentId = responseDataRealEstateAgent.RealEstateAgentId;

            return View(responseDataRealEstateAgent);
        }

        // POST: RealEstateAgent/EditAccount/5
        [HttpPost]
        public async Task<ActionResult> EditAccount(int id, RealEstateAgent realEstateAgentToEdit)
        {
            TempData["agent"] = realEstateAgentToEdit;

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/RealEstateAgents/{id}");
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);
            var oldRealEstateAgent = response.Content.ReadAsAsync<RealEstateAgent>().Result;

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            Account account = new Account()
            {
                Email = realEstateAgentToEdit.Email,
                Password = realEstateAgentToEdit.Password
            };
            HttpRequestMessage accountRequest = CreateRequestToService(HttpMethod.Post, $"api/Accounts/Edit/{oldRealEstateAgent.Email}");
            accountRequest.Content = new ObjectContent<Account>(account, new JsonMediaTypeFormatter());
            // obtain respose from API
            HttpResponseMessage newAccountResponse = await httpClient.SendAsync(accountRequest);
            PassCookiesToClient(newAccountResponse);

            if (!newAccountResponse.IsSuccessStatusCode)
            {
                TempData["error"] = newAccountResponse.ReasonPhrase;
                return View("Error");
            }

            //return RedirectToAction("Index"); old behavior
            return RedirectToAction("EditRealEstateAgent");

        }

        public async Task<ActionResult> EditRealEstateAgent()
        {
            // create request
            RealEstateAgent agent = TempData["agent"] as RealEstateAgent;
            HttpRequestMessage request = CreateRequestToService(HttpMethod.Put, $"api/RealEstateAgents/{agent.RealEstateAgentId}");
            request.Content = new ObjectContent<RealEstateAgent>(agent, new JsonMediaTypeFormatter());

            // obtain respose from API
            HttpResponseMessage response = await httpClient.SendAsync(request);
            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        // GET: RealEstateAgent/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/RealEstateAgents/{id}");
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            var responseDataRealEstateAgent = response.Content.ReadAsAsync<RealEstateAgent>().Result;

            return View(responseDataRealEstateAgent);
        }

        // POST: RealEstateAgent/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, RealEstateAgent agent)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Delete, $"api/RealEstateAgents/{id}");

            // add user object with JSON formater to request content 
            apiRequest.Content = new ObjectContent<RealEstateAgent>(agent, new JsonMediaTypeFormatter());

            // obtain respose from API
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}

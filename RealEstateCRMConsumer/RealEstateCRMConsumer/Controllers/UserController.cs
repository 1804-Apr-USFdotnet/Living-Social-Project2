using RealEstateCRMConsumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Text;
using System.Net.Http.Formatting;

namespace RealEstateCRMConsumer.Controllers
{
    public class UserController : AServiceController
    {
        // GET: Users
        public async Task<ActionResult> Index()
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/Users");


            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            //IdentityExtensions.GetUserName()
            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }

            var users = await response.Content.ReadAsAsync<IEnumerable<User>>();

            return View(users);
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int id)
        {

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Users/{id}");
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            
            var responseUser = response.Content.ReadAsAsync<User>().Result;

            return View(responseUser);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View(new User());
        }

        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            // create request
            HttpRequestMessage apiEmailRequest = CreateRequestToService(HttpMethod.Post, $"api/Users/emailcheck");

            // add user object with JSON formater to request content 
            apiEmailRequest.Content = new ObjectContent<User>(user, new JsonMediaTypeFormatter());

            // obtain respose from API
            HttpResponseMessage emailResponse = await httpClient.SendAsync(apiEmailRequest);
            PassCookiesToClient(emailResponse);


            if (!emailResponse.IsSuccessStatusCode)
            {
                ViewBag.message = emailResponse.ReasonPhrase;
                return View("Create");
            }

            // create request
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, $"api/Users");

            // add User object to request
            apiRequest.Content = new ObjectContent<User>(user, new JsonMediaTypeFormatter());
            
            // send User to API to be added to database 
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }

            // create account based on user to be added to DataDb
            Account account = new Account()
            {
                Email = user.Email,
                Password = user.Password
            };

            // save account to Temp Data to be passed to register action in account controller 
            TempData["account"] = account;

            return RedirectToAction("Register", "Account");
        }

        // GET: User/Edit/5
        // render edit template
        public async Task<ActionResult> Edit(int id)
        {
            // Use "details" action to return user object 
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Users/{id}");
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }

            var responseUser = response.Content.ReadAsAsync<User>().Result;

            return View(responseUser);
        }

        // Edit
        [HttpPost]
        public async Task<ActionResult> Edit(int id, User user)
        {
            // create request
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Put, $"api/Users/{id}");

            // add user object with JSON formater to request content 
            apiRequest.Content = new ObjectContent<User>(user, new JsonMediaTypeFormatter());

            // obtain respose from API
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            //HttpResponseMessage response = await httpClient.PutAsJsonAsync("http://localhost:57955/api/Users/" + id, user);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        // GET: Lead/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            // create request
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, $"api/Users/{id}");

            // wait for response 
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);

            //HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Users/" + id);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            var responseUser = response.Content.ReadAsAsync<User>().Result;

            //var buyerLeadToDelete = JsonConvert.DeserializeObject<BuyerLead>(responseData);

            return View(responseUser);
        }

        // POST: User/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, User user)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Delete, $"api/Users/{id}");

            // add user object with JSON formater to request content 
            apiRequest.Content = new ObjectContent<User>(user, new JsonMediaTypeFormatter());

            // obtain respose from API
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);
            PassCookiesToClient(response);
            //HttpResponseMessage response = await httpClient.DeleteAsync("http://localhost:57955/api/Users/" + id);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            return RedirectToAction("Index");
        }

       
    }
}
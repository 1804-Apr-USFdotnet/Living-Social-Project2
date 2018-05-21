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
    public class UserController : Controller
    {
        private static readonly HttpClient httpClient = new HttpClient();
        // GET: Users
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Users");

             if(!response.IsSuccessStatusCode)
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
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Users/" + id);
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
            // postAsync = async post message

            HttpResponseMessage emailResponse = await httpClient.PostAsJsonAsync("http://localhost:57955/api/Users/emailcheck", user);

            if (!emailResponse.IsSuccessStatusCode)
            {
                ViewBag.message = emailResponse.ReasonPhrase;
                return View("Create");
            }

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:57955/api/Users", user);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            Account account = new Account()
            {
                Email = user.Email,
                Password = user.Password
            };

            TempData["account"] = account;

            // create an account you can pass to the register method 
            


            return RedirectToAction("Register", "Account");
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Users/" + id);
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
            // put Async = async put message
            HttpResponseMessage response = await httpClient.PutAsJsonAsync("http://localhost:57955/api/Users/" + id, user);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        // GET: Lead/Delete/5
        //[HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Users/" + id);
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
            HttpResponseMessage response = await httpClient.DeleteAsync("http://localhost:57955/api/Users/" + id);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            return RedirectToAction("Index");
        }

       
    }
}
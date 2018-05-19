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

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var users = await response.Content.ReadAsAsync<IEnumerable<User>>();

            return View(users);
        }

        public ActionResult Create()
        {
            return View(new User());
        }

        // Create
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            // postAsync = async post message
            HttpResponseMessage response = await httpClient.PutAsJsonAsync("http://localhost:57955/api/Users", user);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Users/" + id);
            if (!response.IsSuccessStatusCode)
            {
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
                return View("Error");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Users/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            var responseUser = response.Content.ReadAsAsync<User>().Result;

            //var buyerLeadToDelete = JsonConvert.DeserializeObject<BuyerLead>(responseData);

            return View(responseUser);
        }

        // Delete
        [HttpPost]
        public async Task<ActionResult> Delete(int id, User user)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync("http://localhost:57955/api/Users/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}
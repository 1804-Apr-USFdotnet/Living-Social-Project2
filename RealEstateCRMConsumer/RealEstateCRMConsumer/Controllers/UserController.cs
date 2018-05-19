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

<<<<<<< HEAD
        // GET: User/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Users/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }
            
            var responseUser = response.Content.ReadAsAsync<User>().Result;

            return View(responseUser);
        }

        // GET: User/Create
=======
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
        public ActionResult Create()
        {
            return View(new User());
        }

<<<<<<< HEAD
        // POST: User/Create
=======
        // Create
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            // postAsync = async post message
<<<<<<< HEAD
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:57955/api/Users", user);
=======
            HttpResponseMessage response = await httpClient.PutAsJsonAsync("http://localhost:57955/api/Users", user);
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

<<<<<<< HEAD
        // GET: User/Edit/5
=======
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
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
<<<<<<< HEAD

        // GET: Lead/Delete/5
        //[HttpGet]
=======
        [HttpGet]
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
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

<<<<<<< HEAD
        // POST: User/Delete/5
=======
        // Delete
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
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
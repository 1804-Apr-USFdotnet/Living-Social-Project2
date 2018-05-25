using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RealEstateCRMConsumer.Models;

namespace RealEstateCRMConsumer.Controllers
{
    public class AccountController : AServiceController
    {

        // GET: Account
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            Account account = TempData["account"] as Account;
            string role = TempData["role"] as String;
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:57955/api/Accounts/Register/" +role, account);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            PassCookiesToClient(response);

            return RedirectToAction("Index", "Lead");
        }

        public ActionResult Create()
        {
            return View(new Account());
        }

        [HttpPost]
        public async Task<ActionResult> Login(Account account)
            {

            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, "api/Accounts/Login");
            apiRequest.Content = new ObjectContent<Account>(account, new JsonMediaTypeFormatter());
            HttpResponseMessage  response = await httpClient.SendAsync(apiRequest);
            
            PassCookiesToClient(response);

            



            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            // set session role for View 


            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Account account)
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Post, "api/Accounts/Edit");
            apiRequest.Content = new ObjectContent<Account>(account, new JsonMediaTypeFormatter());
            HttpResponseMessage response = await httpClient.SendAsync(apiRequest);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }

            return RedirectToAction("Index", "User");
        }


        public async Task<ActionResult> Logout()
        {

            HttpContext.Session.Remove("currentUser");
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Accounts/Logout");
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }

            PassCookiesToClient(response);
            // delete session
            Session.Abandon();

            return RedirectToAction("Create", "Account");
            
        }

        
    }
}
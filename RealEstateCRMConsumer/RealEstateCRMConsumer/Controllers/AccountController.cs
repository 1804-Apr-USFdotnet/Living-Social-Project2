﻿using System;
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
            Session["role"] = role;
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/api/Accounts/Register/" + role, account);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            PassCookiesToClient(response);

        //     if (role == "user")
        //     {
        //         return RedirectToAction("Index", "User");
        //     } else if (role == "agent") { 
            
        //         return RedirectToAction("Index", "RealEstateAgent");
        //     } else {
        //         return View("role " + role + " not found");
        // }
            return RedirectToAction("Index", "Lead");
        }

        [HttpGet]
        public async Task<ActionResult> RegisterAgent()
        {
            Account account = TempData["account"] as Account;
            string role = TempData["role"] as String;
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/api/Accounts/Register" + role, account);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            PassCookiesToClient(response);



            return RedirectToAction("Index", "RealEstateAgent");
            
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

            

            return RedirectToAction("Index", "Lead");
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
            Session.Clear();
            HttpResponseMessage response = await httpClient.GetAsync("http://ec2-13-58-19-141.us-east-2.compute.amazonaws.com/realestateapi/api/Accounts/Logout");
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }

            PassCookiesToClient(response);


            return RedirectToAction("Create", "Account");
        }

        
    }
}
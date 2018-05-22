using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:57955/api/Accounts/Register", account);
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            PassCookiesToClient(response);



            return RedirectToAction("Index", "User");
        }

        public ActionResult Create()
        {
            return View(new Account());
        }

        [HttpPost]
        public async Task<ActionResult> Login(Account account)
            {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:57955/api/Accounts/Login", account);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }
            PassCookiesToClient(response);


            return RedirectToAction("Index", "User");
        }
    

        public async Task<ActionResult> Logout()
        {
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57955/api/Accounts/Logout");
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = response.ReasonPhrase;
                return View("Error");
            }

            PassCookiesToClient(response);

            return RedirectToAction("Index", "User");
        }

        private bool PassCookiesToClient(HttpResponseMessage apiResponse)
        {
            if (apiResponse.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                foreach (string value in values)
                {
                    Response.Headers.Add("Set-Cookie", value);
                }
                return true;
            }
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity.EntityFramework;
using RealEstateCRM.DataAccessLayer;
using RealEstateCRM.DataAccessLayer.Repositories;
using RealEstateCRM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace RealEstateCRM.API.Controllers
{
    public class AccountController : ApiController
    {
        [AllowAnonymous]
        [ResponseType(typeof(Account))]
        [HttpPost]
        [Route("~/api/Accounts/Register")]
        public IHttpActionResult RegisterUser(Account newAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // register
            var userStore = new UserStore<IdentityUser>(new DataDbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser(newAccount.Email);

            if (userManager.Users.Any(u => u.Email == newAccount.Email))
            {
                return BadRequest();
            }

            userManager.Create(user, newAccount.Password);
            return Ok("registered account");
        }




        [HttpPost]
        [ResponseType(typeof(Account))]
        [Route("~/api/Accounts/Login")]
        [AllowAnonymous]
        public IHttpActionResult LogIn(Account loginAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model state not valid");
            }

            // actually login
            var userStore = new UserStore<IdentityUser>(new DataDbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Users.FirstOrDefault(u => u.Email == loginAccount.Email);

            if (user == null)
            {
                return BadRequest();
            }

            if (!userManager.CheckPassword(user, loginAccount.Password))
            {
                return Unauthorized();
            }

            var authManager = Request.GetOwinContext().Authentication;
            var claimsIdentity = userManager.CreateIdentity(user, "ApplicationCookie");

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claimsIdentity);

            return Ok();
        }
    }
}

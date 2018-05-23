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
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    public class AccountsController : ApiController
    {
        [AllowAnonymous]
        [ResponseType(typeof(Account))]
        [HttpPost]
        [Route("~/api/Accounts/Register")]
        public IHttpActionResult RegisterUser(Account newAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model is invalid");
            }

            // register
            var userStore = new UserStore<IdentityUser>(new DataDbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser(newAccount.Email);

            if (userManager.Users.Any(u => u.Email == newAccount.Email))
            {
                return BadRequest("email is already taken");
            }

            userManager.Create(user, newAccount.Password);
            var authManager = Request.GetOwinContext().Authentication;
            var claimsIdentity = userManager.CreateIdentity(user, WebApiConfig.AuthenticationType);

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claimsIdentity);
            return Ok("registered account and logged in");
        }

        [HttpPost]
        [Route("~/api/Accounts/RegisterAdmin")]
        [AllowAnonymous]

        public IHttpActionResult RegisterAdmin(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is invalid");
            }
            // actually register
            var userStore = new UserStore<IdentityUser>(new DataDbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser(account.Email);

            if (userManager.Users.Any(u => u.UserName == account.Email))
            {
                return BadRequest("email is already in user manager");
            }

            userManager.Create(user, account.Password);
            var authManager = Request.GetOwinContext().Authentication;
            var claimsIdentity = userManager.CreateIdentity(user, WebApiConfig.AuthenticationType);

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claimsIdentity);

            // the only difference from Register action
            userManager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "admin"));

            return Ok();

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


            // Test that request has cookie
            Request.Headers.GetCookies();
            // actually login
            var userStore = new UserStore<IdentityUser>(new DataDbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Users.FirstOrDefault(u => u.UserName == loginAccount.Email);

            if (user == null)
            {
                return BadRequest();
            }

            if (!userManager.CheckPassword(user, loginAccount.Password))
            {
                return Unauthorized();
            }
            

            var authManager = Request.GetOwinContext().Authentication;
            var claimsIdentity = userManager.CreateIdentity(user, WebApiConfig.AuthenticationType);

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claimsIdentity);

            return Ok();

        }

      

        [HttpPost]
        [ResponseType(typeof(Account))]
        [Route("~/api/Accounts/Edit/{email}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Edit([FromBody] Account EditAccount, string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model state not valid");
            }

            var userStore = new UserStore<IdentityUser>(new DataDbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Users.FirstOrDefault(u => u.UserName == email);

            if (user == null)
            {
                return BadRequest("no user found");
            }
            if (userManager.HasPassword(user.Id))
            {

                userManager.RemovePassword(user.Id);
            }
            var newPasswordHash = userManager.PasswordHasher.HashPassword(EditAccount.Password);

            // get user and edit properties
            await userStore.SetPasswordHashAsync(user, newPasswordHash);
            await userManager.UpdateAsync(user);
            user.UserName = EditAccount.Email;
            userStore.Context.SaveChanges();


            return Ok();

        }

   
        

        public IHttpActionResult DeleteUser(Account deleteAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model is invalid");
            }

            // find user
            var userStore = new UserStore<IdentityUser>(new DataDbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser(deleteAccount.Email);

            if (!userManager.Users.Any(u => u.Email == deleteAccount.Email))
            {
                return BadRequest("user does not exist");
            }


            // delete user
            userManager.Delete(user);

            return Ok("deleted account and logged out");
        }

        [HttpGet]
        [Route("~/api/Accounts/Logout")]
        public IHttpActionResult Logout()
        {

            Request.GetOwinContext().Authentication.SignOut(WebApiConfig.AuthenticationType);
            return Ok("signed out");
        }

       
    }
}

    


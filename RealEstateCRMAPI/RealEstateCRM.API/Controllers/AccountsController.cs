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
        [Route("~/api/Accounts/Register/{role}")]
        public IHttpActionResult RegisterUser(Account newAccount, string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model is invalid");
            }

            // register
            var userStore = new UserStore<IdentityUser>(new DataDbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser(newAccount.Email);
            // create the user 
            userManager.Create(user, newAccount.Password);


            if (userManager.Users.Any(u => u.Email == newAccount.Email))
            {
                return BadRequest("email is already taken");
            }

            if (userManager.Users.Any(u => u.Email == newAccount.Email))
            {
                return BadRequest("email is already taken");
            }

            // assign role
            if (role.ToLower() == "user")
            {
                userManager.AddToRole(user.Id, "user");
            }
            else if (role.ToLower() == "agent")
            {
                userManager.AddToRole(user.Id, "agent");

            }
            else if (role.ToLower() == "admin")
            {
                userManager.AddToRole(user.Id, "admin");

            }
            else
            {
                return BadRequest("no role assigned");
            }

            var authManager = Request.GetOwinContext().Authentication;
            var claimsIdentity = userManager.CreateIdentity(user, WebApiConfig.AuthenticationType);

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claimsIdentity);
            return Ok("registered account and logged in");
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


            // Find user in user manager database 
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
            
            // authenticate user and sign in 
            var authManager = Request.GetOwinContext().Authentication;
            var claimsIdentity = userManager.CreateIdentity(user, WebApiConfig.AuthenticationType);

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claimsIdentity);

            return Ok();

        }
        //[HttpGet]
        //[Route("api/Accounts/Seed")]
        //public void Seed()
        //{
        //    DataDbContext context = new DataDbContext();
        //    if (!context.Roles.Any(r => r.Name == "Admin"))
        //    {
        //        var store = new RoleStore<IdentityRole>(context);
        //        var manager = new RoleManager<IdentityRole>(store);
        //        var role = new IdentityRole { Name = "Admin" };

        //        manager.Create(role);
        //    }
        //    if (!context.Roles.Any(r => r.Name == "User"))
        //    {
        //        var store = new RoleStore<IdentityRole>(context);
        //        var manager = new RoleManager<IdentityRole>(store);
        //        var role = new IdentityRole { Name = "User" };

        //        manager.Create(role);
        //    }
        //    if (!context.Roles.Any(r => r.Name == "Agent"))
        //    {
        //        var store = new RoleStore<IdentityRole>(context);
        //        var manager = new RoleManager<IdentityRole>(store);
        //        var role = new IdentityRole { Name = "Agent" };

        //        manager.Create(role);
        //    }
        //}




        [HttpPost]
        [ResponseType(typeof(Account))]
        [Route("~/api/Accounts/Edit/{email}")]
        [AllowAnonymous]
        // Edit account 
        // Pass in account object in request body and email string in request url 
        public async Task<IHttpActionResult> Edit([FromBody] Account EditAccount, string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model state not valid");
            }

            // Find user (account) in user manager 
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
            // convert password to hash 
            var newPasswordHash = userManager.PasswordHasher.HashPassword(EditAccount.Password);

            // update user (account) and save changes
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
        // create role if it does not exist in db
        

    }
}

    


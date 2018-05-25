using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace RealEstateCRM.API.Controllers
{
    public class DataController : ApiController
    {
        public IHttpActionResult Get()

        {

            // making use of global authorize filter in webapiconfig / filterconfig

            // get the currently logged-in 
            var user = Request.GetOwinContext().Authentication.User;
            if(!user.Identity.IsAuthenticated)
            {
                return Ok("There is no user logged in");
            }

            // get his username
            string name = user.Identity.Name;

            // get whether user has some role
            bool isAdmin = user.IsInRole("admin");

            // get all user's roles
            List<string> roles = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value.ToString()).ToList();
          
            string message = $"Authenticated {name}, with roles: {string.Join(", ", roles)}!";
            return Ok(message);

        }
    }
}

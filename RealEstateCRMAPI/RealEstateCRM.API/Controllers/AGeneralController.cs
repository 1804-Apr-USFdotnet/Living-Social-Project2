using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RealEstateCRM.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RealEstateCRM.API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AGeneralController : ApiController
    {
      
        [AllowAnonymous]
        public async Task<DataTransfer> GetCurrentUserInfo()
        {
            var userStore = new UserStore<IdentityUser>(new DataDbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            string name = HttpContext.Current?.User?.Identity?.GetUserName();




            var user = userManager.Users.FirstOrDefault(u => u.UserName == name);
            var currentRoles = await userManager.GetRolesAsync(user.Id);
            DataTransfer info = new DataTransfer()
            {
                userName = name,
                roles = currentRoles
            };
            return info;
            

        }
    }
}

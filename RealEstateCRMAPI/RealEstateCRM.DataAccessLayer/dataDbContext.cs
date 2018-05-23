using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RealEstateCRM.DataAccessLayer
{
    public class DataDbContext : IdentityDbContext<IdentityUser>
    {
        public DataDbContext() : base("DataDb")
        {
            
        }
    }
}

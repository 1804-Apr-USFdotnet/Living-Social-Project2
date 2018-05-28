using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccountDb
{
    public class DataDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Account> Accounts { get; set; }


        public DataDbContext() : base("DataDb")
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RealEstateCRM.Models
{
    public class RealEstateCRMContext : DbContext
    {
		//public DbSet<Lead> Leads { get; set; }
		public DbSet<User> Users { get; set; }
        public DbSet<BuyerLead> BuyerLeads { get; set; }
        public DbSet<SellerLead> SellerLeads { get; set; }
		public DbSet<RealEstateAgent> RealEstateAgents { get; set; }

    }

}

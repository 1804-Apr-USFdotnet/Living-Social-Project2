﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateCRM.Models
{
    public class RealEstateAgent : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RealEstateAgentId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Alias { get; set; }

        //public virtual List<Lead> Leads { get; set; }
       


        public virtual List<SellerLead> SellerLeads { get; set; }
        public virtual List<BuyerLead> BuyerLeads { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
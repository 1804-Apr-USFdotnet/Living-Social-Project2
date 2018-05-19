using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateCRMConsumer.Models
{
    public class Lead : IEntity
    {
        public int LeadId { get; set; }

        //buyer or seller, may be separated into SellerLead and BuyerLead by type
        [Required]
        public string LeadType { get; set; }

        [Required]
        public string LeadName { get; set; }
        public Boolean PriorApproval { get; set; }

        public int? Min { get; set; }
        public int? Max { get; set; }
        public int? Bed { get; set; }
        public int? Bath { get; set; }
        public int? SqFootage { get; set; }
        public int? Floors { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zipcode { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public virtual RealEstateAgent RealEstateAgent { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
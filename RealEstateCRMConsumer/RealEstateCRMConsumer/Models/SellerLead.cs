﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateCRMConsumer.Models
{
    public class SellerLead : IEntity
    {
        [Column("Id")]
        public int SellerLeadId { get; set; }


        //public string Type { get; set; }

        public string LeadName { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Bed { get; set; }
        public int Bath { get; set; }
        public int SqFootage { get; set; }
        public int Floors { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public virtual RealEstateAgent RealEstateAgent { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

    }
}
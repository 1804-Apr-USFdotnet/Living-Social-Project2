using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Models
{
    public class BuyerLead
    {
        public int BuyerLeadId { get; set; }

        //buyer or seller, may be separated into SellerLead and BuyerLead
        public string Type { get; set; }

        public string LeadName { get; set; }
        public Boolean priorApproval { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Bed { get; set; }
        public int Bath { get; set; }
        public int SqFootage { get; set; }
        public int Floors { get; set; }

        public virtual RealEstateAgent RealEstateAgent { get; set; }
    }
}

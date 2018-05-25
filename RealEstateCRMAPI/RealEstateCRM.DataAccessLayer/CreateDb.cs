using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateCRM.Models;

namespace RealEstateCRM.DataAccessLayer
{
    class CreateDb
    {
        static void Main(string[] args)
        {
            Lead testLead = new Lead
            {
                LeadType = "Seller",
                LeadName = "firstname lastname",
                PriorApproval = true,
                
                Min = 200,
                Max = 800,
                Bed = 1,
                Bath = 1,
                SqFootage = 1500,
                Floors = 1,

                PhoneNumber = "555-555-5555",
                Email = "testlead@testleads.com",

                Address = "1283 Red Rd",
                City = "Tampa",
                State = "FL",
                Zipcode = 11223
               
            };

            Console.WriteLine("Creating DB...............");
            RealEstateCRMContext crmDB = new RealEstateCRMContext();
            Console.WriteLine("DB Created..............");

            //block comment ctrl+K, ctrl+C
            //block uncomment ctrl+K, ctrl+U

            #region AddTestLead
            crmDB.Leads.Add(testLead);
            crmDB.SaveChanges();
            Console.WriteLine("Db Changes Saved..............");
            #endregion



            #region UpdateBuyerLead
            //var editLead = crmDB.BuyerLeads.Where(x => x.BuyerLeadId == 1).FirstOrDefault();
            //editLead.LeadName = "Replaced Name";
            //crmDB.Entry<BuyerLead>(editLead).State = System.Data.Entity.EntityState.Modified;
            //crmDB.SaveChanges();
            //Console.WriteLine("Lead Updated.......");
            #endregion
        }
    }
}

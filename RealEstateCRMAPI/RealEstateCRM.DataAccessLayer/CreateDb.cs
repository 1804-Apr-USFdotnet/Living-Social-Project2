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
            /*Lead testLead = new Lead
            {
                LeadType = "Seller",
                LeadName = "Marcus",
                PriorApproval = false,
                
                Min = 100,
                Max = 900,
                Bed = 2,
                Bath = 3,
                SqFootage = 3000,
                Floors = 2,

                PhoneNumber = "555-555-5555",
                Email = "testlead4@testleads.com",

                Address = "6847 Indigo Lane",
                City = "New Orleans",
                State = "LA",
                Zipcode = 77425
               
            };*/
            /*RealEstateAgent newAgent = new RealEstateAgent
            {
                FirstName = "Irene",
                LastName = "Wise",
                Email = "anything@work.org",
                Password = "password1",
                Alias = "water"
            };*/
            User newUser = new User
            {
                Email = "thismail@thisaddress.edu",
                Password = "password4",
                Alias = "User4"
            };
            Console.WriteLine("Creating DB...............");
            //RealEstateCRMContext crmDB = new RealEstateCRMContext();
            TestCRMContext  testCRM = new TestCRMContext();
            Console.WriteLine("DB Created..............");

            //block comment ctrl+K, ctrl+C
            //block uncomment ctrl+K, ctrl+U

            #region AddTestLead
            //testCRM.Leads.Add(testLead);
            testCRM.Users.Add(newUser);
            testCRM.SaveChanges();
            //crmDB.Leads.Add(testLead);
            //crmDB.SaveChanges();
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

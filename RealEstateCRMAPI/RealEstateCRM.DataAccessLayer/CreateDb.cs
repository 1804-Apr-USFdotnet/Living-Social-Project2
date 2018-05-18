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
            BuyerLead testBuyerLead = new BuyerLead
            {
                LeadName = "firstname lastname",
                PriorApproval = true,
                Min = 200,
                Max = 800,
                Bed = 1,
                Bath = 1,
                SqFootage = 1500,
                Floors = 1
               
            };

            Console.WriteLine("Creating DB...............");
            RealEstateCRMContext crmDB = new RealEstateCRMContext();
            Console.WriteLine("DB Created..............");

            //block comment ctrl+K, ctrl+C
            //block uncomment ctrl+K, ctrl+U

            #region AddTestBuyerLead
            crmDB.BuyerLeads.Add(testBuyerLead);
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

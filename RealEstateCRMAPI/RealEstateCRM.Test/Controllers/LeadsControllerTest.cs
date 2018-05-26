using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstateCRM.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RealEstateCRM.DataAccessLayer;
using RealEstateCRM.DataAccessLayer.Repositories;
using RealEstateCRM.Models;

namespace RealEstateCRM.API.Controllers.Test
{
    [TestClass()]
    public class LeadsControllerTest
    {
        
        IRepository<Lead> leadCrud;
        IDbContext testDbContext;

        [TestMethod()]
        public void LeadsControllerInitialTest()
        {
            testDbContext = new TestCRMContext();
            leadCrud = new CRUD<Lead>(testDbContext);
            Assert.IsNotNull(leadCrud);
        }

        public void LeadsControllerInitializer()
        {
            testDbContext = new TestCRMContext();
            leadCrud = new CRUD<Lead>(testDbContext);
        }

        [TestMethod()]
        public void GetLeadsTest()
        {
            LeadsControllerInitializer();
            IQueryable<Lead> leads = leadCrud.Table;
            Assert.IsNotNull(leads);
        }

        [TestMethod()]
        public void GetLeadTest()
        {
            LeadsControllerInitializer();
            int id = 5;
            Lead lead = leadCrud.GetByID(id);
            string expected = "Fred";
            string actual= lead.LeadName;
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void PutLeadTest()
        {
            LeadsControllerInitializer();
            int id = 5;
            Random rnd = new Random();
            Lead oldLead = leadCrud.GetByID(id);
            oldLead.Min = rnd.Next(100,400);
            oldLead.Max = rnd.Next(500, 900);
            leadCrud.Update(oldLead);
            int expected = (int)oldLead.Min;
            int actual = (int)leadCrud.GetByID(id).Min;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PostLeadTest()
        {
            LeadsControllerInitializer();
            int leadIndex = leadCrud.Table.Count();
            Lead newLead = new Lead { LeadType = "Seller", LeadName = "Annie",
                PhoneNumber = "614-223-8479", Email = "newmail@this.com" };
            leadCrud.Insert(newLead);
            int expected = leadIndex + 1;
            int actual = leadCrud.Table.Count();
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void DeleteLeadTest()
        {
            LeadsControllerInitializer();
            int expected = leadCrud.Table.Count();
            Lead newLead = new Lead
            {
                LeadType = "Seller",
                LeadName = "Marcus",
                PhoneNumber = "225-845-9326",
                Email = "newmail@this.com"
            };
            leadCrud.Insert(newLead);
            Lead lastLead = leadCrud.Table.First(num => num.PhoneNumber == newLead.PhoneNumber);
            leadCrud.Delete(lastLead);
            int actual = leadCrud.Table.Count();
            Assert.AreEqual(expected,actual);
        }
    }
}
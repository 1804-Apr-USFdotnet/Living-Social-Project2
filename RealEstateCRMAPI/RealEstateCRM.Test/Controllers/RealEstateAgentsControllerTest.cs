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
    public class RealEstateAgentsControllerTest
    {
        IRepository<RealEstateAgent> agentCrud;
        IDbContext testDbContext;

        [TestMethod()]
        public void RealEstateAgentsControllerInitialTest()
        {
            testDbContext = new TestCRMContext();
            agentCrud = new CRUD<RealEstateAgent>(testDbContext);
            Assert.IsNotNull(agentCrud);
        }
        public void RealEstateAgentsControllerInitializer()
        {
            testDbContext = new TestCRMContext();
            agentCrud = new CRUD<RealEstateAgent>(testDbContext);
        }

        [TestMethod()]
        public void GetRealEstateAgentsTest()
        {
            RealEstateAgentsControllerInitializer();
            IQueryable<RealEstateAgent> agents = agentCrud.Table;
            Assert.IsNotNull(agents);
        }

        [TestMethod()]
        public void GetRealEstateAgentTest()
        {
            RealEstateAgentsControllerInitializer();
            int id = 2;
            RealEstateAgent currentAgent = agentCrud.GetByID(id);
            string expected = "Hugo";
            string actual = currentAgent.FirstName;
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void PutRealEstateAgentTest()
        {
            RealEstateAgentsControllerInitializer();
            int id = 5;
            Random rnd = new Random();
            RealEstateAgent currAgent = agentCrud.GetByID(id);
            currAgent.Password = rnd.Next(10000, 99999).ToString();
            agentCrud.Update(currAgent);
            string expected = currAgent.Password;
            string actual = agentCrud.GetByID(id).Password;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PostRealEstateAgentTest()
        {
            RealEstateAgentsControllerInitializer();
            int agentIndex = agentCrud.Table.Count();
            RealEstateAgent newAgent = new RealEstateAgent
            {
                FirstName = "Allison",
                LastName = "Ross",
                Email = "random@mail.net",
                Password = "password2",
                Alias = "TestDb"
            };
            agentCrud.Insert(newAgent);
            int actual = agentCrud.Table.Count();
            int expected = agentIndex + 1;
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void DeleteRealEstateAgentTest()
        {
            RealEstateAgentsControllerInitializer();
            int expected = agentCrud.Table.Count();
            RealEstateAgent newAgent = new RealEstateAgent
            {
                FirstName = "Allison",
                LastName = "Ross",
                Email = "random@mail.net",
                Password = "password2",
                Alias = "TestDb"
            };
            agentCrud.Insert(newAgent);
            RealEstateAgent lastAgent = agentCrud.Table.First(x => x.Alias == newAgent.Alias);
            agentCrud.Delete(lastAgent);
            int actual = agentCrud.Table.Count();
            Assert.AreEqual(expected,actual);
        }
    }
}
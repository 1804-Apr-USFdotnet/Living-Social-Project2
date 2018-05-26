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
    public class UsersControllerTest
    {
        IRepository<User> crud;
        IDbContext testDbContext;

        [TestMethod()]
        public void UsersControllerInitialTest()
        {
            testDbContext = new TestCRMContext();
            crud = new CRUD<User>(testDbContext);
            Assert.IsNotNull(crud);
        }

        public void UsersControllerInitializer()
        {
            testDbContext = new TestCRMContext();
            crud = new CRUD<User>(testDbContext);
        }

        [TestMethod()]
        public void GetAllUsersTest()
        {
            UsersControllerInitializer();
            IQueryable<User> allUsers = crud.Table;
            Assert.IsNotNull(allUsers);
        }

        [TestMethod()]
        public void GetUserTest()
        {
            UsersControllerInitializer();
            int id = 4;
            User user = crud.GetByID(id);
            string expected = "thismail@thisaddress.edu";
            string actual = user.Email;
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void PostUserTest()
        {
            UsersControllerInitializer();
            int userIndex = crud.Table.Count();
            User newUser = new User { Email = "notanother@website.com", Password = "password5",
                Alias = "User5" };
            crud.Insert(newUser);
            int expected = userIndex + 1;
            int actual = crud.Table.Count();
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void PutUserTest()
        {
            UsersControllerInitializer();
            int id = 3;
            Random rnd = new Random();
            User oldUser = crud.GetByID(id);
            oldUser.Alias = rnd.Next(10000, 99999).ToString();
            string expected = oldUser.Alias;
            crud.Update(oldUser);
            string actual = crud.GetByID(id).Alias;
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            UsersControllerInitializer();
            int expected = crud.Table.Count();
            User newUser = new User { Email = "testMail@testdb.org", Password = "testing",
                Alias = "Test" };
            crud.Insert(newUser);
            User tempUser = crud.Table.First(u => u.Alias == newUser.Alias);
            crud.Delete(tempUser);
            int actual = crud.Table.Count();
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void UserExistsTest()
        {
            UsersControllerInitializer();
            int id = 2;
            int count = crud.Table.ToList().Count(u => u.UserId == id);
            Assert.IsNotNull(count);
        }
    }
}
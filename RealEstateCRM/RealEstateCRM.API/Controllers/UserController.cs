using RealEstateCRM.DataAccessLayer;
using RealEstateCRM.DataAccessLayer.Repositories;
using RealEstateCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace RealEstateCRM.API.Controllers
{
    public class UserController : ApiController
    {
        IRepository<User> crud;
        IDbContext realEstateDb;

        public UserController()
        {
            realEstateDb = new RealEstateCRMContext();
            crud = new CRUD<User>(realEstateDb);

        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> allUsers = crud.Table;
            return allUsers;
        }

        public IHttpActionResult GetUserById(int id)
        {
            var user = crud.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        public IHttpActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                crud.Insert(user);
                return Ok();
            }
            else
            {
                 
            }
        }
    }
}

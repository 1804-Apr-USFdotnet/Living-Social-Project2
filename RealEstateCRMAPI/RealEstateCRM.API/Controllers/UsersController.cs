using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RealEstateCRM.DataAccessLayer;
using RealEstateCRM.DataAccessLayer.Repositories;
using RealEstateCRM.Models;

namespace RealEstateCRM.API.Controllers
{
    public class UsersController : ApiController
    {
        IRepository<User> crud;
        IRepository<SellerLead> sellerCrud;
        IRepository<BuyerLead> buyerCrud;
        IDbContext realEstateDb;

        public UsersController()
        {
            realEstateDb = new RealEstateCRMContext();
            crud = new CRUD<User>(realEstateDb);

        }

        [ResponseType(typeof(User))]
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> allUsers = crud.Table;
            return allUsers;
        }

        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            var user = crud.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                crud.Insert(user);
                return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
            }
            else
            {
                return BadRequest();
            }
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            crud.Update(user);

            try
            {

                //db.SaveChanges();
                crud.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            if(crud.GetByID(id) == null)
            {
                return NotFound();
            }
            else
            {
                User user = crud.GetByID(id);
                crud.Delete(user);
                return Ok();
            }
        }

        public bool UserExists(int id)
        {
            return crud.Table.ToList().Count(u => u.UserId == id) > 0;
        }
    }
}

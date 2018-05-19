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
        IDbContext realEstateDb;

        public UsersController()
        {
            realEstateDb = new RealEstateCRMContext();
            crud = new CRUD<User>(realEstateDb);

        }

<<<<<<< HEAD
        // GET: api/Users

        public IHttpActionResult GetAllUsers()
=======
        [ResponseType(typeof(User))]
        public IEnumerable<User> GetAllUsers()
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
        {
            

            try
            {
                IQueryable<User> allUsers = crud.Table;
                return Ok(allUsers);
            }
            catch
            {
                return InternalServerError();
            }

        }

<<<<<<< HEAD
        // GET: api/Users/5
=======
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
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

<<<<<<< HEAD
        // POST: api/Users
=======
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {

            if (!ModelState.IsValid)
            {
<<<<<<< HEAD
                return BadRequest(ModelState);
=======
                crud.Insert(user);
                return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
            }



            crud.Insert(user);

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // PUT: api/Users/5
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

<<<<<<< HEAD
        // DELETE: api/Users/5
=======
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

>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            if (crud.GetByID(id) == null)
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

<<<<<<< HEAD
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                crud.Dispose();
            }
            base.Dispose(disposing);
        }

=======
>>>>>>> 18697f707ead2c485665d8c610197eea8ba66f0a
        public bool UserExists(int id)
        {
            return crud.Table.ToList().Count(u => u.UserId == id) > 0;
        }
    }
}

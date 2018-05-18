using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> allUsers = crud.Table;
            return allUsers;
        }

        public IHttpActionResult GetUser(int id)
        {
            var user = crud.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        public IHttpActionResult PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                crud.Insert(user);
                return CreatedAtRoute("shared/Index", new { id = user.UserId }, user);
            }
            else
            {
                return BadRequest();
            }
        }

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

        //public IHttpActionResult GetAllLeads(int user_id)
        //{
        //    IQueryable<BuyerLead> userBuyerLeads = buyerCrud.Table.Where(l => l.UserId == user_id);
        //    IQueryable<SellerLead> userSellerLeads = sellerCrud.Table.Where(l => l.UserId == user_id);
        //    var allLeads = userBuyerLeads.Union(userSellerLeads);
        //}
    }
}

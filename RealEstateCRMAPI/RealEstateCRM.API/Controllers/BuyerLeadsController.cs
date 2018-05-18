using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class BuyerLeadsController : ApiController
    {
        //private RealEstateCRMContext db = new RealEstateCRMContext();
        IRepository<BuyerLead> buyerCrud;
        IDbContext realEstateDb;

        public BuyerLeadsController()
        {
            realEstateDb = new RealEstateCRMContext();
            buyerCrud = new CRUD<BuyerLead>(realEstateDb);
        }

        // GET: api/BuyerLeads
        
          //public IQueryable<BuyerLead> GetBuyerLeads()
          //public IEnumerable<BuyerLead> GetBuyerLeads()
          //I'm using IQueryable
          public IHttpActionResult GetBuyerLeads()
        {
            try
            {

                //var buyerleads = buyerCrud.Table;
                IQueryable<BuyerLead> buyerleads = buyerCrud.Table;
                return Ok(buyerleads);
            } catch
            {
                return InternalServerError();
            }
        }

        // GET: api/BuyerLeads/5
        [ResponseType(typeof(BuyerLead))]
        public IHttpActionResult GetBuyerLead(int id)
        {
            BuyerLead buyerLead = buyerCrud.GetByID(id);
            if (buyerLead == null)
            {
                return NotFound();
            }

            return Ok(buyerLead);
        }

        // PUT: api/BuyerLeads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuyerLead(int id, BuyerLead buyerLead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buyerLead.BuyerLeadId)
            {
                return BadRequest();
            }

            //db.Entry(buyerLead).State = EntityState.Modified;
            buyerCrud.Update(buyerLead);

            try
            {
                
                //db.SaveChanges();
                buyerCrud.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyerLeadExists(id))
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

        // POST: api/BuyerLeads
        [ResponseType(typeof(BuyerLead))]
        public IHttpActionResult PostBuyerLead(BuyerLead buyerLead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.BuyerLeads.Add(buyerLead);
            //db.SaveChanges();
            buyerCrud.Insert(buyerLead);

            return CreatedAtRoute("DefaultApi", new { id = buyerLead.BuyerLeadId }, buyerLead);
        }

        // DELETE: api/BuyerLeads/5
        [ResponseType(typeof(BuyerLead))]
        public IHttpActionResult DeleteBuyerLead(int id)
        {
            BuyerLead buyerLead = buyerCrud.GetByID(id);

            if (buyerLead == null)
            {
                return NotFound();
            }

            buyerCrud.Delete(buyerLead);

            return Ok(buyerLead);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                buyerCrud.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuyerLeadExists(int id)
        {
            return buyerCrud.Table.ToList().Count(e => e.BuyerLeadId == id) > 0;
            
        }
    }
}
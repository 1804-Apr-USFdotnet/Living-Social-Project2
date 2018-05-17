using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RealEstateCRM.DataAccessLayer;
using RealEstateCRM.Models;

namespace RealEstateCRM.API.Controllers
{
    public class BuyerLeadsController : ApiController
    {
        private RealEstateCRMContext db = new RealEstateCRMContext();

        // GET: api/BuyerLeads
        public IQueryable<BuyerLead> GetBuyerLeads()
        {
            return db.BuyerLeads;
        }

        // GET: api/BuyerLeads/5
        [ResponseType(typeof(BuyerLead))]
        public async Task<IHttpActionResult> GetBuyerLead(int id)
        {
            BuyerLead buyerLead = await db.BuyerLeads.FindAsync(id);
            if (buyerLead == null)
            {
                return NotFound();
            }

            return Ok(buyerLead);
        }

        // PUT: api/BuyerLeads/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBuyerLead(int id, BuyerLead buyerLead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buyerLead.BuyerLeadId)
            {
                return BadRequest();
            }

            db.Entry(buyerLead).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> PostBuyerLead(BuyerLead buyerLead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BuyerLeads.Add(buyerLead);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = buyerLead.BuyerLeadId }, buyerLead);
        }

        // DELETE: api/BuyerLeads/5
        [ResponseType(typeof(BuyerLead))]
        public async Task<IHttpActionResult> DeleteBuyerLead(int id)
        {
            BuyerLead buyerLead = await db.BuyerLeads.FindAsync(id);
            if (buyerLead == null)
            {
                return NotFound();
            }

            db.BuyerLeads.Remove(buyerLead);
            await db.SaveChangesAsync();

            return Ok(buyerLead);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuyerLeadExists(int id)
        {
            return db.BuyerLeads.Count(e => e.BuyerLeadId == id) > 0;
        }
    }
}
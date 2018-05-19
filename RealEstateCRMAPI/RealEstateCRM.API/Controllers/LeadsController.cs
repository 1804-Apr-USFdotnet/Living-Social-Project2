﻿using System;
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
    public class LeadsController : ApiController
    {
        //private RealEstateCRMContext db = new RealEstateCRMContext();
        IRepository<Lead> leadCrud;
        IDbContext realEstateDb;

        public LeadsController()
        {
            realEstateDb = new RealEstateCRMContext();
            leadCrud = new CRUD<Lead>(realEstateDb);
        }

        // GET: api/Leads
        public IHttpActionResult GetLeads()
        {
            try
            {
                IQueryable<Lead> leads = leadCrud.Table;
                return Ok(leads);
            } catch
            {
                return InternalServerError();
            }
        }

        // GET: api/Leads/5
        [ResponseType(typeof(Lead))]
        public IHttpActionResult GetLead(int id)
        {
            Lead lead = leadCrud.GetByID(id);

            if (lead == null)
            {
                return NotFound();
            }

            return Ok(lead);
        }

        // PUT: api/Leads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLead(int id, Lead lead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lead.LeadId)
            {
                return BadRequest();
            }

            leadCrud.Update(lead);

            try
            {
                leadCrud.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeadExists(id))
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

        // POST: api/Leads
        [ResponseType(typeof(Lead))]
        public IHttpActionResult PostLead(Lead lead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            leadCrud.Insert(lead);

            return CreatedAtRoute("DefaultApi", new { id = lead.LeadId }, lead);
        }

        // DELETE: api/Leads/5
        [ResponseType(typeof(Lead))]
        public IHttpActionResult DeleteLead(int id)
        {
            Lead lead = leadCrud.GetByID(id);
            if (lead == null)
            {
                return NotFound();
            }


            
            leadCrud.Delete(lead);

            return Ok(lead);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                leadCrud.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeadExists(int id)
        {
            return leadCrud.Table.ToList().Count(e => e.LeadId == id) > 0;
        }
    }
}
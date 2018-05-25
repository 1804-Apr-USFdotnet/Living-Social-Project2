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
using RealEstateCRM.DataAccessLayer.Repositories;
using RealEstateCRM.Models;

namespace RealEstateCRM.API.Controllers
{
    public class LeadsController : AGeneralController
    {
        //private RealEstateCRMContext db = new RealEstateCRMContext();
        IRepository<Lead> leadCrud;
        IRepository<User> userCrud;
        IRepository<RealEstateAgent> agentCrud;
        IDbContext realEstateDb;

        public LeadsController()
        {
            realEstateDb = new RealEstateCRMContext();
            leadCrud = new CRUD<Lead>(realEstateDb);
            userCrud = new CRUD<User>(realEstateDb);
            agentCrud = new CRUD<RealEstateAgent>(realEstateDb);

        }

        // GET: api/Leads
        // needed to add route because default route wasnt being found properly
        [Route("api/Leads")]
        [ResponseType(typeof(Lead))]
        public async Task<IHttpActionResult> GetLeads()
        {
            DataTransfer curUser = await GetCurrentUserInfo();

            try
            {
                if(curUser.roles[0].ToLower() == "user")
                {
                    User user = userCrud.Table.First(u => u.Email == curUser.userName);
                    IQueryable<Lead> leads = leadCrud.Table.Where(l => l.UserId == user.UserId);
                    return Ok(leads);


                }
                else if(curUser.roles[0].ToLower() == "agent")
                {
                    RealEstateAgent agent = agentCrud.Table.First(a => a.Email == curUser.userName);
                    IQueryable<Lead> leads = leadCrud.Table;

                    return Ok(leads);
                }
                
                else
                {
                    return BadRequest();
                }
                

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
        public async Task<IHttpActionResult> PutLead(int id, Lead lead)
        {
            // get cur user info
            DataTransfer curUser = await GetCurrentUserInfo();
            User user = userCrud.Table.First(u => u.Email == curUser.userName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            if (id != lead.LeadId)
            {
                return BadRequest("Editing the wrong lead");
            }

            if(user.UserId != lead.UserId)
            {
                return BadRequest("User did not create lead");
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
        public async Task<IHttpActionResult> PostLead(Lead lead)
        {
            DataTransfer curUser = await GetCurrentUserInfo();
            User user = userCrud.Table.First(u => u.Email == curUser.userName);
            lead.UserId = user.UserId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            leadCrud.Insert(lead);

            return CreatedAtRoute("DefaultApi", new { id = lead.LeadId }, lead);
        }

        // DELETE: api/Leads/5
        [ResponseType(typeof(Lead))]
        public async Task<IHttpActionResult> DeleteLead(int id)
        {
            Lead lead = leadCrud.GetByID(id);
            DataTransfer curUser = await GetCurrentUserInfo();
            User user = userCrud.Table.First(u => u.Email == curUser.userName);

            if (lead == null)
            {
                return NotFound();
            }
            if(lead.UserId != user.UserId)
            {
                return BadRequest();
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
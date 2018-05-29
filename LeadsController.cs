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
using System.Web.Http.Cors;
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

        // Angular api action
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Leads/ng/{email}")]
        [ResponseType(typeof(Lead))]
        public IHttpActionResult GetLeadByEmail(string email)
        {
            IQueryable<Lead> leads = leadCrud.Table;
            IQueryable<Lead> foundLeads = leads.Where(l => l.Email.Contains(email.ToLower()));

            if (leads == null)
            {
                return NotFound();
            }

            return Ok(foundLeads);
        }

        // POST: api/Leads
        [ResponseType(typeof(Lead))]
        [HttpPost]
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

        // GET: api/Leads
        // needed to add route because default route wasnt being found properly
        [AllowAnonymous]
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
                    IQueryable<Lead> leads = leadCrud.Table.Where(l => l.RealEstateAgent == null);

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

        
        [Route("api/Leads/Favorites")]
        [ResponseType(typeof(Lead))]
        [HttpGet]
        //public async Task<IHttpActionResult> GetFavorites() //old attempts - serverside sorting
        public IHttpActionResult GetFavorites()
        {
            //DataTransfer curUser = await GetCurrentUserInfo();

            #region old attempts -serverside sorting region
            //try
            //{
            //RealEstateAgent curAgent = agentCrud.Table.First(agent => agent.Email == curUser.userName);
            //IQueryable<Lead> leads = leadCrud.Table.Where(lead => lead.RealEstateAgentId == curAgent.RealEstateAgentId);

            //var leads = leadCrud.Table.ToList()
            //    .Where(lead => lead.RealEstateAgentId == curAgent.RealEstateAgentId);


            //RealEstateAgent agent = agentCrud.Table.First(a => a.Email == curUser.userName);
            //IQueryable<Lead> leads = leadCrud.Table.Where(l => l.RealEstateAgentId == agent.RealEstateAgentId);

            //RealEstateAgent agent = agentCrud.Table.First(a => a.Email == curUser.userName);
            //IEnumerable<Lead> leads = leadCrud.Table.ToList();

            //var myleads = leads.Where(l => l.RealEstateAgentId == agent.RealEstateAgentId);
            //return Ok(myleads);
            //} catch
            //{
            //    return InternalServerError();
            //}
            #endregion



            IQueryable<Lead> leads = leadCrud.Table;
            return Ok(leads);
            


            

            

        }

        // GET: api/Leads/5
        [AllowAnonymous]

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
        
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Leads/ng")]
        [ResponseType(typeof(Lead))]
        public IHttpActionResult GetAllLeads()
        {

            
            try
            {
                IQueryable<Lead> leads = leadCrud.Table;
                return Ok(leads);


            }
            catch
            {
                return InternalServerError();
            }
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

        // PUT: api/Leads/checkout/5
        [Route("api/Leads/checkout/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> CheckoutLead(int id, Lead lead)
        {
            // get cur user info
            DataTransfer curUser = await GetCurrentUserInfo();
            User user = new User();

            if (curUser.roles[0].ToLower() == "user")
            {
                user = userCrud.Table.First(u => u.Email == curUser.userName);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (curUser.roles[0].ToLower() == "user")
            {

                if (id != lead.LeadId)
                {
                    return BadRequest("Editing the wrong lead");
                }



                if (user.UserId != lead.UserId)
                {
                    return BadRequest("User did not create lead");
                }
            }

            RealEstateAgent agentFavoriting = agentCrud.Table.ToList().Find(agent => agent.Email == curUser.userName);

            lead.RealEstateAgentId = agentFavoriting.RealEstateAgentId;
            
            leadCrud.Update(lead);
            agentCrud.Update(agentFavoriting);

            try
            {
                //leadCrud.Save();

                
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

        // PUT: api/Leads/return/5
        [Route("api/Leads/return/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> CheckInLead(int id, Lead lead)
        {
            // get cur user info
            DataTransfer curUser = await GetCurrentUserInfo();
            

            if (curUser.roles[0].ToLower() == "user")
            {
                return BadRequest("Users cannot unfavorite leads");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RealEstateAgent agentUnFavoriting = agentCrud.Table.ToList().Find(agent => agent.Email == curUser.userName);

            if (agentUnFavoriting.RealEstateAgentId != lead.RealEstateAgentId)
            {
                return BadRequest("Agent did not favorite this lead");
            }

            lead.RealEstateAgent = null;
            lead.RealEstateAgentId = null;

            leadCrud.Update(lead);
            agentCrud.Update(agentUnFavoriting);

            try
            {
                //leadCrud.Save();


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
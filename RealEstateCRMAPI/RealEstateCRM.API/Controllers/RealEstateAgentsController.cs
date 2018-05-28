using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using RealEstateCRM.DataAccessLayer;
using RealEstateCRM.DataAccessLayer.Repositories;
using RealEstateCRM.Models;

namespace RealEstateCRM.API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class RealEstateAgentsController : ApiController
    {
        //private RealEstateCRMContext db = new RealEstateCRMContext();
        IRepository<RealEstateAgent> agentCrud;
        IRepository<User> userCrud;
        IDbContext realEstateDb;

        public RealEstateAgentsController()
        {
            realEstateDb = new RealEstateCRMContext();
            agentCrud = new CRUD<RealEstateAgent>(realEstateDb);
            userCrud = new CRUD<User>(realEstateDb);
        }

        // GET: api/RealEstateAgents
        [ResponseType(typeof(RealEstateAgent))]
        public IHttpActionResult GetRealEstateAgents()
        {
            try
            {
                IQueryable<RealEstateAgent> agents = agentCrud.Table;
                return Ok(agents.Include ( agent => agent.Leads).AsEnumerable());
            }
            catch
            {
                return InternalServerError();
            }
        }



        // GET: api/RealEstateAgents/5
        [ResponseType(typeof(RealEstateAgent))]
        public IHttpActionResult GetRealEstateAgent(int id)
        {
            RealEstateAgent realEstateAgent = agentCrud.GetByID(id);
            if (realEstateAgent == null)
            {
                return NotFound();
            }

            return Ok(realEstateAgent);
        }

        // PUT: api/RealEstateAgents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRealEstateAgent(int id, RealEstateAgent realEstateAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != realEstateAgent.RealEstateAgentId)
            {
                return BadRequest();
            }

            agentCrud.Update(realEstateAgent);

            try
            {
                agentCrud.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealEstateAgentExists(id))
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

        // POST: api/RealEstateAgents
        [ResponseType(typeof(RealEstateAgent))]
        [AllowAnonymous]
        public IHttpActionResult PostRealEstateAgent(RealEstateAgent realEstateAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            agentCrud.Insert(realEstateAgent);

            return CreatedAtRoute("DefaultApi", new { id = realEstateAgent.RealEstateAgentId }, realEstateAgent);
        }

        // DELETE: api/RealEstateAgents/5
        [HttpDelete]
        [ResponseType(typeof(RealEstateAgent))]
        public IHttpActionResult DeleteRealEstateAgent(int id)
        {
            RealEstateAgent realEstateAgent = agentCrud.GetByID(id);
            if (realEstateAgent == null)
            {
                return NotFound();
            }

            agentCrud.Delete(realEstateAgent);

            return Ok(realEstateAgent);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/RealEstateAgents/emailcheck")]
        public HttpResponseMessage EmailCheck(RealEstateAgent agent)
        {
            if (RealEstateAgentEmailExists(agent.Email))
            {
                int HttpResponse = 400;
                var response = Request.CreateResponse((HttpStatusCode)HttpResponse);
                response.ReasonPhrase = "Duplicate email";
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                agentCrud.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RealEstateAgentExists(int id)
        {
            return agentCrud.Table.ToList().Count(e => e.RealEstateAgentId == id) > 0;
        }

        public bool RealEstateAgentEmailExists(string email)
        {
            bool inAgents = agentCrud.Table.ToList().Count(a => a.Email == email) > 0;
            bool inUsers = userCrud.Table.ToList().Count(u => u.Email == email) > 0;
            return (inAgents && inUsers);
        }
    }
}
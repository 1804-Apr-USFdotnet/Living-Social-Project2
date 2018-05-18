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
    public class SellerLeadsController : ApiController
    {
        IRepository<SellerLead> sellCrud;
        IDbContext realEstateDb;

        public SellerLeadsController()
        {
            realEstateDb = new RealEstateCRMContext();
            sellCrud = new CRUD<SellerLead>(realEstateDb);
        }
        private bool SellerLeadExists(int id)
        {
            return sellCrud.Table.ToList().Count(x => x.SellerLeadId == id) > 0;
        }
        //Get: Api/SellerLeads
        public IHttpActionResult GetSellerLeads()
        {
            try
            {
                IQueryable<SellerLead> sellerLeads = sellCrud.Table;
                return Ok(sellerLeads);
            }
            catch
            {
                return InternalServerError();
            }
        }
        //Get: Api/SellerLeads/8
        [ResponseType(typeof(SellerLead))]
        public IHttpActionResult GetSellerLead(int leadId)
        {
            SellerLead sellLead = sellCrud.GetByID(leadId);
            if (sellLead == null)
                return NotFound();
            return Ok(sellLead);
        }
        //Put: Api/SellerLeads/8
        /*[ResponseType(typeof(void))]
        public IHttpActionResult PutSellerLead(int sellId, SellerLead sellerLead)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(sellId != sellerLead.SellerLeadId)
                sellCrud.Save();
            sellCrud.Update(sellerLead);
            try
            {
                sellCrud.Save();
            }
            catch (DbUpdateConcurrencyException)
                {
                    if (!SellerLeadExists(sellId))
                        return NotFound();
                    else
                        throw;
                }
        }*/
    }
}

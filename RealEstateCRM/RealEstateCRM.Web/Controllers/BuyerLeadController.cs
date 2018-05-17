using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateCRM;
using RealEstateCRM.Models;
using RealEstateCRM.DataAccessLayer;
using RealEstateCRM.DataAccessLayer.Repositories;

namespace RealEstateCRM.Web.Controllers
{
    public class BuyerLeadController : Controller
    {
        IRepository<BuyerLead> buyerCrud;
        IDbContext crmDb;

        public BuyerLeadController()
        {
            crmDb = new RealEstateCRMContext();
            buyerCrud = new CRUD<BuyerLead>(crmDb);
        }


        public BuyerLeadController(IDbContext fakeDb)
        {
            //fakeDb = new FakeRealEstateCRMContext();
            //testDb = new TestCRMContext();
        }


        // GET: BuyerLead
        public ActionResult Index()
        {
            var BuyerLeadList = buyerCrud.Table.ToList();
            return View(BuyerLeadList);
        }

        // GET: BuyerLead/Details/5
        public ActionResult Details(int id)
        {
            return View(buyerCrud.GetByID(id));
        }

        // GET: BuyerLead/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuyerLead/Create
        [HttpPost]
        public ActionResult Create(BuyerLead newBuyerLead)
        {
            try
            {
                buyerCrud.Insert(newBuyerLead);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BuyerLead/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BuyerLead/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BuyerLead/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BuyerLead/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateCRM;
using RealEstateCRM.Models;
using RealEstateCRM.DataAccessLayer;
using RealEstateCRM.DataAccessLayer.Repositories;
using System.Data.Entity;

namespace RealEstateCRM.Web.Controllers
{
    public class SellerLeadController : Controller
    {
        IRepository<SellerLead> sellerCrud;
        IDbContext crmDb;

        public SellerLeadController()
        {
            crmDb = new RealEstateCRMContext();
            sellerCrud = new CRUD<SellerLead>(crmDb);

        }


        public SellerLeadController(IDbContext fakeDb)
        {
            //fakeDb = new FakeRealEstateCRMContext();
            //testDb = new TestCRMContext();
        }

        // GET: SellerLead
        public ActionResult Index()
        {
            var SellerLeadList = sellerCrud.Table.ToList();
            return View(SellerLeadList);
        }

        // GET: SellerLead/Details/5
        public ActionResult Details(int id)
        {
            return View(sellerCrud.GetByID(id));
        }

        // GET: SellerLead/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellerLead/Create
        [HttpPost]
        public ActionResult Create(SellerLead newSellerLead)
        {
            try
            {
                sellerCrud.Insert(newSellerLead);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SellerLead/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SellerLead/Edit/5
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

        // GET: SellerLead/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SellerLead/Delete/5
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

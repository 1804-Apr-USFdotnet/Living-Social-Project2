using RealEstateCRM.DataAccessLayer;
using RealEstateCRM.DataAccessLayer.Repositories;
using RealEstateCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RealEstateCRM.Web.Controllers
{
    public class UserController : Controller
    {
        //private IRepository<User> _repository = null;
        //public RealEstateCRMContext dbcontext;

        //public UserController()
        //{
        //    this._repository = new CRUD<User>(dbcontext);
        //}// GET: User

        IRepository<User> crud;
        IDbContext realEstateDb;

        public UserController()
        {
            realEstateDb = new RealEstateCRMContext();
            crud = new CRUD<User>(realEstateDb);

        }
        public ActionResult Index()
        { 
            IEnumerable<User> sampleObj = crud.Table;
            return View(sampleObj);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                crud.Insert(user);
                crud.Save();
                return RedirectToAction("index");
            }
            else
            {
                return View(user);
            }
        }
    }
}
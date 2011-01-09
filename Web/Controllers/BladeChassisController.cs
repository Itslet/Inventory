using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure;
using Core;


namespace Web.Controllers
{
    public class BladeChassisController : Controller
    {
        //
        // GET: /BladeChassis/
       
        public ActionResult Index()
        {
            var chassis = SessionFactory.Current.All<BladeChassis>();
            return View(chassis);
            
        }

        //
        // GET: /BladeChassis/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BladeChassis/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /BladeChassis/Create

        [HttpPost]
        public ActionResult Create(BladeChassis chassis)
        {
            try
            {
               SessionFactory.Current.Save(chassis);
               SessionFactory.Current.CommitChanges();
               return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /BladeChassis/Edit/5
        
        public ActionResult Edit(int id)
        {
            string cid = id.ToString();
            var chassis = SessionFactory.Current.Single<BladeChassis>(x => x.ChassisID == cid);
            return View(chassis);
        }

        //
        // POST: /BladeChassis/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection values)
        {
            try
            {
                // TODO: Add update logic here
                string cid = id.ToString();
                var c = SessionFactory.Current.Single<BladeChassis>(x => x.ChassisID == cid);
                
                UpdateModel(c);
                SessionFactory.Current.Save(c);
                SessionFactory.Current.CommitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                string cid = id.ToString();
                var chassis = SessionFactory.Current.Single<BladeChassis>(x => x.ChassisID == cid);
                SessionFactory.Current.Delete(chassis);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}

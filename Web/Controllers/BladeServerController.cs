using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Web.Infrastructure;
using Web.Model;

namespace Web.Controllers
{
    public class BladeServerController : Controller
    {
        //
        // GET: /BladeServer/

        public ActionResult Index()
        {
            var servers = SessionFactory.Current.All<BladeServer>();
            return View(servers);
            
        }

        //
        // GET: /BladeServer/Details/5

        public ActionResult Details()
        {
            return View();
        }

        //
        // GET: /BladeServer/Create

        public ActionResult Create()
        {
            var enclosures = SessionFactory.Current.All<BladeChassis>();
            ViewData["ChassisID"] = new SelectList(enclosures, "ChassisID", "Description");
            return View();
        } 

        //
        // POST: /BladeServer/Create

        [HttpPost]
        public ActionResult Create(BladeServer server, FormCollection formvalues)
        {
            var enclosures = SessionFactory.Current.All<BladeChassis>();
            ViewData["ChassisID"] = new SelectList(enclosures, "ChassisID", "Description");

            if (!String.IsNullOrEmpty(formvalues["Hostname"]))
            {
                try
                {
                    string chassisId = formvalues["ChassisID"];
                    var chassis = SessionFactory.Current.Single<BladeChassis>(x => x.ChassisID == chassisId);
                    server.BladeChassis = chassis;
                    SessionFactory.Current.Save(server);
                    SessionFactory.Current.CommitChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            
                
                return View(server);
            }
            
            return View();
         }
        
        //
        // GET: /BladeServer/Edit/5
 
        public ActionResult Edit(string id)
        {

            var serverToEdit = SessionFactory.Current.Single<BladeServer>(x => x.Hostname == id);
            var enclosures = SessionFactory.Current.All<BladeChassis>().ToList();
         
            var selectList = enclosures.Select(x => new SelectListItem
            {   Text = x.Description,
                Value = x.ChassisID
            }).ToList();

            BladeServerEditViewData viewData = new BladeServerEditViewData
                {
                    Hostname = serverToEdit.Hostname,
                    IPAddress = serverToEdit.IPAddress,
                    ServerRole = serverToEdit.ServerRole,
                    OS = serverToEdit.OS,
                    BladeEnclosures = selectList,
                    SelectedBladeEnclosure = serverToEdit.BladeChassis.ChassisID
                };
            
            return View(viewData);
        }

        //
        // POST: /BladeServer/Edit/5

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                //AutoMapper would come into handy here
                var srv = SessionFactory.Current.Single<BladeServer>(z => z.Hostname == collection["Hostname"]);

                var chassis = SessionFactory.Current.Single<BladeChassis>(y => y.ChassisID == collection["SelectedBladeEnclosure"]);
                    srv.BladeChassis = chassis;
                
                srv.Hostname = collection["Hostname"];
                srv.IPAddress = collection["IPAddress"];
                srv.OS = collection["OS"];
                srv.ServerRole = collection["Serverrole"];
                
                SessionFactory.Current.Save(srv);
                SessionFactory.Current.CommitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BladeServer/Delete/5
 
      
        public ActionResult Delete(string id)
        {
            try
            {
                // TODO: Add delete logic here
                string hostname = id.ToString();
                BladeServer server = SessionFactory.Current.Single<BladeServer>(x => x.Hostname == hostname);
                string test = server.Hostname;
                SessionFactory.Current.Delete(server);
                //return View("Index");

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}

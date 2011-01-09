using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Web.Infrastructure;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            var servers = SessionFactory.Current.All<BladeServer>();
            return View(servers);
        }

        }
   
}

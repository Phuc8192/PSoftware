using PSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSoftware.Controllers
{
    public class HomeController : Controller
    {
        private PSoftwareDB db = new PSoftwareDB();
        public ActionResult Index()
        {
            //var sANPHAMs = db.SanPhams.OrderByDescending(s => s.NgayTao);
            var sANPHAMs = db.SanPhams.OrderBy(s => Guid.NewGuid());
            return View(sANPHAMs.ToList());
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
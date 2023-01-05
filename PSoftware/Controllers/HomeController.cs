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

        [ChildActionOnly]
        public PartialViewResult IndexRight()
        {
            var right = db.SanPhams.OrderBy(x => x.NgayTao).Take(2);
            return PartialView(right.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult IndexAction()
        {
            var action = db.SanPhams.Where(x => x.TheLoai.TenTL == "Hành Động").OrderByDescending(x => x.NgayTao).Take(8);
            return PartialView(action.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult IndexAdven()
        {
            var adven = db.SanPhams.Where(x => x.TheLoai.TenTL == "Phiêu Lưu").OrderByDescending(x => x.NgayTao).Take(8);
            return PartialView(adven.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult IndexRPG()
        {
            var rpg = db.SanPhams.Where(x => x.TheLoai.TenTL == "RPG").OrderByDescending(x => x.NgayTao).Take(8);
            return PartialView(rpg.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult IndexSoftware()
        {
            var sw = db.SanPhams.Where(x => x.LoaiSanPham.TenLSP == "Phần Mềm").OrderByDescending(x => x.NgayTao).Take(8);
            return PartialView(sw.ToList());
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}
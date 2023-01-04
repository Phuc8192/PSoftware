using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PSoftware.Models;
using PagedList;

namespace PSoftware.Areas.Admin.Controllers
{
    public class QLHoaDonsController : Controller
    {
        private PSoftwareDB db = new PSoftwareDB();

        // GET: Admin/QLHoaDons
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var links = (from l in db.HoaDons select l).OrderByDescending(s => s.MaHD);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DuyetHD(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HoaDon hOADON = db.HoaDons.Find(id);
            hOADON.TrangThai = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/QLHoaDons/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: Admin/QLHoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoKH");           
            return View();
        }

        // POST: Admin/QLHoaDons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,NgayBan,TrangThai,MaKH")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                hoaDon.MaHD = Guid.NewGuid();
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoKH", hoaDon.MaKH);            
            return View(hoaDon);
        }

        // GET: Admin/QLHoaDons/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoKH", hoaDon.MaKH);       
            return View(hoaDon);
        }

        // POST: Admin/QLHoaDons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,NgayBan,TrangThai,MaKH")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoKH", hoaDon.MaKH);            
            return View(hoaDon);
        }

        // GET: Admin/QLHoaDons/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/QLHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

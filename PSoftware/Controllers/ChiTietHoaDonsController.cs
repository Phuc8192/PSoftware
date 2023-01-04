using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PSoftware.Models;

namespace PSoftware.Controllers
{
    public class ChiTietHoaDonsController : Controller
    {
        private PSoftwareDB db = new PSoftwareDB();

        // GET: ChiTietHoaDons
        public ActionResult Index(Guid id)
        {
            List<ChiTietHoaDon> _cthd = db.ChiTietHoaDons.Where(x => x.MaHD == id).ToList();
            return View(_cthd);
        }

        // GET: ChiTietHoaDons/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaHD");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: ChiTietHoaDons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaSP,Gia,SoLuong")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                chiTietHoaDon.MaHD = Guid.NewGuid();
                db.ChiTietHoaDons.Add(chiTietHoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaHD", chiTietHoaDon.MaHD);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietHoaDon.MaSP);
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDons/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaHD", chiTietHoaDon.MaHD);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietHoaDon.MaSP);
            return View(chiTietHoaDon);
        }

        // POST: ChiTietHoaDons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaSP,Gia,SoLuong")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietHoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaHD", chiTietHoaDon.MaHD);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietHoaDon.MaSP);
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDons/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // POST: ChiTietHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            db.ChiTietHoaDons.Remove(chiTietHoaDon);
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

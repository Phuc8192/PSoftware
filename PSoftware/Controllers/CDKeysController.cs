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
    public class CDKeysController : Controller
    {
        private PSoftwareDB db = new PSoftwareDB();

        // GET: CDKeys
        public ActionResult Index(Guid id)
        {
            List<CDKey> _cdkey = db.CDKeys.Where(x => x.MaHD == id).ToList();
            return View(_cdkey);
        }

        // GET: CDKeys/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDKey cDKey = db.CDKeys.Find(id);
            if (cDKey == null)
            {
                return HttpNotFound();
            }
            return View(cDKey);
        }

        // GET: CDKeys/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaHD");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: CDKeys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaSP,CDKey1")] CDKey cDKey)
        {
            if (ModelState.IsValid)
            {
                cDKey.MaHD = Guid.NewGuid();
                db.CDKeys.Add(cDKey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaHD", cDKey.MaHD);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cDKey.MaSP);
            return View(cDKey);
        }

        // GET: CDKeys/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDKey cDKey = db.CDKeys.Find(id);
            if (cDKey == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaHD", cDKey.MaHD);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cDKey.MaSP);
            return View(cDKey);
        }

        // POST: CDKeys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaSP,CDKey1")] CDKey cDKey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cDKey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "MaHD", cDKey.MaHD);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cDKey.MaSP);
            return View(cDKey);
        }

        // GET: CDKeys/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CDKey cDKey = db.CDKeys.Find(id);
            if (cDKey == null)
            {
                return HttpNotFound();
            }
            return View(cDKey);
        }

        // POST: CDKeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CDKey cDKey = db.CDKeys.Find(id);
            db.CDKeys.Remove(cDKey);
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

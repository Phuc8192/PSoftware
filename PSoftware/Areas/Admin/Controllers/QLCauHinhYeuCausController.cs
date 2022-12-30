using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PSoftware.Models;

namespace PSoftware.Areas.Admin.Controllers
{
    public class QLCauHinhYeuCausController : Controller
    {
        private PSoftwareDB db = new PSoftwareDB();

        // GET: Admin/QLCauHinhYeuCaus
        public ActionResult Index()
        {
            var cauHinhYeuCaus = db.CauHinhYeuCaus.Include(c => c.SanPham);
            return View(cauHinhYeuCaus.ToList());
        }

        // GET: Admin/QLCauHinhYeuCaus/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHinhYeuCau cauHinhYeuCau = db.CauHinhYeuCaus.Find(id);
            if (cauHinhYeuCau == null)
            {
                return HttpNotFound();
            }
            return View(cauHinhYeuCau);
        }

        // GET: Admin/QLCauHinhYeuCaus/Create
        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/QLCauHinhYeuCaus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCHYC,CPU,RAM,GPU,Storage,OS,DirectX,MaSP")] CauHinhYeuCau cauHinhYeuCau)
        {
            if (ModelState.IsValid)
            {
                cauHinhYeuCau.MaCHYC = Guid.NewGuid();
                db.CauHinhYeuCaus.Add(cauHinhYeuCau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cauHinhYeuCau.MaSP);
            return View(cauHinhYeuCau);
        }

        // GET: Admin/QLCauHinhYeuCaus/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHinhYeuCau cauHinhYeuCau = db.CauHinhYeuCaus.Find(id);
            if (cauHinhYeuCau == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cauHinhYeuCau.MaSP);
            return View(cauHinhYeuCau);
        }

        // POST: Admin/QLCauHinhYeuCaus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCHYC,CPU,RAM,GPU,Storage,OS,DirectX,MaSP")] CauHinhYeuCau cauHinhYeuCau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cauHinhYeuCau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cauHinhYeuCau.MaSP);
            return View(cauHinhYeuCau);
        }

        // GET: Admin/QLCauHinhYeuCaus/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHinhYeuCau cauHinhYeuCau = db.CauHinhYeuCaus.Find(id);
            if (cauHinhYeuCau == null)
            {
                return HttpNotFound();
            }
            return View(cauHinhYeuCau);
        }

        // POST: Admin/QLCauHinhYeuCaus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CauHinhYeuCau cauHinhYeuCau = db.CauHinhYeuCaus.Find(id);
            db.CauHinhYeuCaus.Remove(cauHinhYeuCau);
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

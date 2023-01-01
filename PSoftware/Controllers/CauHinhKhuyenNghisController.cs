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
    public class CauHinhKhuyenNghisController : Controller
    {
        private PSoftwareDB db = new PSoftwareDB();

        // GET: CauHinhKhuyenNghis
        public ActionResult Index()
        {
            var cauHinhKhuyenNghis = db.CauHinhKhuyenNghis.Include(c => c.SanPham);
            return View(cauHinhKhuyenNghis.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult Details(Guid? id)
        {
            var chkn = db.CauHinhKhuyenNghis.Where(x => x.MaSP == id).FirstOrDefault();
            return PartialView(chkn);
        }

        // GET: CauHinhKhuyenNghis/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CauHinhKhuyenNghi cauHinhKhuyenNghi = db.CauHinhKhuyenNghis.Find(id);
        //    if (cauHinhKhuyenNghi == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cauHinhKhuyenNghi);
        //}

        // GET: CauHinhKhuyenNghis/Create
        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: CauHinhKhuyenNghis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCHKN,CPU,RAM,GPU,Storage,OS,DirectX,MaSP")] CauHinhKhuyenNghi cauHinhKhuyenNghi)
        {
            if (ModelState.IsValid)
            {
                cauHinhKhuyenNghi.MaCHKN = Guid.NewGuid();
                db.CauHinhKhuyenNghis.Add(cauHinhKhuyenNghi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cauHinhKhuyenNghi.MaSP);
            return View(cauHinhKhuyenNghi);
        }

        // GET: CauHinhKhuyenNghis/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHinhKhuyenNghi cauHinhKhuyenNghi = db.CauHinhKhuyenNghis.Find(id);
            if (cauHinhKhuyenNghi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cauHinhKhuyenNghi.MaSP);
            return View(cauHinhKhuyenNghi);
        }

        // POST: CauHinhKhuyenNghis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCHKN,CPU,RAM,GPU,Storage,OS,DirectX,MaSP")] CauHinhKhuyenNghi cauHinhKhuyenNghi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cauHinhKhuyenNghi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cauHinhKhuyenNghi.MaSP);
            return View(cauHinhKhuyenNghi);
        }

        // GET: CauHinhKhuyenNghis/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHinhKhuyenNghi cauHinhKhuyenNghi = db.CauHinhKhuyenNghis.Find(id);
            if (cauHinhKhuyenNghi == null)
            {
                return HttpNotFound();
            }
            return View(cauHinhKhuyenNghi);
        }

        // POST: CauHinhKhuyenNghis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CauHinhKhuyenNghi cauHinhKhuyenNghi = db.CauHinhKhuyenNghis.Find(id);
            db.CauHinhKhuyenNghis.Remove(cauHinhKhuyenNghi);
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

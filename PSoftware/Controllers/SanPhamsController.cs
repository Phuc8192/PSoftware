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
    public class SanPhamsController : Controller
    {
        private PSoftwareEntities1 db = new PSoftwareEntities1();

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.NhaPhatTrien).Include(s => s.TheLoai);
            return View(sanPhams.ToList());
        }

        public ActionResult TimKiem()
        {           
            var sanPham = db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.NhaPhatTrien).Include(s => s.TheLoai);
            return View(sanPham.ToList());
        }
        [HttpPost]
        public ActionResult TimKiem(string tenSP)
        {
            //var sanPham = db.SANPHAMs.Where(abc => abc.TenSP == tenSP);
            //return View(sanPham.ToList());
            ViewBag.tenSP = tenSP;
            var games = db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.NhaPhatTrien).Include(s => s.TheLoai);
            if (!String.IsNullOrEmpty(tenSP))
            {
                tenSP = tenSP.ToLower();
                games = games.Where(b => b.TenSP.ToLower().Contains(tenSP));
            }

            return View(games.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP");
            ViewBag.MaNPT = new SelectList(db.NhaPhatTriens, "MaNPT", "TenNPT");
            ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,Gia,NgayTao,NgayChinhSua,Anh,NoiDung,MaLSP,MaNPT,MaTL")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.MaSP = Guid.NewGuid();
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            ViewBag.MaNPT = new SelectList(db.NhaPhatTriens, "MaNPT", "TenNPT", sanPham.MaNPT);
            ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL", sanPham.MaTL);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            ViewBag.MaNPT = new SelectList(db.NhaPhatTriens, "MaNPT", "TenNPT", sanPham.MaNPT);
            ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL", sanPham.MaTL);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,Gia,NgayTao,NgayChinhSua,Anh,NoiDung,MaLSP,MaNPT,MaTL")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            ViewBag.MaNPT = new SelectList(db.NhaPhatTriens, "MaNPT", "TenNPT", sanPham.MaNPT);
            ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL", sanPham.MaTL);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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

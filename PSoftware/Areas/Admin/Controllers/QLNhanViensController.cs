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
    public class QLNhanViensController : Controller
    {
        private PSoftwareEntities1 db = new PSoftwareEntities1();

        // GET: Admin/QLNhanViens
        public ActionResult Index()
        {
            return View(db.NhanViens.ToList());
        }

        // GET: Admin/QLNhanViens/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: Admin/QLNhanViens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QLNhanViens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,HoNV,TenNV,GioiTinh,NgaySinh,SDT,Email,DiaChi,TenDN,MatKhau,PhanQuyen,Anh")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                nhanVien.MaNV = Guid.NewGuid();
                nhanVien.MatKhau = Encryptor.MD5Hash(nhanVien.MatKhau);
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhanVien);
        }

        // GET: Admin/QLNhanViens/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/QLNhanViens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,HoNV,TenNV,GioiTinh,NgaySinh,SDT,Email,DiaChi,TenDN,MatKhau,PhanQuyen,Anh")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanVien);
        }

        // GET: Admin/QLNhanViens/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/QLNhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
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

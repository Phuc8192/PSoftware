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
    public class QLNhaPhatTriensController : Controller
    {
        private PSoftwareEntities1 db = new PSoftwareEntities1();

        // GET: Admin/QLNhaPhatTriens
        public ActionResult Index()
        {
            return View(db.NhaPhatTriens.ToList());
        }

        // GET: Admin/QLNhaPhatTriens/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaPhatTrien nhaPhatTrien = db.NhaPhatTriens.Find(id);
            if (nhaPhatTrien == null)
            {
                return HttpNotFound();
            }
            return View(nhaPhatTrien);
        }

        // GET: Admin/QLNhaPhatTriens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QLNhaPhatTriens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNPT,TenNPT")] NhaPhatTrien nhaPhatTrien)
        {
            if (ModelState.IsValid)
            {
                nhaPhatTrien.MaNPT = Guid.NewGuid();
                db.NhaPhatTriens.Add(nhaPhatTrien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhaPhatTrien);
        }

        // GET: Admin/QLNhaPhatTriens/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaPhatTrien nhaPhatTrien = db.NhaPhatTriens.Find(id);
            if (nhaPhatTrien == null)
            {
                return HttpNotFound();
            }
            return View(nhaPhatTrien);
        }

        // POST: Admin/QLNhaPhatTriens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNPT,TenNPT")] NhaPhatTrien nhaPhatTrien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaPhatTrien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaPhatTrien);
        }

        // GET: Admin/QLNhaPhatTriens/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaPhatTrien nhaPhatTrien = db.NhaPhatTriens.Find(id);
            if (nhaPhatTrien == null)
            {
                return HttpNotFound();
            }
            return View(nhaPhatTrien);
        }

        // POST: Admin/QLNhaPhatTriens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            NhaPhatTrien nhaPhatTrien = db.NhaPhatTriens.Find(id);
            db.NhaPhatTriens.Remove(nhaPhatTrien);
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

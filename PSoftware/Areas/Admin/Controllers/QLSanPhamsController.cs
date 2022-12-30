using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PSoftware.Models;
using System.Xml.Linq;
using System.Web.Script.Serialization;

namespace PSoftware.Areas.Admin.Controllers
{
    public class QLSanPhamsController : Controller
    {
        private PSoftwareDB db = new PSoftwareDB();

        // GET: Admin/QLSanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.NhaPhatTrien).Include(s => s.TheLoai);
            return View(sanPhams.ToList());
        }

        [HttpGet]
        public ActionResult IndexNC(Guid? maLSP, Guid? maNPT, Guid? maTL, string tenSP = "", string giaMin = "", string giaMax = "")
        {
            string min = giaMin, max = giaMax;
            ViewBag.tenSP = tenSP;

            if (giaMin == "")
            {
                ViewBag.giaMin = "";
                min = "0";
            }
            else
            {
                ViewBag.giaMin = giaMin;
                min = giaMin;
            }
            if (max == "")
            {
                max = Int32.MaxValue.ToString();
                ViewBag.giaMax = "";// Int32.MaxValue.ToString(); 
            }
            else
            {
                ViewBag.giaMax = giaMax;
                max = giaMax;
            }

            ViewBag.maLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP");
            ViewBag.maNPT = new SelectList(db.NhaPhatTriens, "MaNPT", "TenNPT");
            ViewBag.maTL = new SelectList(db.TheLoais, "MaTL", "TenTL");

            var sanPhams = db.SanPhams.AsEnumerable()
                .Where(x => x.TenSP == tenSP && (x.Gia >= double.Parse(min) && x.Gia <= double.Parse(max)) && x.MaLSP == maLSP && x.MaNPT == maNPT && x.MaTL == maTL);
            if (sanPhams.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(sanPhams.ToList());
        }

        // GET: Admin/QLSanPhams/Details/5
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

        // GET: Admin/QLSanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP");
            ViewBag.MaNPT = new SelectList(db.NhaPhatTriens, "MaNPT", "TenNPT");
            ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL");
            return View();
        }

        // POST: Admin/QLSanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,Gia,NgayTao,NgayChinhSua,Anh,NoiDung,MaLSP,MaNPT,MaTL")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.MaSP = Guid.NewGuid();
                sanPham.NgayTao = DateTime.Now;
                sanPham.NgayChinhSua = DateTime.Now;
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            ViewBag.MaNPT = new SelectList(db.NhaPhatTriens, "MaNPT", "TenNPT", sanPham.MaNPT);
            ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL", sanPham.MaTL);
            return View(sanPham);
        }
        public SanPham ChiTiet(Guid id)
        {
            return db.SanPhams.Find(id);
        }
        public void UpdateImages(Guid id, string images)
        {
            var product = db.SanPhams.Find(id);
            product.Anh = images;
            db.SaveChanges();
        }
        public JsonResult LoadImages(Guid id)
        {
            //ProductDao dao = new ProductDao();
            var product = ChiTiet(id);
            var images = product.Anh;
            XElement xImages = XElement.Parse(images);
            List<string> listImagesReturn = new List<string>();

            foreach (XElement element in xImages.Elements())
            {
                listImagesReturn.Add(element.Value);
            }
            return Json(new
            {
                data = listImagesReturn
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveImages(Guid id, string images)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);

            XElement xElement = new XElement("Images");

            foreach (var item in listImages)
            {
                var subStringItem = item.Substring(21);
                xElement.Add(new XElement("Image", subStringItem));
            }
            //ProductDao dao = new ProductDao();
            try
            {
                UpdateImages(id, xElement.ToString());
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }

        }

        // GET: Admin/QLSanPhams/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            //SanPham sanPham = db.SanPhams.Where(x => x.MaSP == id).Include(x => x.NoiDung).FirstOrDefault();
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            ViewBag.MaNPT = new SelectList(db.NhaPhatTriens, "MaNPT", "TenNPT", sanPham.MaNPT);
            ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL", sanPham.MaTL);
            return View(sanPham);
        }

        // POST: Admin/QLSanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,Gia,NgayTao,NgayChinhSua,Anh,NoiDung,MaLSP,MaNPT,MaTL")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.NgayChinhSua = System.DateTime.Now;
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            ViewBag.MaNPT = new SelectList(db.NhaPhatTriens, "MaNPT", "TenNPT", sanPham.MaNPT);
            ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL", sanPham.MaTL);
            return View(sanPham);
        }

        // GET: Admin/QLSanPhams/Delete/5
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

        // POST: Admin/QLSanPhams/Delete/5
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSoftware.Models;

namespace PSoftware.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        private PSoftwareDB db = new PSoftwareDB();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            ViewBag.hoaDon = db.HoaDons.Count(x => x.TrangThai == true);
            ViewBag.doanhThu = db.HoaDons.Where(x => x.TrangThai == true).ToList().Sum(x => x.ChiTietHoaDons.Sum(n => n.Gia * n.SoLuong));
            ViewBag.khachHang = db.KhachHangs.Count();
            ViewBag.sanPham = db.SanPhams.Count();
            ViewBag.nhanVien = db.NhanViens.Count();
            ViewBag.theLoai = db.TheLoais.Count();
            ViewBag.NPT = db.NhaPhatTriens.Count();
            ViewBag.LSP = db.LoaiSanPhams.Count();
            return View();           
        }

        public ActionResult Logoff()
        {
            Session.Remove("TenDNNV");
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModels model)
        {
            if (ModelState.IsValid)
            {
                var dn = new HomeAdminController();
                var kq = dn.KtraDN(model.TenDN, Encryptor.MD5Hash(model.MK));
                if (kq == 1)
                {
                    var user = db.NhanViens.Where(x => x.TenDN.Equals(model.TenDN));
                    Session["TenDNNV"] = user.FirstOrDefault().TenDN;
                    Session["MaNV"] = user.FirstOrDefault().MaNV;
                    Session["PQ"] = user.FirstOrDefault().PhanQuyen;
                    ModelState.AddModelError("", "Đăng nhập thành công");
                    return RedirectToAction("Index");
                }
                else if (kq == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (kq == -1)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            return View(model);
        }

        public int KtraDN(string tenDN, string MK)
        {
            var kq = db.NhanViens.SingleOrDefault(x => x.TenDN == tenDN);

            if (kq == null)
            {
                return 0;
            }
            else
            {
                if (kq.MatKhau == MK)
                    return 1;
                else
                    return -1;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSoftware.Models;
using System.Data.Entity.Validation;
using System.Net;

namespace PSoftware.Controllers
{
    public class UserController : Controller
    {
        private PSoftwareEntities1 db = new PSoftwareEntities1();
        // GET: User       

        public ActionResult UserInfo(string id)
        {
            //return View();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        public ActionResult Logoff()
        {
            Session.Clear();
            return Redirect("/");
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
                var dn = new UserController();
                var kq = dn.KtraDN(model.TenDN, Encryptor.MD5Hash(model.MK));
                if (kq == 1)
                {
                    var user = db.KhachHangs.Where(x => x.TenDN.Equals(model.TenDN));
                    Session["TenDN"] = user.FirstOrDefault().TenDN;
                    Session["MaKH"] = user.FirstOrDefault().MaKH;
                    ModelState.AddModelError("", "Đăng nhập thành công");
                    return Redirect("/");
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


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var ktra = new UserController();
                if (ktra.KtraTenDN(model.TenDN))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (ktra.KtraEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else if (ktra.KtraSDT(model.SDT))
                {
                    ModelState.AddModelError("", "Số điện thoại đã tồn tại");
                }
                else
                {
                    var KH = new KhachHang();
                    KH.MaKH = Guid.NewGuid();
                    KH.HoKH = model.HoKH;
                    KH.TenKH = model.TenKH;
                    KH.TenDN = model.TenDN;
                    KH.DiaChi = model.DiaChi;
                    KH.Email = model.Email;
                    KH.SDT = model.SDT;
                    KH.MatKhau = Encryptor.MD5Hash(model.MK);
                    db.KhachHangs.Add(KH);
                    try
                    {
                        db.SaveChanges();
                        ViewBag.Success = "Đăng ký thành công";
                        return RedirectToAction("Login");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }
                }
            }
            return View(model);
        }

        public KhachHang LayTenDN(string tenDN)
        {
            return db.KhachHangs.SingleOrDefault(x => x.TenDN == tenDN);
        }
        public bool KtraTenDN(string TenDN)
        {
            return db.KhachHangs.Count(x => x.TenDN == TenDN) > 0;
        }
        public bool KtraEmail(string Email)
        {
            return db.KhachHangs.Count(x => x.Email == Email) > 0;
        }
        public bool KtraSDT(string SDT)
        {
            return db.KhachHangs.Count(x => x.SDT == SDT) > 0;
        }
        public int KtraDN(string tenDN, string MK, bool isLoginAdmin = false)
        {
            var kq = db.KhachHangs.SingleOrDefault(x => x.TenDN == tenDN);

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
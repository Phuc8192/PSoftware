using PSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSoftware.Controllers
{
    public class CartController : Controller
    {
        private PSoftwareDB db = new PSoftwareDB();
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            return View(cart);
        }

        public RedirectToRouteResult ThemVaoGio(Guid MaSP)
        {

            if (Session["cart"] == null)
            {
                Session["cart"] = new List<CartItem>();
            }

            List<CartItem> cart = Session["cart"] as List<CartItem>;

            if (cart.FirstOrDefault(x => x.MaSP == MaSP) == null)
            {
                SanPham sp = db.SanPhams.Find(MaSP);
                var newItem = new CartItem();
                newItem.MaSP = MaSP;
                newItem.TenSP = sp.TenSP;
                newItem.Anh = sp.Anh;
                newItem.SLuong = 1;
                newItem.Gia = (int)sp.Gia;
                cart.Add(newItem);
            }
            else
            {
                CartItem cardItem = cart.FirstOrDefault(x => x.MaSP == MaSP);
                cardItem.SLuong++;
                Session["SL"] = cardItem.SLuong;
            }

            return RedirectToAction("Index", "Cart");
        }

        public RedirectToRouteResult SuaSoLuong(Guid MaSP, int SLuongMoi)
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            CartItem itemSua = cart.FirstOrDefault(x => x.MaSP == MaSP);

            if (itemSua != null)
            {
                itemSua.SLuong = SLuongMoi;
                Session["SL"] = itemSua.SLuong;
            }
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult XoaKhoiGio(Guid MaSP)
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            CartItem itemXoa = cart.FirstOrDefault(x => x.MaSP == MaSP);

            if (itemXoa != null)
            {
                cart.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public PartialViewResult _CartPartial()
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<CartItem>();
            }

            List<CartItem> cart = Session["cart"] as List<CartItem>;

            return PartialView(cart);
        }

        public ActionResult ThanhToan(Guid MaKH)
        {
            try
            {
                List<CartItem> cart = Session["cart"] as List<CartItem>;
                HoaDon _hoadon = new HoaDon();
                _hoadon.MaHD = Guid.NewGuid();
                _hoadon.NgayBan = DateTime.Now;
                _hoadon.MaKH = MaKH;

                _hoadon.TrangThai = false;

                db.HoaDons.Add(_hoadon);
                Session["MaHD"] = _hoadon.MaHD;
                Session["MaHDC"] = _hoadon.MaHD;
                foreach (var item in cart)
                {
                    ChiTietHoaDon _cthd = new ChiTietHoaDon();
                    _cthd.MaHD = _hoadon.MaHD;
                    _cthd.MaSP = item.MaSP;
                    _cthd.SoLuong = item.SLuong;
                    _cthd.Gia = item.Gia;

                    db.ChiTietHoaDons.Add(_cthd);
                    CDKey _dscdk = new CDKey();
                    _dscdk.MaHD = _hoadon.MaHD;
                    _dscdk.MaSP = item.MaSP;
                    _dscdk.CDKey1 = CDKEY(16);

                    db.CDKeys.Add(_dscdk);

                    db.SaveChanges();
                }

                db.SaveChanges();
                return RedirectToAction("PaymentWithMomo", "Payment");
                cart.Clear();
                Session.Remove("MaHD");
                return RedirectToAction("Message", new { mess = "Thanh toán thành công" });
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
                return Content("Lỗi");
            }
        }

        public ActionResult XoaHDTB(string id)  //Xóa hóa đơn thanh toán thất bại
        {
            HoaDon hOADON = db.HoaDons.Find(id);
            db.HoaDons.Remove(hOADON);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Message(string mess)
        {
            ViewBag.Message = mess;
            return View();
        }

        string CDKEY(int numberRD)
        {
            string randomStr = "";
            string[] myIntArray = new string[numberRD];
            int x;
            Random autoRand = new Random();
            for (x = 0; x < numberRD; x++)
            {
                myIntArray[x] = Convert.ToChar(Convert.ToInt32(autoRand.Next(65, 87))).ToString();
                randomStr += (myIntArray[x].ToString());
            }
            return randomStr;
        }
    }
}
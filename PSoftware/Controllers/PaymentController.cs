using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSoftware.Models;
using PSoftware.MoMo;
using Newtonsoft.Json.Linq;

namespace PSoftware.Controllers
{
    public class PaymentController : Controller
    {
        private PSoftwareDB db = new PSoftwareDB();
        // GET: Payment
        public ActionResult PaymentWithMomo()
        {
            List<CartItem> cart = Session["cart"] as List<CartItem>;
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOO4DT20220605";
            string accessKey = "qcGi0nwvXhzQ09J2";
            string serectkey = "eak52lxTAqOHELgIUdcB2LWX342N3MU8";
            string orderInfo = "Thanh toán hóa đơn";
            string returnUrl = "https://localhost:11000/Payment/ReturnUrl/";
            string notifyUrl = "https://localhost:11000/Payment/NotifyUrl/";

            string amount = Convert.ToInt32(cart.Sum(x => x.ThanhTien)).ToString();
            string orderid = Session["MaHD"].ToString();
            string requestId = Session["MaKH"].ToString();
            string extraData = "";

            string rawHash = "partnerCode=" + partnerCode
                + "&accessKey=" + accessKey
                + "&requestId=" + requestId
                + "&amount=" + amount
                + "&orderId=" + orderid
                + "&orderInfo=" + orderInfo
                + "&returnUrl=" + returnUrl
                + "&notifyUrl=" + notifyUrl
                + "&extraData=" + extraData;
            MoMoSercurity crypto = new MoMoSercurity();
            string signature = crypto.signSHA256(rawHash, serectkey);
            JObject message = new JObject
            {
                {"partnerCode",partnerCode },
                {"accessKey",accessKey },
                {"requestId",requestId },
                {"amount",amount },
                {"orderId",orderid },
                {"orderInfo",orderInfo },
                {"returnUrl",returnUrl },
                {"notifyUrl",notifyUrl },
                {"requestType","captureMoMoWallet" },
                {"signature",signature }
            };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMomo);
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        public ActionResult ReturnUrl()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);
            MoMoSercurity crypto = new MoMoSercurity();
            string serectkey = "eak52lxTAqOHELgIUdcB2LWX342N3MU8";
            string signature = crypto.signSHA256(param, serectkey);
            if (signature != Request["signature"].ToString())
            {
                ViewBag.message = "Thông tin request không hợp lệ";
            }
            else if (!Request.QueryString["errorCode"].Equals("0"))
            {
                ViewBag.message = "Thanh toán thất bại";
            }
            else
            {
                Session.Remove("cart");
                Session.Remove("MaHD");
                ViewBag.message = "Thanh toán thành công";
            }
            return View();
        }

        public JsonResult NotifyUrl()
        {
            string param = "";
            param = "partner_code=" + Request["partner_code"]
                + "&access_key=" + Request["access_key"]
                + "&amount=" + Request["amount"]
                + "&order_id=" + Request["order_id"]
                + "&order_info=" + Request["orderInfo"]
                + "&order_type=" + Request["order_type"]
                + "&transaction_id=" + Request["transaction_id"]
                + "&message=" + Request["message"]
                + "&response_time=" + Request["response_time"]
                + "&status_code=" + Request["status_code"];
            param = Server.UrlDecode(param);
            MoMoSercurity crypto = new MoMoSercurity();
            string serectkey = "eak52lxTAqOHELgIUdcB2LWX342N3MU8";
            //Thành công = 1
            //Thất bại = 0
            //Chờ thanh toán = -1
            string signature = crypto.signSHA256(param, serectkey);
            if (signature != Request["signature"].ToString())
            {
                //Fail
            }
            string status_code = Request["status_code"].ToString();
            if (status_code != "0")
            {
                //Fail
            }
            else
            {
                //Success
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
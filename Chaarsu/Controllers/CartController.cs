﻿using Chaarsu.Models;
using Chaarsu.Models.ViewModel;
using Chaarsu.Repository.GRepository;
using Chaarsu.Repository.Interface;
using Chaarsu.Repository.SPRepository;
using Chaarsu.Repository.UnitofWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaarsu.Controllers
{
    public class CartController : Controller
    {
        anytimea_GROCERYEntities DbEntities = new anytimea_GROCERYEntities();

        private GenericRepository<PRODUCT> _PRODUCTS;
        private GenericRepository<CART> _CART;
        private GenericRepository<PAYMENT_MODES> _PAYMENT_MODES;
        private GenericRepository<ORDER> _ORDER;
        private GenericRepository<ORDER_PRODUCTS> _ORDER_PRODUCTS;
        private GenericRepository<BRANCH> _BRANCHES;
        private GenericRepository<BARCODE> _BARCODES;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SpRepository _sp;
        public CartController()
        {
            _unitOfWork = new UnitOfWork();
            _sp = new SpRepository();
        }

        // GET: Cart

        [HttpGet]
        public JsonResult ProcessCheckOut()
        {
            if (Session["UserSession"] != null || Session["GuestSession"] != null)
            {
                return Json(new { Status = true, RetMessage = "User Found" }, JsonRequestBehavior.AllowGet);
                //return View();

            }
            else
            {
                return Json(new { Status = false, RetMessage = "User Not Found" }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("SignIn", "Accounts");
            }
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            return View();
        }

        public int GetBranchId(double Lang, double Lat)
        {
            double EarthRadius = 6400000.0, minD = 6400000.0;
            int nearestBranchId = 0;
            double bLat = 0;
            double bLong = 0;
            _BRANCHES = new GenericRepository<BRANCH>(_unitOfWork);
            List<BRANCH> branches = _BRANCHES.Repository.GetAll();
            foreach (var row in branches)
            {
                bLat = row.LATITUDE ?? 0;
                bLong = row.LONGITUDE ?? 0;

                Double latDistance = DegreeToRadian(bLat - Lat);
                Double lonDistance = DegreeToRadian(bLong - Lang);
                Double a = Math.Sin(latDistance / 2) * Math.Sin(latDistance / 2)
                        + Math.Cos(DegreeToRadian(Lat)) * Math.Cos(DegreeToRadian(bLat))
                        * Math.Sin(lonDistance / 2) * Math.Sin(lonDistance / 2);
                Double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                Double distance = EarthRadius * c;
                if (distance < minD)
                {
                    minD = distance;
                    nearestBranchId = row.BRANCH_ID;
                }
            }

            return nearestBranchId;
        }
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }


        //try
        //{

        //    _BRANCHES = new GenericRepository<BRANCH>(_unitOfWork);
        //    var data = _BRANCHES.Repository.GetAll(x => x.LATITUDE== Convert.ToInt32(Lat) && Convert.ToInt32(x.LONGITUDE) == Convert.ToInt32(Lang));
        //    return Json(new { Status = true, response = data }, JsonRequestBehavior.AllowGet);
        //}
        //catch (Exception ex)
        //{
        //    return Json(new { Status = false, RetMessage = ex.Message }, JsonRequestBehavior.AllowGet);
        //}





        [HttpGet]
        public JsonResult GetPaymentModes()
        {
            try
            {
                _PAYMENT_MODES = new GenericRepository<PAYMENT_MODES>(_unitOfWork);
                var data = _PAYMENT_MODES.Repository.GetAll();
                return Json(new { Status = true, response = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult InsertOrder(InsertOrderWithBranchVM order)
        {
            try
            {
                var bb = Session["BranchId"];

                var UserId = 0;
                string guestUserName = "";
                if (Session["GuestSession"] != null)
                {
                    guestUserName = Session["GuestSession"].ToString();
                }
                if (Session["UserSession"] != null)
                {
                    UserId = Convert.ToInt32(Session["UserSession"]);
                }
                _CART = new GenericRepository<CART>(_unitOfWork);
                _ORDER = new GenericRepository<ORDER>(_unitOfWork);
                var data = new ORDER()
                {
                    CUSTOMER_ID = order.CUSTOMER_ID,
                    NAME = order.NAME,
                    MOBILE = order.MOBILE,
                    ADDRESS = order.ADDRESS,
                    PAYMENT_MODE_ID = order.PAYMENT_MODE_ID,
                    DELIVERY_DESCRIPTION = order.DELIVERY_DESCRIPTION
                };

                if (Session["BranchId"] != null)
                {
                    //data.BRANCH_ID = GetBranchId(order.LONGITUDE, order.LATITUDE);
                    data.BRANCH_ID = Convert.ToInt32(Session["BranchId"]);
                }

                if (Session["UserSession"] != null)
                {
                    data.CUSTOMER_ID = UserId;
                }
                else
                {
                    data.CUSTOMER_ID = null;
                }
                data.STATUS = 1;
                data.CREATED_ON = DateTime.Now;
                DateTime orderDate = data.CREATED_ON;
                data.CREATED_BY = UserId;
                data.IS_ACTIVE = 1;
                _ORDER.Repository.Add(data);
                var orderId = data.ORDER_ID;

                sendOrderSms($"Your order {orderId} has been Placed.", data.MOBILE);

                return Json(new { Status = true, OrderId = orderId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult InsertOrderProducts(List<ORDER_PRODUCTS> orderProductsList)
        {
            try
            {
                _ORDER_PRODUCTS = new GenericRepository<ORDER_PRODUCTS>(_unitOfWork);
                _BARCODES = new GenericRepository<BARCODE>(_unitOfWork);

                foreach (var item in orderProductsList)
                {
                    //var barcode = _BARCODES.Repository.GetAll().FirstOrDefault(x => x.ITEM_CODE == item.PRODUCT_ID && x.bDEFAULT == true && x.IsActive == true);
                    //item.BAR_CODE = barcode.BAR_CODE;
                    _ORDER_PRODUCTS.Repository.Add(item);
                }
                return Json(new { Status = true, RetMessage = "Your Order Submit Successfully. Thank You" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void sendOrderSms(string msg, string num)
        {
            string username = "923183183341";
            string pass = "Zong@123";
            string destinationnum = num;
            string masking = "SachiChakki";
            string text = msg;
            string language = "English";
            int msgCount = 0;
            //start sending SMS on request
            if (msg != string.Empty)
            {
                GenerateSMSAlert(masking, destinationnum, msg, username, pass);
            }

            //end sending SMS on request
        }
        #region Generate SMS

        public int GenerateSMSAlert(string Masking, string toNumber, string MessageText, string MyUsername, string MyPassword)
        {
            int count = 0;
            if (toNumber.Length == 12 && toNumber.Substring(0, 2).Equals("92"))
            {
                count++;
            }
            SendSMS(Masking, toNumber, MessageText, MyUsername, MyPassword);
            return count;
        }
        #endregion

        #region Send SMS

        public static string SendSMS(string Masking, string toNumber, string MessageText, string MyUsername, string MyPassword)
        {
            // OLD IMPLEMENTATION
            //string URI = @"http://api.bizsms.pk/api-send-branded-sms.aspx?username=" + MyUsername + "&pass=" + MyPassword +
            //    "&text=" + MessageText + "&masking=" + Masking + "&destinationnum=" + toNumber + "&language=English";

            //WebRequest req = WebRequest.Create(URI);
            //WebResponse resp = req.GetResponse();
            //var sr = new System.IO.StreamReader(resp.GetResponseStream());
            //return sr.ReadToEnd().Trim();

            SmsApiService.QuickSMSResquest quickSMSResquest = new SmsApiService.QuickSMSResquest()
            {
                loginId = MyUsername,
                loginPassword = MyPassword,
                Destination = toNumber,
                Mask = Masking,
                Message = MessageText,
                ShortCodePrefered = "n",
                UniCode = "0"
            };

            SmsApiService.BasicHttpBinding_ICorporateCBS client = new SmsApiService.BasicHttpBinding_ICorporateCBS();
            string message = client.QuickSMS(quickSMSResquest);
            return message;
        }
        #endregion
        [HttpGet]
        public ActionResult OrderSuccess()
        {
            if (Session["UserSession"] != null || Session["GuestSession"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("SignIn", "Accounts");
            }
        }

    }
}
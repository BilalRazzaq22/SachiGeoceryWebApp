using Chaarsu.Repository.Interface;
using Chaarsu.Repository.SPRepository;
using Chaarsu.Repository.UnitofWork;
using Chaarsu.Models;
using Chaarsu.Repository.GRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaarsu.Controllers
{
    public class UserController : Controller
    {
        private GenericRepository<USER> _USER;
        private GenericRepository<USER_ADDRESSES> _UserAddresses;
        private GenericRepository<ORDER_STATUSES> _OrderStatues;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SpRepository _sp;

        public UserController()
        {
            _unitOfWork = new UnitOfWork();
            _sp = new SpRepository();
        }

        // GET: User
        [HttpGet]
        public ActionResult Profile()
        {
            if (Session["UserSession"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SignIn", "Accounts", new { area = "" });
            }           
        }

        // GET: User
        [HttpGet]
        public ActionResult TrackOrder()
        {
            if (Session["UserSession"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SignIn", "Accounts", new { area = "" });
            }
        }


        [HttpGet]
        public JsonResult GetUserDetail()
        {
            try
            {
                _USER = new GenericRepository<USER>(_unitOfWork);
                var UserId = Convert.ToInt32(Session["UserSession"].ToString());
                var entity = _USER.Repository.Get(x => x.USER_ID == UserId);
                if (entity != null)
                {
                    return Json(entity, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Invalid", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetAddressList()
        {
            try
            {
                _UserAddresses = new GenericRepository<USER_ADDRESSES>(_unitOfWork);
                var UserId = Convert.ToInt32(Session["UserSession"].ToString());
                var entity = _UserAddresses.Repository.GetAll(x => x.USER_ID == UserId).ToList();
                if (entity != null)
                {
                    return Json(entity, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Invalid", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult UpdateUserDetail(string username, string email, string mobile, string address)
        {
            try
            {
                var UserId = Convert.ToInt32(Session["UserSession"].ToString());
                _USER = new GenericRepository<USER>(_unitOfWork);
                var emailExist = _USER.Repository.Get(x => x.USER_ID != UserId && x.EMAIL == email);
                var mobileExist = _USER.Repository.Get(x => x.USER_ID != UserId && x.MOBILE_NO == mobile);             
                if (emailExist != null)
                {
                    return Json(new { Status = false, RetMessage = "Email already exist" }, JsonRequestBehavior.AllowGet);
                }
                else if (mobileExist != null)
                {
                    return Json(new { Status = false, RetMessage = "Mobile number already exist" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var entity = _USER.Repository.Get(x => x.USER_ID == UserId);
                    entity.USERNAME = username;
                    entity.EMAIL = email;
                    entity.MOBILE_NO = mobile;
                    entity.ADDRESS = address;
                    entity.UPDATED_BY = UserId;
                    entity.UPDATED_ON = DateTime.Now;
                    _USER.Repository.Update(entity);
                    return Json(new { Status = true, RetMessage = "Your account detail updated successfully" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ChangePassword(string OldPassword, string NewPassword)
        {
            try
            {
                _USER = new GenericRepository<USER>(_unitOfWork);
                var UserId = Convert.ToInt32(Session["UserSession"].ToString());
                var entity = _USER.Repository.Get(x => x.USER_ID == UserId && x.PASSWORD == OldPassword);
                if (entity != null)
                {
                   entity.PASSWORD = NewPassword;
                   entity.UPDATED_BY = UserId;
                   entity.UPDATED_ON = DateTime.Now;
                   _USER.Repository.Update(entity);
                   Session.RemoveAll();
                   Session.Abandon();
                   return Json(new { Status = true, RetMessage = "Your password changed successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                   return Json(new { Status = false, RetMessage = "Your old password is invalid" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetAllOrders(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText, int OrderStatusId)
        {
            try
            {
                if (Session["UserSession"] == null)
                {
                    return Json("AccessDenied", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var CustomerId = Convert.ToInt32(Session["UserSession"].ToString());
                    var orders = _sp.GetAllOrders(PageIndex, PageSize, SortColumn, SortOrder, SearchText, OrderStatusId, CustomerId);
                    if (orders == null)
                    {
                        return Json("AccessDenied", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        return Json(orders, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

    }
}
using Chaarsu.Repository.Interface;
using Chaarsu.Repository.SPRepository;
using Chaarsu.Repository.UnitofWork;
using Chaarsu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chaarsu.Repository.GRepository;
using System.Net.Mail;
using System.Net;
using System.Reactive.Subjects;
using System.Web.Services.Description;
using System.Configuration;

namespace Chaarsu.Controllers
{
    public class AccountsController : Controller
    {
        private GenericRepository<USER> _USER;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SpRepository _sp;
        public AccountsController()
        {
            _unitOfWork = new UnitOfWork();
            _sp = new SpRepository();
        }

        // GET: Accounts

        [HttpGet]
        public ActionResult SignUp()
        {
            if (Session["UserSession"] != null)
            {
                return RedirectToAction("Profile", "User");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            if (Session["UserSession"] != null)
            {
                return RedirectToAction("Profile", "User");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword(string ReferenceNo)
        {
            _USER = new GenericRepository<USER>(_unitOfWork);
            var user = _USER.Repository.Get(x => x.ResetPasswordCode == ReferenceNo);
            if (user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = ReferenceNo;
                return View(model);
            }
            else
            {
                return RedirectToAction("SignIn", "Accounts");
            }
          
            
        }

        [HttpGet]
        public JsonResult signInUser(string EmailOrmobile, string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(EmailOrmobile))
                {
                    return Json(new { Status = false, RetMessage = "Email / Mobile No is required" },JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    return Json(new { Status = false, RetMessage = "Password is required" },JsonRequestBehavior.AllowGet);
                }
                else
                {                  
                    _USER = new GenericRepository<USER>(_unitOfWork);
                    var entity = _USER.Repository.Get(x => x.PASSWORD == Password && x.IS_ACTIVE == true && (x.EMAIL == EmailOrmobile || x.MOBILE_NO == EmailOrmobile));
                    if (entity != null)
                    {
                        Session.Remove("GuestSession");
                        Session["UserSession"] = entity.USER_ID.ToString();
                        return Json(new { Status = true, RetMessage = "You are SignIn Successfully" },JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Status = false, RetMessage = "Invalid email/mobile number or password" },JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult signUpUser(USER user)
        {
            try
            {
                _USER = new GenericRepository<USER>(_unitOfWork);
                if (user.IS_GUEST==true)
                {
                    Random r = new Random();
                    int random = r.Next();
                    string randomString = Convert.ToString(random);
                    user.MOBILE_NO = randomString + " " + user.USERNAME;
                    user.EMAIL = randomString + " " + user.USERNAME;
                    user.PASSWORD = randomString + " " + user.USERNAME;
                    user.ADDRESS = randomString + " " + user.USERNAME;
                    user.CREATED_ON = DateTime.Now;
                    user.USER_TYPE = 9;
                    user.IS_ACTIVE = true;
                    _USER.Repository.Add(user);
                    Session["GuestSession"] = user.USER_ID.ToString();
                    return Json(new { Status = true, RetMessage = "You are signin successfully as guest" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (string.IsNullOrEmpty(user.MOBILE_NO))
                    {
                        return Json(new { Status = false, RetMessage = "Mobile no is required" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (string.IsNullOrEmpty(user.USERNAME))
                    {
                        return Json(new { Status = false, RetMessage = "Username is required" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (string.IsNullOrEmpty(user.ADDRESS))
                    {
                        return Json(new { Status = false, RetMessage = "Address is required" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (string.IsNullOrEmpty(user.PASSWORD))
                    {
                        return Json(new { Status = false, RetMessage = "Password is required" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var entity = _USER.Repository.Get(x => x.MOBILE_NO == user.MOBILE_NO);
                        if (entity != null)
                        {
                            return Json(new { Status = false, RetMessage = "Mobile no already exist" }, JsonRequestBehavior.AllowGet);
                        }

                        if (!string.IsNullOrEmpty(user.EMAIL))
                        {
                            entity = _USER.Repository.Get(x => x.EMAIL == user.EMAIL);
                            if (entity != null)
                            {
                                return Json(new { Status = false, RetMessage = "Email already exist" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        user.CREATED_ON = DateTime.Now;
                        user.USER_TYPE = 3;
                        user.IS_ACTIVE = true;
                        _USER.Repository.Add(user);
                        var GuestId = user.USER_ID;

                        return Json(new { Status = true, RetMessage = "You are Sign Up Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message },JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult SignOut()
        {
            try
            {
                Session.RemoveAll();
                Session.Abandon();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult PasswordForgot(string email)
        {
            try
            {
                _USER = new GenericRepository<USER>(_unitOfWork);
                string message = "";
                bool status = false;
                var account = _USER.Repository.Get(x => x.EMAIL == email && x.IS_ACTIVE == true);
                if(account!=null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.EMAIL, resetCode);
                    account.ResetPasswordCode = resetCode;
                    _USER.Repository.Update(account);
                    return Json(new { Status = true, RetMessage = "Please Check Your Email<br/>Reset password link has been sent to your email." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Status = false, RetMessage = "Email not found" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if(ModelState.IsValid)
            {
                _USER = new GenericRepository<USER>(_unitOfWork);
                var user = _USER.Repository.Get(x => x.ResetPasswordCode == model.ResetCode);
                if(user==null)
                {
                    message = "Invalid";
                }
                else
                {
                    user.PASSWORD = model.NewPassword;
                    user.ResetPasswordCode = null;
                    _USER.Repository.Update(user);
                    message = "success";
                }         
            }
            else
            {
                message = "Invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Accounts/ResetPassword?ReferenceNo=" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress(ConfigurationManager.AppSettings["email"], "Sachi Grocery");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = ConfigurationManager.AppSettings["password"];
            string subject = "Reset Password";
            string body = "Hi,<br/><br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "is successfully created. Please click on the below link to verify your account" +
                    "<br/><br/><a href='" + link + "'>Reset Password Link</a>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromEmail.Address,fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            smtp.Send(message);
        }

        
    }
}
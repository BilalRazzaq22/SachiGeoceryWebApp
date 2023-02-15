using Chaarsu.Models;
using Chaarsu.Repository.GRepository;
using Chaarsu.Repository.Interface;
using Chaarsu.Repository.SPRepository;
using Chaarsu.Repository.UnitofWork;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Chaarsu.Controllers
{
    public class WishesController : Controller
    {
        anytimea_GROCERYEntities DbEntities = new anytimea_GROCERYEntities();

        private GenericRepository<USER> _USER;
        private GenericRepository<WISHES_PRODUCT> _WISHES;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SpRepository _sp;
        public WishesController()
        {
            _unitOfWork = new UnitOfWork();
            _sp = new SpRepository();
        }

        // GET: Wishes

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserSession"] == null)
            {
                return RedirectToAction("SignIn", "Accounts");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult GetWishList(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            try
            {
                if (Session["UserSession"] == null)
                {
                    return Json("AccessDenied", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var UserId = Convert.ToInt32(Session["UserSession"]);
                    var response = _sp.GetWishesProduct(PageIndex, PageSize, SortColumn, SortOrder, SearchText, UserId);
                    return Json(response, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Add(WISHES_PRODUCT wp)
        {
            try
            {
                if (Session["UserSession"] == null)
                {
                    return Json(new { Status = false, RetMessage = "Please first signin then you can add products in wish list" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var UserId = Convert.ToInt32(Session["UserSession"]);
                    _WISHES = new GenericRepository<WISHES_PRODUCT>(_unitOfWork);
                    var item = _WISHES.Repository.Get(x => x.PRODUCT_ID == wp.PRODUCT_ID && x.USER_ID == UserId);
                    if (item == null)
                    {
                        wp.USER_ID = UserId;
                        wp.CREATED_DATE = DateTime.Now;
                        _WISHES.Repository.Add(wp);
                        return Json(new { Status = true, RetMessage = "Product have been added to your wish list" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Status = false, RetMessage = "Product already exist in your wish list" }, JsonRequestBehavior.AllowGet);
                    }

                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            try
            {
                if (Session["UserSession"] == null)
                {
                    return Json("AccessDenied", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var UserId = Convert.ToInt32(Session["UserSession"]);
                    var entity = DbEntities.WISHES_PRODUCT.Where(x => x.PRODUCT_ID == id && x.USER_ID == UserId).FirstOrDefault();
                    DbEntities.WISHES_PRODUCT.Remove(entity);
                    DbEntities.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult TotalWishesItems()
        {
            try
            {
                var UserId = Convert.ToInt32(Session["UserSession"]);
                var totalItems = DbEntities.WISHES_PRODUCT.Where(x => x.USER_ID == UserId).Count();
                return Json(totalItems, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }
    }
}
using Chaarsu.Business;
using Chaarsu.DBManager;
using Chaarsu.Models;
using Chaarsu.Repository.GRepository;
using Chaarsu.Repository.Interface;
using Chaarsu.Repository.SPRepository;
using Chaarsu.Repository.UnitofWork;
using Chaarsu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaarsu.Controllers
{
    //[OutputCache(Duration = 60, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
    public class HomeController : Controller
    {
        private readonly IHomeBLL _homeBLL;
        private GenericRepository<CompanyInfo> _CompanyInfo;
        private GenericRepository<BRANCH> _BRANCHES;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SpRepository _sp;
        public HomeController(IHomeBLL homeBLL)
        {
            _homeBLL = homeBLL;
            _unitOfWork = new UnitOfWork();
            _sp = new SpRepository();
        }

        public ActionResult Index()
        {
            int BranchId = 0;
            if (Session["BranchId"] != null)
            {
                BranchId = Convert.ToInt32(Session["BranchId"]);
            }
            QuickQueries _dbmanager = new QuickQueries();
            var homeProducts = _dbmanager.GetHomeProducts(1, BranchId);
            var homeBlogs = _dbmanager.GetHomeBlogs();
            var homeReviews = _dbmanager.GetHomeProductReviews();
            var homeBnners = _dbmanager.GetAllBannersHome();
            HomeRootModel objHome = new HomeRootModel();
            objHome._ModelHomeBanner = homeBnners;
            objHome._ModelHomeBlogs = homeBlogs;
            objHome._ModelHomeProduct = homeProducts;
            objHome._ModelProductReviewHome = homeReviews;
            objHome._HeaderCategories= _homeBLL.GetHeaderCategories();

            return View(objHome);
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public JsonResult GetHeaderCategories()
        {
            try
            {
                var response=_homeBLL.GetHeaderCategories();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetGroupAndCategories()
        {
            try
            {
                var response = _homeBLL.GetGroupCategories();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

        //[HttpGet]
        //public JsonResult GetLandingPageData(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText, string CategoryId, string SubCategoryId, string GroupId)
        //{
        //    try
        //    {
              

        //        return Json(objHome, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Status = false, RetMessage = ex.Message });
        //    }
        //}

        [HttpGet]
        public JsonResult GetCompanyInfo(double lang, double lot)
        {
            try
            {
                int branchid = GetBranchId(lang, lot);
                Session["BranchId"] = branchid;

                _CompanyInfo = new GenericRepository<CompanyInfo>(_unitOfWork);
                var response  = _CompanyInfo.Repository.GetAll().FirstOrDefault();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
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

    }
}
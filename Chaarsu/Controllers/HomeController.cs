using Chaarsu.Business;
using Chaarsu.Models;
using Chaarsu.Models.ViewModel;
using Chaarsu.Repository.DBManager;
using Chaarsu.Repository.GRepository;
using Chaarsu.Repository.Interface;
using Chaarsu.Repository.SPRepository;
using Chaarsu.Repository.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Chaarsu.Controllers
{
    //[OutputCache(Duration = 60, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Server)]
    public class HomeController : Controller
    {
        private readonly IHomeBLL _homeBLL;
        //private GenericRepository<CompanyInfo> _CompanyInfo;
        //private GenericRepository<BRANCH> _BRANCHES;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly SpRepository _sp;
        public HomeController(IHomeBLL homeBLL)
        {
            _homeBLL = homeBLL;
            //_unitOfWork = new UnitOfWork();
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
            if (MyCollection.Instance.ModelHomeProducts == null)
                MyCollection.Instance.ModelHomeProducts = _dbmanager.GetHomeProducts(1, BranchId);

            if (MyCollection.Instance.ModelHomeBlogs == null)
                MyCollection.Instance.ModelHomeBlogs= _dbmanager.GetHomeBlogs();

            if (MyCollection.Instance.ModelProductReviewHomes == null)
                MyCollection.Instance.ModelProductReviewHomes = _dbmanager.GetHomeProductReviews();

            if (MyCollection.Instance.ModelHomeBanners == null)
                MyCollection.Instance.ModelHomeBanners = _dbmanager.GetAllBannersHome();

            if (MyCollection.Instance.Branches == null)
                MyCollection.Instance.Branches = _dbmanager.GetAllBranches();

            HomeRootModel objHome = new HomeRootModel();
            objHome._ModelHomeBanner = MyCollection.Instance.ModelHomeBanners;
            objHome._ModelHomeBlogs = MyCollection.Instance.ModelHomeBlogs;
            objHome._ModelHomeProduct = MyCollection.Instance.ModelHomeProducts;
            objHome._ModelProductReviewHome = MyCollection.Instance.ModelProductReviewHomes;
            objHome._HeaderCategories = _homeBLL.GetHeaderCategories();

            return View(objHome);
        }

        [HttpGet]
        public ActionResult About()
        {
            var a = Session["Longitude"];
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
                var response = _homeBLL.GetHeaderCategories();
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
                QuickQueries sq = new QuickQueries();
                if (Session["BranchId"] == null)
                {
                    //int branchid = GetBranchId(lang, lot);
                    //Session["BranchId"] = branchid;
                }
                // _CompanyInfo = new GenericRepository<CompanyInfo>(_unitOfWork);
                //var response  = _CompanyInfo.Repository.GetAll().FirstOrDefault();
                var response = sq.GetCompanyInfo().FirstOrDefault();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

        public JsonResult GetBranchId(double Lang, double Lat)
        {
            if (Session["BranchId"] == null || Convert.ToInt32(Session["BranchId"]) == 0)
            {
                int nearestBranchId = 0;
                double EarthRadius = 6400000.0, minD = 6400000.0;
                double bLat = 0;
                double bLong = 0;
                //_BRANCHES = new GenericRepository<BRANCH>(_unitOfWork);
                List<BRANCH> branches = MyCollection.Instance.Branches;
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
                Session["BranchId"] = nearestBranchId;
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

    }
}
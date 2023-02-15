﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chaarsu.Business;
using Chaarsu.Library;
using Chaarsu.Library.DataTableExtension;
using Chaarsu.Models;
using Chaarsu.Models.ViewModel;
using Chaarsu.Repository.GRepository;
using Chaarsu.Repository.Interface;
using Chaarsu.Repository.SPRepository;
using Chaarsu.Repository.UnitofWork;

namespace Chaarsu.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHomeBLL _homeBLL;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SpRepository _sp;
        private GenericRepository<PRODUCT_REVIEWS> _PRODUCT_REVIEWS;
        public ProductsController(IHomeBLL homeBLL)
        {
            _unitOfWork = new UnitOfWork();
            _sp = new SpRepository();
            _homeBLL = homeBLL;
        }
       
        // GET: Products
        [HttpGet]
        public ActionResult Index()
        {
            var response = _homeBLL.LoadCategoreis ();
           

            return View(response);
        }

        // GET: Detail
        [HttpGet]
        public ActionResult Detail(string productNameUrl)
        {
            ViewBag.ProductNameUrl = productNameUrl;
            return View();
        }

        [HttpGet]
        public JsonResult GetAllProducts(int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText, string CategoryId, string SubCategoryId, string GroupId)
        {
            try
            {
                int BranchId = 0;
                if (Session["BranchId"] != null)
                {
                    BranchId = Convert.ToInt32(Session["BranchId"]);
                }

                var response = _sp.SpGetAllProducts(PageIndex, PageSize, SortColumn, SortOrder, SearchText, CategoryId, SubCategoryId, GroupId, BranchId).ToList();
                return Json(new { Status = true, response }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }
        
        [HttpGet]
        public JsonResult GetProductDetail(string productNameUrl)
        {
            try
            {
                int BranchId = 0;
                if (Session["BranchId"] != null)
                {
                    BranchId = Convert.ToInt32(Session["BranchId"]);
                }

                Dictionary<string, object> response = new Dictionary<string, object>();
                var productDetail = _sp.GetProductDetailByProductNameUrl(productNameUrl,BranchId);
                if(productDetail==null)
                {
                    return Json(new { Status = false, RetMessage = productNameUrl + "of detail not exist." });
                }
                else
                {
                    response.Add("ProductDetail", productDetail);
                    int productId = productDetail.PRODUCT_ID;
                    var productImages = _sp.GetProductImagesById(productId);
                    response.Add("ProductImages", productImages);
                    return Json(new { Status = true, response }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetAllProductReviews(int PRODUCT_ID, int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            try
            {
                Dictionary<string, object> response = new Dictionary<string, object>();
                var productReviews = _sp.GetProductReviewsById(PRODUCT_ID, PageIndex, PageSize, SortColumn, SortOrder, SearchText);
                response.Add("ProductReviews", productReviews);
                return Json(new { Status = true, response }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AddReviews(PRODUCT_REVIEWS pRODUCT_REVIEWS)
        {
            try
            {
                var UserId = Convert.ToInt32(Session["UserSession"].ToString());
                _PRODUCT_REVIEWS = new GenericRepository<PRODUCT_REVIEWS>(_unitOfWork);
                pRODUCT_REVIEWS.USER_ID = UserId;
                pRODUCT_REVIEWS.CREATED_DATE = DateTime.Now;
                pRODUCT_REVIEWS.RECORD_STATUS = "Active";
                _PRODUCT_REVIEWS.Repository.Add(pRODUCT_REVIEWS);
                return Json(new { Status = true, RetMessage= "Submited Your Product Review. Thank You" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

    }
}
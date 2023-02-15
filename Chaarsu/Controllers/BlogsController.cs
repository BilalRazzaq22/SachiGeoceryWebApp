using Chaarsu.Repository.Interface;
using Chaarsu.Repository.SPRepository;
using Chaarsu.Repository.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaarsu.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SpRepository _sp;
        public BlogsController()
        {
            _unitOfWork = new UnitOfWork();
            _sp = new SpRepository();
        }

        // GET: Blog
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllBlogs (int PageIndex, int PageSize, string SortColumn, string SortOrder, string SearchText)
        {
            try
            {
                var allBlogs = _sp.SpGetAllBlogs(PageIndex, PageSize, SortColumn, SortOrder, SearchText);
                return Json(new { Status = true, allBlogs }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetBlogDetail(string blogTitleUrl)
        {
            try
            {
                Dictionary<string, object> response = new Dictionary<string, object>();
                var blogDetail = _sp.GetBlogDetailByBlogTitleUrl(blogTitleUrl);
                response.Add("BlogDetail", blogDetail);
                var recentBlogs = _sp.SpGetAllBlogs(1, 3, "CreatedDate", "desc", "");
                response.Add("recentBlog", recentBlogs);
                return Json(new { Status = true, response }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, RetMessage = ex.Message });
            }
        }

    }
}
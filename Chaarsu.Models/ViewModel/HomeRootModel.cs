using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Models.ViewModel
{
    public class HomeRootModel
    {
        public List<ModelHomeProduct> _ModelHomeProduct { get; set; }
        public List<ModelHomeBlogs> _ModelHomeBlogs { get; set; }
        public List<ModelProductReviewHome> _ModelProductReviewHome { get; set; }
        public List<ModelHomeBanner> _ModelHomeBanner { get; set; }
        public List<GROUP> _HeaderCategories { get; set; }
    }
    public class ModelHomeProduct
    {
        public int PRODUCT_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<decimal> PRICE1 { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public Nullable<int> PRICE2 { get; set; }
        public string COLOR { get; set; }
        public string BRAND { get; set; }
        public string FLAVOR { get; set; }
        public string PACKING { get; set; }
        public string TAG { get; set; }
        public string PRODUCT_NAME_URL { get; set; }
        public string CategoryName { get; set; }
        public string IMAGE_THUMBNAIL_PATH { get; set; }
    }

    public class ModelHomeBlogs
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogHtml { get; set; }
        public string BlogImageUrl { get; set; }
        public string BlogTitleUrl { get; set; }
        public string CreatedDateString { get; set; }
    }

    public class ModelProductReviewHome
    {
        public long PRODUCT_REVIEW_ID { get; set; }
        public string USERNAME { get; set; }
        public string IMAGE_PATH { get; set; }
        public string NAME { get; set; }
        public Nullable<int> REVIEW { get; set; }
        public string COMMENT { get; set; }
        public string DateString { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
    }

    public class ModelHomeBanner
    {
        public string ImageUrl { get; set; }
        public string AdminImagePath { get; set; }
        public string BannerTitle { get; set; }
        public string Description { get; set; }
    }
}

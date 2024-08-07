﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Models.ViewModel
{
    public class GetAllProducts
    {
        public List<ProductDetail> products { get; set; }
    }

    public class ProductDetail
    {
        public int PRODUCT_ID { get; set; }
        public Nullable<int> OLD_PRODUCT_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<int> SUB_CATEGORY_ID { get; set; }
        public string UNIT_OF_MEASUREMENT { get; set; }
        public Nullable<decimal> AVG_COST { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public Nullable<int> DISCOUNT_PERCENTAGE { get; set; }
        public Nullable<bool> IMPORTED { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public Nullable<int> MODIFIED_BY { get; set; }
        public string COLOR { get; set; }
        public string BRAND { get; set; }
        public string FLAVOR { get; set; }
        public string PACKING { get; set; }
        public string TAG { get; set; }
        public string TYPES { get; set; }
        public Nullable<double> DISCOUNT { get; set; }
        public Nullable<int> SEC_SUB_CATEGORY_A { get; set; }
        public Nullable<int> SEC_SUB_CATEGORY_B { get; set; }
        public Nullable<int> PRICE2 { get; set; }
        public Nullable<bool> HAS_IMAGE { get; set; }
        public Nullable<bool> HAS_THUMBNAIL_IMAGE { get; set; }
        public Nullable<int> VENDOR_ID { get; set; }
        public Nullable<int> CATEGORY_ID { get; set; }
        public string CategoryName { get; set; }
        public string IMAGE_PATH { get; set; }
        public string IMAGE_THUMBNAIL_PATH { get; set; }
        public Nullable<bool> IS_FEATURED { get; set; }
        public Nullable<int> THRESHOLD { get; set; }
        public Nullable<bool> IS_EXEMPTED { get; set; }
        public string PRODUCT_NAME_URL { get; set; }
        public Nullable<long> RowIndex { get; set; }
        public Nullable<int> TotalRecords { get; set; }
        public Nullable<decimal> TotalPages { get; set; }
        public Nullable<int> Start { get; set; }
        public Nullable<int> End { get; set; }
    }
}

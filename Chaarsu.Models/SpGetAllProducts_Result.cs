//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chaarsu.Models
{
    using System;
    
    public partial class SpGetAllProducts_Result
    {
        public int PRODUCT_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<decimal> PRICE1 { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public string BARCODE { get; set; }
        public Nullable<int> PRICE2 { get; set; }
        public string COLOR { get; set; }
        public string BRAND { get; set; }
        public string FLAVOR { get; set; }
        public string PACKING { get; set; }
        public string TAG { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string PRODUCT_NAME_URL { get; set; }
        public string CategoryName { get; set; }
        public string IMAGE_THUMBNAIL_PATH { get; set; }
        public Nullable<int> GroupID { get; set; }
        public Nullable<int> VENDOR_ID { get; set; }
        public Nullable<int> CATEGORY_ID { get; set; }
        public Nullable<int> SUB_CATEGORY_ID { get; set; }
        public Nullable<long> RowIndex { get; set; }
        public Nullable<int> TotalRecords { get; set; }
        public Nullable<decimal> TotalPages { get; set; }
        public Nullable<int> Start { get; set; }
        public Nullable<int> End { get; set; }
    }
}

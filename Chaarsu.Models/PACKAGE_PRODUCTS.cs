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
    using System.Collections.Generic;
    
    public partial class PACKAGE_PRODUCTS
    {
        public int PACKAGE_PRODUCT_ID { get; set; }
        public Nullable<int> PACKAGE_ID { get; set; }
        public Nullable<int> PRODUCT_ID { get; set; }
        public Nullable<int> QUANTITY { get; set; }
        public Nullable<int> IS_ACTIVE { get; set; }
        public Nullable<int> UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_ON { get; set; }
        public string BAR_CODE { get; set; }
    }
}
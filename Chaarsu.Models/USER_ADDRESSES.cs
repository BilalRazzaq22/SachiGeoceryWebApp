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
    
    public partial class USER_ADDRESSES
    {
        public int USER_ADDRESS_ID { get; set; }
        public Nullable<int> USER_ID { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_ON { get; set; }
        public Nullable<int> UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_ON { get; set; }
        public Nullable<int> BRANCH_ID { get; set; }
        public string LONGITUDE { get; set; }
        public string LATITUDE { get; set; }
    }
}

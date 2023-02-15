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
    
    public partial class ITEMINFO
    {
        public int ITEM_CODE { get; set; }
        public string HV_CODE { get; set; }
        public byte ITEM_ATTRIB { get; set; }
        public byte ITEM_TYPE { get; set; }
        public string UOM { get; set; }
        public decimal PACKING_UNITS { get; set; }
        public bool ISSUE_BY_WEIGHT { get; set; }
        public string ITEM_DESC { get; set; }
        public string ITEM_DESC_LONG { get; set; }
        public int SECTID { get; set; }
        public short DEPT_CODE { get; set; }
        public short GRCODE { get; set; }
        public short SUBGRCODE { get; set; }
        public int CATCODE { get; set; }
        public int BRAND_CODE { get; set; }
        public int DESIGN_CD { get; set; }
        public short CLR_CODE { get; set; }
        public short MAKE_CODE { get; set; }
        public int SIZE_CODE { get; set; }
        public int AUTH_CODE { get; set; }
        public int PUB_CODE { get; set; }
        public string EDITION { get; set; }
        public string CLASS { get; set; }
        public string ISBN { get; set; }
        public string SEX { get; set; }
        public string SEASON { get; set; }
        public string AGE { get; set; }
        public string FABRIC { get; set; }
        public bool IsPACKAGE { get; set; }
        public bool IsRECIPE { get; set; }
        public bool SERIALIZED { get; set; }
        public bool FEATURED { get; set; }
        public bool EXPIRY_ITEM { get; set; }
        public string COMMENT { get; set; }
        public bool EXEMPT { get; set; }
        public decimal VAT { get; set; }
        public decimal GST { get; set; }
        public decimal GST_SALES { get; set; }
        public byte GST_MODE { get; set; }
        public string GST_REF { get; set; }
        public string HS_CODE { get; set; }
        public decimal GROSS_WEIGHT { get; set; }
        public decimal NET_WEIGHT { get; set; }
        public bool OPEN_PRICE { get; set; }
        public bool FRACTIONAL { get; set; }
        public bool ALLOWQTY { get; set; }
        public decimal BATCH_QTY { get; set; }
        public decimal ITEM_DISC { get; set; }
        public bool bDISCOUNTED { get; set; }
        public decimal DISC_QTY { get; set; }
        public decimal SALE_DISC { get; set; }
        public bool bPrintOnBill { get; set; }
        public string DESCR { get; set; }
        public string IMAGE_PATH { get; set; }
        public string THUMBNAIL_PATH { get; set; }
        public bool IMPORTED { get; set; }
        public bool ShowOnWeb { get; set; }
        public bool bNEW { get; set; }
        public System.DateTime CDATE { get; set; }
        public int CUSER { get; set; }
        public System.DateTime MDATE { get; set; }
        public int MUSER { get; set; }
        public short CreatedAt { get; set; }
        public bool NeedsReplication { get; set; }
        public bool L1P1NeedsUpdation { get; set; }
        public bool L1P2NeedsUpdation { get; set; }
        public bool L1P3NeedsUpdation { get; set; }
        public bool L1P4NeedsUpdation { get; set; }
        public bool L1P5NeedsUpdation { get; set; }
        public bool L1P6NeedsUpdation { get; set; }
        public bool L1P7NeedsUpdation { get; set; }
        public bool L1P8NeedsUpdation { get; set; }
        public bool L1P9NeedsUpdation { get; set; }
        public bool L1P10NeedsUpdation { get; set; }
        public bool L2P1NeedsUpdation { get; set; }
        public bool L2P2NeedsUpdation { get; set; }
        public bool L2P3NeedsUpdation { get; set; }
        public bool L3P1NeedsUpdation { get; set; }
        public bool L3P2NeedsUpdation { get; set; }
        public bool L3P3NeedsUpdation { get; set; }
        public bool L3P4NeedsUpdation { get; set; }
        public bool L3P5NeedsUpdation { get; set; }
        public bool L3P6NeedsUpdation { get; set; }
        public bool L3P7NeedsUpdation { get; set; }
        public bool L3P8NeedsUpdation { get; set; }
        public bool L4P1NeedsUpdation { get; set; }
        public bool L5P1NeedsUpdation { get; set; }
        public bool L6P1NeedsUpdation { get; set; }
        public bool L4P2NeedsUpdation { get; set; }
        public bool L4P3NeedsUpdation { get; set; }
        public bool L5P3NeedsUpdation { get; set; }
        public bool L6P3NeedsUpdation { get; set; }
        public bool L5P2NeedsUpdation { get; set; }
        public bool L6P2NeedsUpdation { get; set; }
        public bool L1P11NeedsUpdation { get; set; }
        public bool L1P12NeedsUpdation { get; set; }
        public bool L3P9NeedsUpdation { get; set; }
        public bool L3P10NeedsUpdation { get; set; }
        public bool L4P4NeedsUpdation { get; set; }
        public bool L4P5NeedsUpdation { get; set; }
        public bool L5P4NeedsUpdation { get; set; }
        public bool L5P5NeedsUpdation { get; set; }
        public bool L6P4NeedsUpdation { get; set; }
        public bool L6P5NeedsUpdation { get; set; }
        public bool L7P1NeedsUpdation { get; set; }
        public bool L7P2NeedsUpdation { get; set; }
        public bool L7P3NeedsUpdation { get; set; }
        public bool L7P4NeedsUpdation { get; set; }
        public bool L7P5NeedsUpdation { get; set; }
        public bool L7P6NeedsUpdation { get; set; }
        public bool L7P7NeedsUpdation { get; set; }
        public bool L7P8NeedsUpdation { get; set; }
        public bool L2P4NeedsUpdation { get; set; }
        public bool L2P5NeedsUpdation { get; set; }
        public bool L2P6NeedsUpdation { get; set; }
        public bool L2P7NeedsUpdation { get; set; }
        public bool L2P8NeedsUpdation { get; set; }
        public bool L2P9NeedsUpdation { get; set; }
        public bool L2P10NeedsUpdation { get; set; }
        public bool L0NeedsUpdation { get; set; }
        public bool L1NeedsUpdation { get; set; }
        public bool L2NeedsUpdation { get; set; }
        public bool L3NeedsUpdation { get; set; }
        public bool L4NeedsUpdation { get; set; }
        public bool L5NeedsUpdation { get; set; }
        public bool L6NeedsUpdation { get; set; }
        public bool L4P6NeedsUpdation { get; set; }
        public bool L5P6NeedsUpdation { get; set; }
        public bool L6P6NeedsUpdation { get; set; }
        public bool L7NeedsUpdation { get; set; }
        public bool L8NeedsUpdation { get; set; }
        public bool L9NeedsUpdation { get; set; }
        public bool L10NeedsUpdation { get; set; }
        public bool L11NeedsUpdation { get; set; }
        public bool L12NeedsUpdation { get; set; }
        public bool L8P1NeedsUpdation { get; set; }
        public bool L9P1NeedsUpdation { get; set; }
        public bool L10P1NeedsUpdation { get; set; }
        public bool L11P1NeedsUpdation { get; set; }
        public bool L12P1NeedsUpdation { get; set; }
        public bool L8P2NeedsUpdation { get; set; }
        public bool L8p3NeedsUpdation { get; set; }
    }
}
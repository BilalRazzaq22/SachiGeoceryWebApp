using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Models.ViewModel
{
    public class InsertOrderWithBranchVM
    {
        public double LATITUDE { get; set; }
        public double LONGITUDE { get; set; }
        public int ORDER_ID { get; set; }
        public Nullable<int> CUSTOMER_ID { get; set; }
        public string NAME { get; set; }
        public string MOBILE { get; set; }
        public string ADDRESS { get; set; }
        public string DELIVERY_DESCRIPTION { get; set; }
        public Nullable<int> PAYMENT_MODE_ID { get; set; }
    }
}

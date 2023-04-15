using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Models.Dto
{
    public class RecommendedProductDto
    {
        public int RECOMMENDED_PRODUCT_ID { get; set; }
        public Nullable<int> PRODUCT_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public string PACKING { get; set; }
        public string CategoryName { get; set; }
        public string IMAGE_THUMBNAIL_PATH { get; set; }
        public string PRODUCT_NAME_URL { get; set; }
        public bool FavouriteProduct { get; set; }
    }
}

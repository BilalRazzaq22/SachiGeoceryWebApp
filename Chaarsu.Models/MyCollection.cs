using Chaarsu.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Models
{
    public class MyCollection
    {
        private static MyCollection _instance;
        private static object objLock = new object();
        public static MyCollection Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MyCollection();
                        }
                    }
                }
                return _instance;
            }
        }

        public List<ModelHomeProduct> ModelHomeProducts { get; set; }
        public List<ModelHomeBlogs> ModelHomeBlogs { get; set; }
        public List<ModelProductReviewHome> ModelProductReviewHomes { get; set; }
        public List<ModelHomeBanner> ModelHomeBanners { get; set; }
        public List<GROUP> Groups { get; set; }
        public List<CATEGORy> Categories { get; set; }
        public List<BRANCH> Branches { get; set; }
    }
}

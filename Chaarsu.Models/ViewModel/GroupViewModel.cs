using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Models.ViewModel
{
    public class GroupViewModel
    {
        
        public int GroupId { get; set; }
        public string Name { get; set; }
        public List<CATEGORy> listCategories { get; set; }
    }

}

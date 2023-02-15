using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Library.API
{
   public class AdminResponse
    {
        public bool Status { get; set; }
        public string RetMessage { get; set; }

        public string ID { get; set; }

        public AdminResponse()
        {
            Status = false;
            RetMessage = "There is some problem , please check..";
        }

    }
}

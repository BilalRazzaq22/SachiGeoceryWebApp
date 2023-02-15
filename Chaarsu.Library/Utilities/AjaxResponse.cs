using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Library.Utilities
{
    public class AjaxResponse
    {
        public bool IsValid { get; set; }
        public bool ShowMessage { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public AjaxResponse()
        {
        }
        public AjaxResponse(bool IsValid, string Message, bool ShowMessage = true, object Data = null)
        {
            this.IsValid = IsValid;
            this.Message = Message;
            this.ShowMessage = ShowMessage;
            this.Data = Data;
        }
    }
}

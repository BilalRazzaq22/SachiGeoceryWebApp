using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Models.InputModel
{
    public class InputUserModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<bool> AdminFlag { get; set; }
        public Nullable<bool> OnHold { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> TrnTime { get; set; }
        public string Operator { get; set; }
        public string LoginMAC { get; set; }
        public Nullable<System.DateTime> LoginTime { get; set; }
        public string IPAddress { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ImageUrl { get; set; }
        public string CurrentPassword { get; set; }
    }
}

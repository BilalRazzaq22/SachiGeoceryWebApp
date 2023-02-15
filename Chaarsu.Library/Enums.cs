using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Library
{
    public static class Enums
    {
        public enum RecordStatus
        {
            Active,
            InActive,
            PinConfirmed,
            Deleted,
            VerificationPending,
            Creating,
            Pending,
            Completed,
            Canceled,
            Paid,
            Blocked
        }

        public enum PropertyStatus
        {
            Active,
            InActive,
            PinConfirmed,
            Deleted,
            VerificationPending,
            Creating,
            Pending,
            Completed,
            Canceled,
            Paid,
            Blocked
        }
        public enum UserRole
        {
            Landlord,
            PropertyManager,
            Tenant,
            Vendor,
        }
        public enum UserApp
        {
            Main,
            Vendor
        }

        public enum ResponseCode
        {
            Success,
            Failure,
            Unauthorized,
            Exception,
            Info,
            NotExists
        }
        public enum RecordType
        {
            ServiceImage,
            ServiceVideo,
            VendorImage,
            VendorVideo,
            Invoice,
            Quotation
        }
        public enum MediaType
        {
            Image,
            Video
        }
        public enum ServiceType
        {
            ServiceRequest,
            TenantImprovement,
            Emergency
        }
        public enum ScheduleType
        {
            ASAP,
            Schedule,
            Emergency
        }
        public enum ServiceStatus
        {
            Available,
            Accepted,
            Quoted,
            PaymentRequired,
            Paid,
            NeedApproval,
            Preferred,
            Canceled
        }
        public enum VendorServiceStatus
        {
            Accepted,
            Quoted,
            PaymentRequest,
            Paid,
            Rejected,
            Preferred,
            PreferredExpired,
            Canceled,
            OnHold
        }
        public enum OrderStatus
        {
            Active,
            Production,
            Paid,
            Pending,
            Completed
        }
        public enum ContactStatus
        {
            WhatsApp,
            Website,
            Mobile,
            Email,
            Facebook
        }
    }
}

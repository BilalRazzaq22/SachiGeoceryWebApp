using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Library.Utilities
{
    public static class ResponseMessage
    {
        public const string Success = "SUCCESS";
        public const string NotFound = "NOT_FOUND";
        public const string LoggedOut = "LoggedOut";

        public const string User_already_exists_with_username = "User already exists with this User name.";
        public const string User_already_entered_inthis_contest = "User already entered in this Contest.";
        public const string User_already_exists_with_email = "User already exists with this Email.";
        public const string Exception_Something_Went_Wrong = "Oops! something went wrong, please try again later.";
        public const string Something_Went_Wrong = "Oops! something went wrong.";
        public const string Invalid_Password = "Username/Password is invalid.";
        public const string Invalid_onlyPassword = "Password is invalid.";
        public const string Invalid_Username = "Username/Password is invalid.";
        public const string Invalid_Email = "Email/Password is invalid.";
        public const string User_Not_Found = "User not found.";
        public const string Invalid_Request = "Request is invalid.";
        public const string PromoCode_Not_Valid = "Promo code is not valid.";
        public const string Signed_Up_Successfully = "Signed up successfully.";
        public const string Signed_In_Successfully = "Signed in successfully.";
        public const string Profile_Image_Updated = "Profile image updated successfully.";
        public const string Updated_Successfully = "Updated successfully.";
        public const string Saved_Successfully = "Saved successfully.";
        public const string Email_Sent_Successfully = "Email sent successfully. Thanks for your time.";
        public const string Contest_Created = "Contest created successfully.";
        public const string Comment_Created = "Comment created successfully.";
        public const string Delete_Successfully = "Deleted successfully.";
        public const string Amount_lessthen_Entryfee = "You don't have enough money for this entry.";
        public const string Amount_greaterthen_Entryfee = "You have money for the entry.";
        public const string Prediction_Add = "Successfully submit predictions.";
        public const string IsFacebookUser = "Facebook User.";
    }
}

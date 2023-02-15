using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaarsu.Library.Utilities
{
    public class LanguageSwitch
    {
        public readonly string culture = "en-US";
        public readonly string Success = "SUCCESS";
        public readonly string NotFound = "NOT_FOUND";
        public readonly string LoggedOut = "LoggedOut";

        public readonly string User_already_exists_with_username = "User already exists with this User name.";
        public readonly string User_already_entered_inthis_contest = "User already entered in this Contest.";
        public readonly string User_already_exists_with_email = "User already exists with this Email.";
        public readonly string Exception_Something_Went_Wrong = "Oops! something went wrong, please try again later.";
        public readonly string Something_Went_Wrong = "Oops! something went wrong.";
        public readonly string Invalid_Password = "Username/Password is invalid.";
        public readonly string Invalid_onlyPassword = "Password is invalid.";
        public readonly string Invalid_Username = "Username/Password is invalid.";
        public readonly string Invalid_Email = "Email/Password is invalid.";
        public readonly string User_Not_Found = "User not found.";
        public readonly string Invalid_Request = "Request is invalid.";
        public readonly string PromoCode_Not_Valid = "Promo code is not valid.";
        public readonly string Signed_Up_Successfully = "Signed up successfully.";
        public readonly string Signed_In_Successfully = "Signed in successfully.";
        public readonly string Profile_Image_Updated = "Profile image updated successfully.";
        public readonly string Updated_Successfully = "Updated successfully.";
        public readonly string Saved_Successfully = "Saved successfully.";
        public readonly string Email_Sent_Successfully = "Email sent successfully. Thanks for your time.";
        public readonly string Contest_Created = "Contest created successfully.";
        public readonly string Comment_Created = "Comment created successfully.";
        public readonly string Delete_Successfully = "Deleted successfully.";
        public readonly string Amount_lessthen_Entryfee = "You don't have enough money for this entry.";
        public readonly string Amount_greaterthen_Entryfee = "You have money for the entry.";
        public readonly string Prediction_Add = "Successfully submit predictions.";
        public readonly string IsFacebookUser = "Facebook User.";

        // MES Message Box
        public readonly string User_ID_Not_Found = "User ID not found";
        public readonly string Invalid_User_Password = "Inalid user password";
        public readonly string User_Id_Locked = "User ID is locked";
        public readonly string Invalid_Company = "Invalid company";
        public readonly string Invalid_Company_Password = "Invalid company password";
        public readonly string User_group_not_found = "User Group not found";
        public readonly string Security_Not_Assigned = "Security not assigned";
        public readonly string Current_password_is_wrong = "Current password is wrong.";
        public readonly string Password_changed_success = "Password changed successfully.";
        public readonly string User_Id_already_exist = "User Id already exist.";
        public readonly string Group_already_exist = "Group already exist.";
        public readonly string Database_not_fount = "Database not found.";
        public readonly string Company_already_exist = "Company already exist.";
        public LanguageSwitch(string language)
        {
            culture = language;
            if (language == "zh-Hunt")
            {
                
               
                User_ID_Not_Found = "用户不存在";
                Invalid_User_Password = "用户密码不正确";
                User_Id_Locked = "用户已锁定";
                Invalid_Company = "公司不正确";
                Invalid_Company_Password = "公司密码不正确";
                User_group_not_found = "用户组不存在";
                Security_Not_Assigned = "权限未分配";
                Current_password_is_wrong = "当前密码错误";
                Password_changed_success = "密码更换成功。";
                User_Id_already_exist = "用户已存在";
                Delete_Successfully = "成功删除";
                Saved_Successfully = "保存成功";
                Updated_Successfully = "更新成功";
                Group_already_exist = "群组已经存在";
                Database_not_fount = "找不到数据库";
                Company_already_exist = "公司已经存在";

               
            }
        }

    }
}

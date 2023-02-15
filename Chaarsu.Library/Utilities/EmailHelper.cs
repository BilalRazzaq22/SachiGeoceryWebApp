using Chaarsu.Library.Validations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Hosting;

namespace Chaarsu.Library.Utilities
{
    public static class EmailHelper
    {
        public static string SendEmail(string To, string Subject, string BodyText)
        {

            if (FValidations.IsValidEmail(To))
            {
                MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(), To, Subject, BodyText);
                MailAddress Reply = new MailAddress(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(), "relay", System.Text.Encoding.UTF8);
                mailMessage.ReplyTo = Reply;


                SmtpClient smtp = new SmtpClient();
                try
                {

                    mailMessage.IsBodyHtml = true;

                    smtp.Send(mailMessage);

                    return "Success";
                }
                catch (Exception ex)
                {
                    LogHelper.CreateLog(ex, ErrorType.Exception);
                    return "Error";
                }
                finally
                {
                    mailMessage.Dispose();
                    smtp = null;
                }
            }
            else
            {
                return "Invalid EmailAddress";
            }

        }

        public static async Task<string> SendEmailAsync(string To, string Subject, string BodyText)
        {
            return await Task.Factory.StartNew(() =>
            {
                if (FValidations.IsValidEmail(To))
                {
                    MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(), To, Subject, BodyText);
                    MailAddress Reply = new MailAddress(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(), "relay", System.Text.Encoding.UTF8);
                    mailMessage.ReplyTo = Reply;


                    SmtpClient smtp = new SmtpClient();
                    try
                    {

                        mailMessage.IsBodyHtml = true;

                        smtp.Send(mailMessage);

                        return "Success";
                    }
                    catch (Exception ex)
                    {
                        LogHelper.CreateLog(ex, ErrorType.Exception);
                        return "Error";
                    }
                    finally
                    {
                        mailMessage.Dispose();
                        smtp = null;
                    }
                }
                else
                {
                    return "Invalid EmailAddress";
                }
            });

        }


    }

    public static class StandardEmail
    {
        public static void ForgetPassword(string _ToUserName, string _Password, string _ToEmail)
        {
            string html = "<p>Hi " + _ToUserName + ",</p>";
            html += "<p><br><br>Thanks for using " + WebConfigurationManager.AppSettings["AppName"] + "!</p>";
            html += "<p><br>Your password is: " + _Password + "</p>";
            html += "<p><br>If you would like to change your password, please go to the profile area of the app.</p>";
            html += "<p><br>Thanks,</p>";
            html += "<p><br><strong>The " + WebConfigurationManager.AppSettings["AppName"] + " Team</strong></p>";

            EmailHelper.SendEmailAsync(_ToEmail, WebConfigurationManager.AppSettings["AppName"] + " : Forgot Password", html);

        }
        public static void SendPicCode(string _ToUserName, string Pincode, string _ToEmail, string _MoreHtml = "")
        {
            string html = "<p>Hi " + _ToUserName + ",</p>";
            html += "<p><br><br>Thanks for using " + WebConfigurationManager.AppSettings["AppName"] + "!</p>";
            html += "<p><br>Your pincode is : " + Pincode + "</p>";
            html += "<p><br>" + _MoreHtml + "</p>";
            html += "<p><br>Thanks,</p>";
            html += "<p><br><strong>The " + WebConfigurationManager.AppSettings["AppName"] + " Team</strong></p>";

            EmailHelper.SendEmailAsync(_ToEmail, WebConfigurationManager.AppSettings["AppName"] + " : Pincode", html);

        }

        public static void ContactUs(string _ToEmail, string _subject, string _body, string _SendInfor,string UserName,string UserApp)
        {
            string html = "<p>Hi " + WebConfigurationManager.AppSettings["AppName"] + " Team,</p>";
            html += "<p><br>User Name : "+ UserName + "</p>";
            html += "<p><br>Email : " + _SendInfor + "</p>";
            html += "<p><br>User App : " + UserApp + "</p>";
            html += "<p><br>Message!</p>";
            html += "<p><br>" + _body + "</p>";
            html += "<p><br>Thanks,</p>";

          EmailHelper.SendEmailAsync(_ToEmail, "Contact Us :" + _subject, html);

        }

        public static void PasswordUpdate(string _ToUserName, string _Password, string _ToEmail)
        {

            string html = "<p>Hi " + _ToUserName + ",</p>";
            html += "<p><br>Your password has been changed successfully.</p>";
            html += "<p><br>Thanks,</p>";
            html += "<p><br><strong>The " + WebConfigurationManager.AppSettings["AppName"] + " Team</strong></p>";

            EmailHelper.SendEmailAsync(_ToEmail, WebConfigurationManager.AppSettings["AppName"] + " : Password Changed", html);

        }

        public static void ForgetPassword_Admin(string _ToUserName, string _Password, string _ToEmail)
        {
            string html = "<p>Hi " + _ToUserName + "</p>";
            html += "<p>Here is the password : " + _Password + "</p>";
            html += "<p><br><br>Thanks for using " + WebConfigurationManager.AppSettings["AppName"] + "<br><strong>" + WebConfigurationManager.AppSettings["AppName"] + " team</strong></p>";


            EmailHelper.SendEmailAsync(_ToEmail, WebConfigurationManager.AppSettings["AppName"] + " : Forgot Password", html);

        }

        public static void StoreOrderConfirmation(string _ToUserName, string _OrderNo, string _ToEmail, string _MoreHtml = "")
        {
            string html = "<p>Hi " + _ToUserName + ",</p>";
            html += "<p><br><br>Thanks for using " + WebConfigurationManager.AppSettings["AppName"] + "!</p>";
            html += "<p><br>Your Order No is: " + _OrderNo + "</p>";
            html += "<p><br>" + _MoreHtml + "</p>";
            html += "<p><br>Thanks,</p>";
            html += "<p><br><strong>The " + WebConfigurationManager.AppSettings["AppName"] + " Team</strong></p>";

            EmailHelper.SendEmailAsync(_ToEmail, WebConfigurationManager.AppSettings["AppName"] + " : Order Confirmation #" + _OrderNo, html);

        }

        public static void InviteFrindEmail(string _ToUserName, string _FromUserName, string _ToEmail, string _MoreHtml = "")
        {
            string html = "<p>Hi " + _ToUserName + ",</p>";
            html += "<p><br><br>Thanks for using " + WebConfigurationManager.AppSettings["AppName"] + "!</p>";
            html += "<p><br>User : " + _FromUserName + " has send you the invitatino</p>";
            html += "<p><br>" + _MoreHtml + "</p>";
            html += "<p><br>Thanks,</p>";
            html += "<p><br><strong>The " + WebConfigurationManager.AppSettings["AppName"] + " Team</strong></p>";

            EmailHelper.SendEmailAsync(_ToEmail, WebConfigurationManager.AppSettings["AppName"] + " : Invitation to join MyTutorLab", html);

        }

        public static void MinorSignupEmail(string _ToUserName, string _FromUserName, string _ToEmail, string _MoreHtml = "")
        {
            string html = "<p>Hello " + _ToUserName + ",</p>";

            html += "<p><br>Your child, " + _FromUserName + ", has signed up as a student on the My Tutor Lab mobile app!</p>";

            html += "<p><br>If you are not already signed up as a parent on the My Tutor Lab app, please proceed to sign up now using the link below.</p>";
            html += "<p><br><a href='https://appserver.mytutorlab.com/content/upload/mytutorlab_app.html'>Sign up Now!</a></p>";
            html += "<p><br>NOTE : Students under the age of 18 cannot book sessions with a tutor without the parent's consent. You, as the parent, are responsible to make payments and manage your student's account. </p>";
            html += "<p><br>If you have any questions or concerns, please do not hesitate to  <a href='mailto:support@mytutorlab.com'>contact us.</a>.</p>";
            html += "<p><br>We thank you for your interest in My Tutor Lab and look forward to hearing from you soon!</p>";

            html += "<p><br>Sincerely,</p>";
            html += "<p><br><strong>The " + WebConfigurationManager.AppSettings["AppName"] + " Team</strong></p>";

            EmailHelper.SendEmailAsync(_ToEmail, WebConfigurationManager.AppSettings["AppName"] + " : Student (minor) signup", html);

        }

    }

}

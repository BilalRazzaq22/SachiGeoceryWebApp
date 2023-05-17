using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chaarsu
{
    public class SMSManager
    {
        public static void sendOrderSms(string msg, string num)
        {
            string username = "923183183341";
            string pass = "Zong@123";
            string destinationnum = num;
            string masking = "SachiChakki";
            string text = msg;
            string language = "English";
            int msgCount = 0;
            //start sending SMS on request
            if (msg != string.Empty)
            {
                GenerateSMSAlert(masking, destinationnum, msg, username, pass);
            }

            //end sending SMS on request
        }
        #region Generate SMS

        private static int GenerateSMSAlert(string Masking, string toNumber, string MessageText, string MyUsername, string MyPassword)
        {
            int count = 0;

            if (toNumber.Substring(0, 1).Equals("0"))
            {
                toNumber = toNumber.Remove(0, 1).Insert(0, "92");
            }

            if (toNumber.Length == 12 && toNumber.Substring(0, 2).Equals("92"))
            {
                count++;
            }
            SendSMS(Masking, toNumber, MessageText, MyUsername, MyPassword);
            return count;
        }
        #endregion

        #region Send SMS

        private static string SendSMS(string Masking, string toNumber, string MessageText, string MyUsername, string MyPassword)
        {
            // OLD IMPLEMENTATION
            //string URI = @"http://api.bizsms.pk/api-send-branded-sms.aspx?username=" + MyUsername + "&pass=" + MyPassword +
            //    "&text=" + MessageText + "&masking=" + Masking + "&destinationnum=" + toNumber + "&language=English";

            //WebRequest req = WebRequest.Create(URI);
            //WebResponse resp = req.GetResponse();
            //var sr = new System.IO.StreamReader(resp.GetResponseStream());
            //return sr.ReadToEnd().Trim();

            SmsApiService.QuickSMSResquest quickSMSResquest = new SmsApiService.QuickSMSResquest()
            {
                loginId = MyUsername,
                loginPassword = MyPassword,
                Destination = toNumber,
                Mask = Masking,
                Message = MessageText,
                ShortCodePrefered = "n",
                UniCode = "0"
            };

            SmsApiService.BasicHttpBinding_ICorporateCBS client = new SmsApiService.BasicHttpBinding_ICorporateCBS();
            string message = client.QuickSMS(quickSMSResquest);
            return message;
        }
        #endregion
    }
}
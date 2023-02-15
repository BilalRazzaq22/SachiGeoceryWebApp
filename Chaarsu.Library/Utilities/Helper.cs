using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EvoPdf;

namespace Chaarsu.Library.Utilities
{
    public static class Helper
    {
        public static DateTime? GetUTCDateTime(this DateTime? dt, string _TimeZone = "Eastern Standard Time")
        {
            try
            {
                DateTime specifiedTime = DateTime.SpecifyKind(dt.Value, DateTimeKind.Unspecified);
                TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById(_TimeZone);
                DateTime someDateTimeInUtc = TimeZoneInfo.ConvertTimeToUtc(specifiedTime, est);
                return someDateTimeInUtc;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public static void CreateDirectories(string _dir)
        {
            if (!Directory.Exists(_dir))
            {
                Directory.CreateDirectory(_dir);
            }
        }

        public static void DeleteDirectory(string _dir,bool Subdirectory)
        {
            if (Directory.Exists(_dir))
            {
                Directory.Delete(_dir, Subdirectory);
            }
        }

         public static void DeleteFiles(string _dir)
        {
            if (Directory.Exists(_dir))
            {
                Array.ForEach(Directory.GetFiles(_dir), File.Delete);
            }
        }

         public static List<string> GetFiles(string _dir)
         {
             List<string> _lstFiles = new List<string>();

             if (Directory.Exists(_dir))
             {
                 string[] filespath = Directory.GetFiles(_dir, "*.*",SearchOption.TopDirectoryOnly);
                 foreach (string _file in filespath)
                 {
                     _lstFiles.Add(_file.ToString());
                 }
             }

             return _lstFiles;
         }

       
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
       

        public static string GetTimeAgo(DateTime? dt)
        {
            try
            {
                if (dt.HasValue)
                {
                    TimeSpan span = DateTime.Now - Convert.ToDateTime(dt);
                    if (span.Days > 365)
                    {
                        int years = (span.Days / 365);
                        if (span.Days % 365 != 0)
                            years += 1;
                        return String.Format("{0} {1} ago",
                        years, years == 1 ? "year" : "years");
                    }
                    if (span.Days > 30)
                    {
                        int months = (span.Days / 30);
                        if (span.Days % 31 != 0)
                            months += 1;
                        return String.Format("{0} {1} ago",
                        months, months == 1 ? "month" : "months");
                    }
                    if (span.Days == 1)
                        return String.Format("{0}",
                                         "Yesterday");
                    else if (span.Days > 1)
                        return String.Format("{0} {1} ago",
                        span.Days, span.Days == 1 ? "day" : "days");
                    if (span.Hours > 0)
                        return String.Format("{0} {1} ago",
                        span.Hours, span.Hours == 1 ? "hour" : "hours");
                    if (span.Minutes > 0)
                        return String.Format("{0} {1} ago",
                        span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
                    if (span.Seconds > 5)
                        return String.Format("{0} seconds ago", span.Seconds);
                    if (span.Seconds <= 5)
                        return "just now";
                    return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        public static string GetTimeAgo2(DateTime? dt)
        {
            try
            {
                if (dt.HasValue)
                {
                    DateTime _time = Convert.ToDateTime("2016-10-07 09:43:31.797");

                    TimeSpan span = _time - Convert.ToDateTime(dt);
                    if (span.Days > 365)
                    {
                        return Convert.ToDateTime(dt).ToString("MM/dd/yyyy");
                    }
                    if (span.Days > 30)
                    {
                        return Convert.ToDateTime(dt).ToString("MM/dd/yyyy");
                    }
                    if (span.Days == 1)
                        return String.Format("{0}",
                                         "Yesterday");
                    else if (span.Days > 1)
                        return Convert.ToDateTime(dt).ToString("dddd");
                    if (span.Hours > 0)
                        return Convert.ToDateTime(dt).ToString("h:mm tt");
                    if (span.Minutes > 0)
                        return Convert.ToDateTime(dt).ToString("h:mm tt");
                    if (span.Seconds > 5)
                        return Convert.ToDateTime(dt).ToString("h:mm tt");
                    if (span.Seconds <= 5)
                        return Convert.ToDateTime(dt).ToString("h:mm tt");
                    return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetControlledCharacterString(string inputString, int requiredLength)
        {
            string str = "";
            if (inputString.Length <= requiredLength)
            {
                str = inputString;
            }
            else
            {
                str = inputString.Substring(0, requiredLength).Insert(requiredLength, "...");
            }
            return str;
        }
        public static DateTime? GetFromDateTime(string datetime)
        {
            DateTime _value;
            if (DateTime.TryParse(datetime, out _value))
            {
                DateTime _date = Convert.ToDateTime(datetime + " 00:00:00");
                return _date;
            }
            else
                return null;
        }
        public static DateTime? GetToDateTime(string datetime)
        {
            DateTime _value;
            if (DateTime.TryParse(datetime, out _value))
            {
                DateTime _date = Convert.ToDateTime(datetime + " 23:59:59");
                return _date;
            }
            else
                return null;
        }

        public static string CreateImageFromEncodedString(string encodedImage, string Path)
        {
            try
            {
                var base64String = Convert.ToString(encodedImage);
                // Convert Base64 String to byte[]
                var imageBytes = Convert.FromBase64String(base64String);
                var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                var image = new Bitmap(ms);
                var photoName = Guid.NewGuid().ToString() + ".png";
                Path = Path + photoName;
                image.Save(Path);
                return photoName;
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        public static DateTime EndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public static DateTime StartOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
        //public static string ExtractHtmlInnerText(string htmlText)
        //{
        //    //Match any Html tag (opening or closing tags) 
        //    // followed by any successive whitespaces
        //    //consider the Html text as a single line

        //    //Regex regex = new Regex("(<.*?>\\s*)+", RegexOptions.Singleline);
        //    Regex regex = new Regex(@"<[^>]+>|&nbsp;", RegexOptions.Singleline);

        //    // replace all html tags (and consequtive whitespaces) by spaces
        //    // trim the first and last space

        //    string resultText = regex.Replace(htmlText, "").Trim();

        //    // string str1 = System.Text.RegularExpressions.Regex.Replace(htmlText, "(<[a|A][^>]*>|)", "");
        //    return resultText;
        //}
        public static DateTime GetStartOfWeek(DateTime date)
        {
            int DaysToSubtract = (int)date.DayOfWeek;
            DateTime dt = date.Subtract(TimeSpan.FromDays(DaysToSubtract));
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }
        public static DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }
        public static DateTime LastDayOfMonth(DateTime dateTime)
        {
            DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// An better method to verify whether a value is 
        /// kosher for SQL Server datetime. This uses the native library
        /// for checking range values
        /// </summary>
        /// <param name="someval">A date string that may parse</param>
        /// <returns>true if the parameter is valid for SQL Sever datetime</returns>
        public static bool IsValidSqlDateTimeNative(string someval)
        {
            bool valid = false;
            DateTime testDate = DateTime.MinValue;
            System.Data.SqlTypes.SqlDateTime sdt;
            if (DateTime.TryParse(someval, out testDate))
            {
                try
                {
                    // take advantage of the native conversion
                    sdt = new System.Data.SqlTypes.SqlDateTime(testDate);
                    valid = true;
                }
                catch (System.Data.SqlTypes.SqlTypeException ex)
                {

                    // no need to do anything, this is the expected out of range error
                }
            }

            return valid;
        }

        public static bool IsValidEmail(string emailAddress)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                  + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                  + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                  + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                  + @"[a-zA-Z]{2,}))$";
            Regex reStrict = new Regex(patternStrict);
            bool isStrictMatch = reStrict.IsMatch(emailAddress);
            return isStrictMatch;
        }

        public static bool IsValidPattern(string vToValidate,string vRegex)
        {
            string patternStrict = vRegex;
            Regex reStrict = new Regex(patternStrict);
            bool isStrictMatch = reStrict.IsMatch(vToValidate);
            return isStrictMatch;
        }

        public static Dictionary<string, bool> UserSetting()
        {
            var list = new Dictionary<string, bool>();
            list.Add("Notification", true);
            list.Add("SendEmail", true);
            return list;
        }
      
        public static string GetUserSettingString(string settingKey)
        {
            var retString = "";
            switch (settingKey)
            {
                case "Notification":
                    retString = "Notification";
                    break;
                case "SendEmail":
                    retString = "Send Email";
                    break;
                default:
                    break;
            }
            return retString;
        }

        public static string TitleCase(string value)
        {
            string titleString = ""; // destination string, this will be returned by function
            if (!String.IsNullOrEmpty(value))
            {
                string[] lowerCases = new string[13] { "of", "the", "in", "a", "an", "to", "and", "at", "from", "by", "on", "or", "is" }; // list of lower case words that should only be capitalised at start and end of title
                string[] specialCases = new string[7] { "UK", "USA", "IRS", "UCLA", "PHd", "UB40a", "MSc" }; // list of words that need capitalisation preserved at any point in title
                string[] words = value.ToLower().Split(' ');
                bool wordAdded = false; // flag to confirm whether this word appears in special case list
                int counter = 1;
                foreach (string s in words)
                {

                    // check if word appears in lower case list
                    foreach (string lcWord in lowerCases)
                    {
                        if (s.ToLower() == lcWord)
                        {
                            // if lower case word is the first or last word of the title then it still needs capital so skip this bit.
                            if (counter == 0 || counter == words.Length) { break; };
                            titleString += lcWord;
                            wordAdded = true;
                            break;
                        }
                    }

                    // check if word appears in special case list
                    foreach (string scWord in specialCases)
                    {
                        if (s.ToUpper() == scWord.ToUpper())
                        {
                            titleString += scWord;
                            wordAdded = true;
                            break;
                        }
                    }

                    if (!wordAdded)
                    { // word does not appear in special cases or lower cases, so capitalise first letter and add to destination string
                        titleString += char.ToUpper(s[0]) + s.Substring(1).ToLower();
                    }
                    wordAdded = false;

                    if (counter < words.Length)
                    {
                        titleString += " "; //dont forget to add spaces back in again!
                    }
                    counter++;
                }
            }
            return titleString;
        }

        public static string RelativeTime(DateTime time)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - time.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 0)
            {
                return "not yet";
            }
            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 2 * MINUTE)
            {
                return "a minute ago";
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 90 * MINUTE)
            {
                return "an hour ago";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 48 * HOUR)
            {
                return "yesterday";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + " days ago";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
        public static int GetDayNumber(string value)
        {
            var retString = 0;
            switch (value)
            {
                case "Monday":
                    retString = 1;
                    break;
                case "Tuesday":
                    retString = 2;
                    break;
                case "Wednesday":
                    retString = 3;
                    break;
                case "Thursday":
                    retString = 4;
                    break;
                case "Friday":
                    retString = 5;
                    break;
                case "Saturday":
                    retString = 6;
                    break;
                case "Sunday":
                    retString = 7;
                    break;
            }
            return retString;
        }
        public static PdfConverter GetPdfConverter()
        {

            // get the HTML document width and height in pixels
            // default HTML document width is 1024 pixels
            int pageWidthPx = 0;
            int pageHeightPx = 0;

            // set common properties
            pageWidthPx = 800;
            pageHeightPx = 0;

            // create the PDF converter
            var pdfConverter = new PdfConverter(pageWidthPx, pageHeightPx);
            pdfConverter.PdfDocumentOptions.AutoSizePdfPage = true;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;
            pdfConverter.PdfDocumentOptions.FitWidth = true;
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            pdfConverter.PdfDocumentOptions.EmbedFonts = true;
            pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = false;
            pdfConverter.JavaScriptEnabled = true;
            pdfConverter.LicenseKey = "2fLr+erq+ejr7+r56vfp+ero9+jr9+Dg4OA=";

            //pdfConverter.PdfDocumentOptions.CustomPdfPageSize = new SizeF(595, 842);
            pdfConverter.PdfDocumentOptions.Width = 500;
            pdfConverter.PdfDocumentOptions.Height = 842;

            // set the PDF page orientation (portrait or landscape)
            // the the PDF standard used to generate the PDF document (PDF, PDF/A, PDF/X or PDF/SigQ)
            // show or hide header and footer
            //set PDF document margins
            pdfConverter.PdfDocumentOptions.LeftMargin = 0;
            pdfConverter.PdfDocumentOptions.RightMargin = 0;
            pdfConverter.PdfDocumentOptions.TopMargin = 0;
            pdfConverter.PdfDocumentOptions.BottomMargin = 0;
            pdfConverter.PdfDocumentOptions.ShowFooter = false;
            pdfConverter.PdfDocumentOptions.ShowHeader = false;


            return pdfConverter;
        }

        public static string GetTutorDetailHtml()
        {
            string rootPath = ConfigurationManager.AppSettings["RootPath"] + "";
            string html = File.ReadAllText(rootPath + "/Areas/Admin/PdfTemplates/TutorDetailsPdf.html");
            return html;
        }

       public static int CalculateYourAge(DateTime Dob, DateTime Now)
        {
           
                //DateTime Now = DateTime.Now;
                int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
                DateTime PastYearDate = Dob.AddYears(Years);
                int Months = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (PastYearDate.AddMonths(i) == Now)
                    {
                        Months = i;
                        break;
                    }
                    else if (PastYearDate.AddMonths(i) >= Now)
                    {
                        Months = i - 1;
                        break;
                    }
                }
                int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
                int Hours = Now.Subtract(PastYearDate).Hours;
                int Minutes = Now.Subtract(PastYearDate).Minutes;
                int Seconds = Now.Subtract(PastYearDate).Seconds;
                return Years;
        }  
    }
}

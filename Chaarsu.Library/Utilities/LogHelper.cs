using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Chaarsu.Library.Utilities
{
    public enum ErrorType
    {
        StoreProcedure,
        Notification,
        Schedular,
        System,
        User,
        PerUser,
        PaymentGateway,
        Exception,
        APILog
    }

    public static class LogHelper
    {

        /// <summary>
        /// LogHelper.CreateLogWithRequest(ex, Request);
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="Request"></param>
        /// <param name="_ErrorType"></param>
        /// <returns></returns>
        public static Boolean CreateLogWithRequest(Exception ex, HttpRequestMessage Request, ErrorType _ErrorType = ErrorType.Exception)
        {

            //if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Logger"].ToString()) == false && ErrorType.Exception != _ErrorType)
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Logger"].ToString()) == false)
            {
                return false;
            }


            try
            {

                string filePath = "";

                if (ErrorType.Exception == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\ExceptionLog.txt";
                }
                else if (ErrorType.User == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\UserLog.txt";
                }
                else if (ErrorType.StoreProcedure == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\StoreProcedure.txt";
                }
                else if (ErrorType.Notification == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\Notification.txt";
                }
                else if (ErrorType.PaymentGateway == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\PaymentGateway.txt";
                }
                else if (ErrorType.Schedular == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\Schedular.txt";
                }


                Helper.CreateDirectories(ConfigurationManager.AppSettings["LogFilePath"]);

                string _otherstuff = "";

                try
                {
                    HttpContent requestContent = Request.Content;
                    string jsonContent = requestContent.ReadAsStringAsync().Result;
                    Request.RequestUri.ToString();


                    _otherstuff = "REQUST : " + Request.RequestUri.ToString() + " BODY :" + jsonContent.ToString() + " ";
                }
                catch
                {
                }

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine("" + _otherstuff + "Message :" + ex.Message + " TargetSite: " + ex.TargetSite.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        string _innerexception = (ex.InnerException == null) ? "" : ex.InnerException.ToString();

                        writer.WriteLine("" + _otherstuff + "Inner :" + _innerexception + " Message :" + ex.Message + " TargetSite: " + ex.TargetSite.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }

                }

                return true;
            }
            catch
            {

                return false;
            }





        }



        public static async Task<Boolean> CreateLogAsync(Exception ex, ErrorType _ErrorType = ErrorType.Exception)
        {

            //if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Logger"].ToString()) == false && ErrorType.Exception != _ErrorType)
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Logger"].ToString()) == false)
            {
                return false;
            }

            return await Task.Factory.StartNew(() =>
            {

                try
                {

                    string filePath = "";

                    if (ErrorType.Exception == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\ExceptionLog.txt";
                    }
                    else if (ErrorType.User == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\UserLog.txt";
                    }
                    else if (ErrorType.StoreProcedure == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\StoreProcedure.txt";
                    }
                    else if (ErrorType.Notification == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\Notification.txt";
                    }
                    else if (ErrorType.PaymentGateway == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\PaymentGateway.txt";
                    }
                    else if (ErrorType.Schedular == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\Schedular.txt";
                    }

                    Helper.CreateDirectories(ConfigurationManager.AppSettings["LogFilePath"]);

                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Dispose();
                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("Message :" + ex.Message + " TargetSite: " + ex.TargetSite.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                            writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                        }
                    }
                    else
                    {
                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("Message :" + ex.Message + " TargetSite: " + ex.TargetSite.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                            writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                        }

                    }

                    return true;
                }
                catch
                {

                    return false;
                }


            });


        }

        public static Boolean CreateLog(Exception ex, ErrorType _ErrorType = ErrorType.Exception)
        {

            //if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Logger"].ToString()) == false && ErrorType.Exception != _ErrorType)
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Logger"].ToString()) == false)
            {
                return false;
            }


            try
            {

                string filePath = "";

                if (ErrorType.Exception == _ErrorType)
                {

                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\ExceptionLog.txt";
                }
                else if (ErrorType.User == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\UserLog.txt";
                }
                else if (ErrorType.StoreProcedure == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\StoreProcedure.txt";
                }
                else if (ErrorType.Notification == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\Notification.txt";
                }
                else if (ErrorType.PaymentGateway == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\PaymentGateway.txt";
                }
                else if (ErrorType.Schedular == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\Schedular.txt";
                }

                Helper.CreateDirectories(ConfigurationManager.AppSettings["LogFilePath"]);

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        //var s = new StackTrace(ex);
                        //var thisasm = Assembly.GetExecutingAssembly();
                        //var methodname = s.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == thisasm).Name;

                        writer.WriteLine("Message :" + ex.Message + " Method: " + ex.ToString() + " TargetSite: " + ex.TargetSite.ToString() + ex.TargetSite + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        //var s = new StackTrace(ex);
                        //var thisasm = Assembly.GetExecutingAssembly();
                        //var methodname = s.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == thisasm).Name;

                        writer.WriteLine("Message :" + ex.Message + " Method: " + ex.ToString() + " TargetSite: " + ex.TargetSite.ToString() + ex.TargetSite + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }

                }

                return true;
            }
            catch
            {

                return false;
            }





        }

        public static async Task<Boolean> CreateLogAsync(string logmessage, ErrorType _ErrorType = ErrorType.User)
        {
            //if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Logger"].ToString()) == false && ErrorType.Exception != _ErrorType)
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Logger"].ToString()) == false)
            {
                return false;
            }

            return await Task.Factory.StartNew(() =>
            {

                try
                {
                    string filePath = "";

                    if (ErrorType.Exception == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\ExceptionLog.txt";
                    }
                    else if (ErrorType.User == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\UserLog.txt";
                    }
                    else if (ErrorType.StoreProcedure == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\StoreProcedure.txt";
                    }
                    else if (ErrorType.Notification == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\Notification.txt";
                    }
                    else if (ErrorType.PaymentGateway == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\PaymentGateway.txt";
                    }
                    else if (ErrorType.Schedular == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\Schedular.txt";
                    }

                    Helper.CreateDirectories(ConfigurationManager.AppSettings["LogFilePath"]);

                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Dispose();

                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("Message :" + logmessage.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                            writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                        }
                    }
                    else
                    {

                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("Message :" + logmessage.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                            writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                        }
                    }

                    return true;

                }
                catch
                {
                    return false;

                }


            });

        }

        public static Boolean CreateLog(string logmessage, ErrorType _ErrorType = ErrorType.User,long EvaluateId=0)
        {
            //if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Logger"].ToString()) == false && ErrorType.Exception != _ErrorType)
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["Logger"].ToString()) == false)
            {
                return false;
            }



            try
            {
                string filePath = "";

                if (ErrorType.Exception == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\ExceptionLog.txt";
                }
                else if (ErrorType.User == _ErrorType)
                {
                    if (EvaluateId != 0)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + @"\Logs\" + EvaluateId.ToString() + ".txt";
                    }
                    else
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\UserLog.txt";
                    }
                   
                }
                //else if (ErrorType.PerUser == _ErrorType)
                //{

                //    if (EvaluateId != 0)
                //    {
                //        filePath = ConfigurationManager.AppSettings["LogFilePath"] +@"\Logs\"+ EvaluateId.ToString() + ".txt";
                //    }
                //}
                else if (ErrorType.StoreProcedure == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\StoreProcedure.txt";
                }
                else if (ErrorType.Notification == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\Notification.txt";
                }
                else if (ErrorType.PaymentGateway == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\PaymentGateway.txt";
                }
                else if (ErrorType.System == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\ResetPoints.txt";
                }
                else if (ErrorType.Schedular == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\Schedular.txt";
                }

                Helper.CreateDirectories(ConfigurationManager.AppSettings["LogFilePath"]);

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();

                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine("" + logmessage.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "----------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                    }
                }
                else
                {

                    //using (StreamWriter writer = new StreamWriter(filePath, true))
                    //{
                    //    writer.WriteLine("Message :" + logmessage.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    //    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    //}

                    string str;
                    string str2;
                    using (StreamReader sreader = new StreamReader(filePath, true))
                    {
                        str = sreader.ReadToEnd();
                    }

                    File.Delete(filePath);

                    using (StreamWriter swriter = new StreamWriter(filePath, true))
                    {
                        str2 = logmessage.ToString() + Environment.NewLine + "Date: " + DateTime.Now.ToString();
                        str2 = str2 + Environment.NewLine + "----------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine;
                        str2 = str2 + str;
                        swriter.Write(str2);
                    }
                }

                return true;

            }
            catch(Exception ex)
            {
                return false;

            }



        }


        /// <summary>
        /// LogHelper.CreateAPILog("AddSearch", JsonConvert.SerializeObject(input).ToString(), ErrorType.APILog);
        /// 
        /// </summary>
        /// <param name="Api"></param>
        /// <param name="logmessage"></param>
        /// <param name="_ErrorType"></param>
        /// <returns></returns>
        public static async Task<Boolean> CreateAPILogAsync(string Info, string Api, string logmessage, ErrorType _ErrorType = ErrorType.User)
        {


            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["APILogger"].ToString()) == false)
            {
                return false;
            }

            return await Task.Factory.StartNew(() =>
            {

                try
                {
                    string filePath = "";
                    string _Readvalue = "";

                    if (ErrorType.APILog == _ErrorType)
                    {
                        filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\APILog.txt";
                    }

                    Helper.CreateDirectories(ConfigurationManager.AppSettings["LogFilePath"]);

                    if (File.Exists(filePath))
                    {
                        _Readvalue = File.ReadAllText(filePath);
                    }

                    if (!_Readvalue.Contains(Api))
                    {
                        if (!File.Exists(filePath))
                        {
                            File.Create(filePath).Dispose();

                            using (StreamWriter writer = new StreamWriter(filePath, true))
                            {
                                writer.WriteLine(Info + " : " + logmessage.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                            }
                        }
                        else
                        {

                            using (StreamWriter writer = new StreamWriter(filePath, true))
                            {
                                writer.WriteLine(Info + " : " + logmessage.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                            }
                        }
                    }

                    return true;

                }
                catch
                {
                    return false;

                }


            });

        }

        /// <summary>
        /// LogHelper.CreateAPILog("AddSearch", JsonConvert.SerializeObject(input).ToString(), ErrorType.APILog);
        /// 
        /// </summary>
        /// <param name="Api"></param>
        /// <param name="logmessage"></param>
        /// <param name="_ErrorType"></param>
        /// <returns></returns>
        public static Boolean CreateAPILog(string Info, string Api, string logmessage, ErrorType _ErrorType = ErrorType.User)
        {

            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["APILogger"].ToString()) == false)
            {
                return false;
            }           
            try
            {
                string filePath = "";
                string _Readvalue = "";

                if (ErrorType.APILog == _ErrorType)
                {
                    filePath = ConfigurationManager.AppSettings["LogFilePath"] + "\\APILog.txt";
                }

                Helper.CreateDirectories(ConfigurationManager.AppSettings["LogFilePath"]);

                if (File.Exists(filePath))
                {
                    _Readvalue = File.ReadAllText(filePath);
                }

                //if (!_Readvalue.Contains(Api))
                //{
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();

                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine(Info + " : " + logmessage.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }
                }
                else
                {

                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine(Info + " : " + logmessage.ToString() + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                        writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    }
                }
                //}

                return true;

            }
            catch
            {
                return false;

            }

        }

    }
}

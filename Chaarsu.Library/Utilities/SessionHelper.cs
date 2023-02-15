

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chaarsu.Library.Utilities
{
    public sealed class SessionHelper
    {
        //private AdmUser _UserProfile = null;
        //private LanguageSwitch _languageSwitch = null;

        //public AdmUser UserProfile
        //{
        //    get { return _UserProfile; }
        //    set { _UserProfile = value; }
        //}
        //public LanguageSwitch languageSwitch
        //{
        //    get { return _languageSwitch; }
        //    set { _languageSwitch = value; }
        //}


        //private static SessionHelper _Instance;
        //private static SessionHelper _InstanceLanguage;

        //public static SessionHelper Instance
        //{
        //    get
        //    {
        //        _Instance = new SessionHelper();
        //        if (HttpContext.Current.Session["__MESSession__"] == null)
        //        {
        //            HttpContext.Current.Session["__MESSession__"] = _Instance;
        //        }
        //        else
        //        {
        //            _Instance.UserProfile = HttpContext.Current.Session["__MESSession__"] as AdmUser;
        //        }
        //        return _Instance;
        //    }
        //}
        //public static SessionHelper InstanceLanguage
        //{
        //    get
        //    {
        //        _InstanceLanguage = new SessionHelper();
        //        if (HttpContext.Current.Session["__LangSession__"] == null)
        //        {
        //            HttpContext.Current.Session["__LangSession__"] = _InstanceLanguage;
        //        }
        //        else
        //        {
        //            _InstanceLanguage.languageSwitch = HttpContext.Current.Session["__LangSession__"] as LanguageSwitch;
        //        }
        //        return _InstanceLanguage;
        //    }
        //}

    }
}

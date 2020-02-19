using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AksesuarSat_V1.Models;
using AksesuarSat_V1.Areas.AdminPaneli.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Controllers
{
    public class DefaultController : Controller
    {
        // GET: AdminPaneli/Default
        public ActionResult DefaultIndex()
        {
            return View();
        }

        public ActionResult TestIndex()
        {
            return View();
        }

        UrunManage urunMng = new UrunManage();
        
        public ActionResult EkleFakeData()
        {
            //urunMng.SiparisFakeData();
            //urunMng.SiparisDetayFakeData();
            return View();
        }
 
      
    }
}
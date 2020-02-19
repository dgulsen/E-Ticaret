using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AksesuarSat_V1.Areas.AdminPaneli.Models;
using PagedList;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Controllers
{

    public class UyeAdresleriController : Controller
    {
        UyeAdresleriManage uyeAdres_mng = new UyeAdresleriManage();
        // GET: AdminPaneli/UyeAdresleri
        public ActionResult UyeAdresleriIndex()
        {
            return View();
        }
        #region LİSTELEME
        public ActionResult UyeAdresListesi(int? sayfaNo)
        {
            int numara = sayfaNo ?? 1;
            var liste = uyeAdres_mng.UyeAdresListesi().ToPagedList(numara, 5);
            return View(liste);
        }
        #endregion

        public ActionResult AdresDetay(int detayId)
        {
            var detay = uyeAdres_mng.UyeAdres(detayId);
            return View(detay);
        }


    }

}
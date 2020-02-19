
using AksesuarSat_V1.Areas.AdminPaneli.Models;
using AksesuarSat_V1.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AksesuarSat_V1.Areas.AdminPaneli.Controllers
{
    public class UyelerController : Controller
    {
        UyelerManage uyeler_mng = new UyelerManage();

        // GET: AdminPaneli/Uyeler
        public ActionResult UyelerIndex()
        {
            return View();
        }


        public ActionResult UyeListesi(UyelerPartial nesneUye)
        {
            int numara = nesneUye.SayfaNumarasi ?? 1;
            //var liste = uyeler_mng.UyeListesi().ToPagedList(numara, 5);
            nesneUye.Uye_Listesi= uyeler_mng.UyeListesi(nesneUye).ToPagedList(numara, 5);
            if (Request.IsAjaxRequest())//her Ajax isteği geldiğinde PartialView'e yönlendiriyoruz
            {
                return PartialView("~/Areas/AdminPaneli/Views/Uyeler/_UyeListPartial.cshtml", nesneUye);
            }
            return View(nesneUye);
        }

        public ActionResult UyeDetay(int detayId)
        {
            var detay = uyeler_mng.UyeListesi().FirstOrDefault(k => k.UyelerID == detayId);
            return View(detay);
        }

        public ActionResult UyeSil(int silId)
        {
            var sil = uyeler_mng.UyeListesi().FirstOrDefault(k => k.UyelerID == silId);
            return View(sil);
        }

        [HttpPost, ActionName("UyeSil")]
        public ActionResult DeleteUye(int silId)
        {
            string deletemsg = uyeler_mng.UyeSil(silId);
            if (deletemsg.Contains("başarılı"))
            {
                return RedirectToAction("UyeListesi");
            }
            return View();
        }

        public ActionResult UyeGuncelle(int guncelleId)
        {
            var tiklanan = uyeler_mng.UyeListesi().FirstOrDefault(k => k.UyelerID == guncelleId);
            return View(tiklanan);
        }
        [HttpPost]
        public ActionResult UyeGuncelle(Uyeler ekle)
        {
            string updateResult = uyeler_mng.UyeGuncelle(ekle);
            //return View();
            return RedirectToAction("UyeListesi");
        }
    }
}


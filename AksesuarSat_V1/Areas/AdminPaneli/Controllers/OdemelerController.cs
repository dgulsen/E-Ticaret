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
    public class OdemelerController : Controller
    {
        // GET: AdminPaneli/Odemeler


        OdemelerManage odm_manage = new OdemelerManage();
        public ActionResult OdemeListe(int? sayfaNo)
        {
            int numara = sayfaNo ?? 1;
            //?? => null oldugunda sayfa numarasini 1 olarak ata anlamina geliyor.
            var liste = odm_manage.OdemeListesi().ToPagedList(numara, 5); // Listeyi 5 er 5 er Gonder
            //Jquery sayfalandirma yapilarina gore avantajlari ve dezavantajlari mevcut
            return View(liste);
        }

        public ActionResult OdemeDetay(int id)
        {
            var detay = odm_manage.OdemeListesi().FirstOrDefault(k => k.OdemelerID == id);


            return View(detay);
        }




        public ActionResult OdemeSil(int id)
        {
            var detay = odm_manage.OdemeListesi().FirstOrDefault(k => k.OdemelerID == id);


            return View(detay);
        }
        [HttpPost, ActionName("OdemeSil")]
        // Urunsil metodunun Post İşlemini uzerındeki delete metodun üzerinde gerceklesicek
        public ActionResult DeleteOdeme(int id)
        {
            string deleteResult = odm_manage.OdemeSil(id);
            if (deleteResult.Contains("başarılı"))
            {
                return RedirectToAction("OdemeListe");
            }
            else
            {
                return View();
            }

        }

        public ActionResult OdemeGuncelle(int id)
        {
            List<SelectListItem> Adi = new List<SelectListItem>();
            foreach (var item in odm_manage.SiparisListesi())
            {
                // Sanal List icine de kategori verileri ile doldurduk
                Adi.Add(new SelectListItem { Text = item.SiparisTarihi.ToString(), Value = item.SiparislerID.ToString() }
                );                           // adi                 ,  ıd alır              
            }
            ViewBag.SiparisID = Adi;

            var detay = odm_manage.OdemeListesi().FirstOrDefault(k => k.OdemelerID == id);
            return View(detay);

        }
        [HttpPost]
        public ActionResult OdemeGuncelle(Odemeler nesne)
        {

            string detay = odm_manage.Guncelle(nesne);
            if (detay.Contains("güncellendi"))
            {
                return RedirectToAction("OdemeGuncelle", nesne.OdemelerID);
            }
            return View();

        }


    }
}
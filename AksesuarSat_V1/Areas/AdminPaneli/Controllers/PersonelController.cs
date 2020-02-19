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
    public class PersonelController : Controller
    {
        PersonelManage pers_manage = new PersonelManage();
        // GET: AdminPaneli/Personel
        public ActionResult PersonelIndex()
        {
            return View();
        }

        public ActionResult Deneme()
        {
            return View();
        }

        public ActionResult PersonelListesi(int? sayfaNo)
        {
            int numara = sayfaNo ?? 1;
            var liste = pers_manage.PersonelListesi().ToPagedList(numara, 5);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/AdminPaneli/Views/Personel/_PersListPartial.cshtml", liste);
            }
            return View(liste);
        }

        public ActionResult Guncelle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Guncelle(string adi)
        {
            return View();
        }
        public ActionResult PersonelEkle()
        {
            return View();
        }

      [HttpPost]
        public ActionResult PersonelEkle(string adi, string soyadi, string telefon, string adres, string email, string Cinsiyet, string medeniHal, string dogumYeri, DateTime dogumtarih, DateTime isegirisTarihi)
        {
            string insertResult = pers_manage.PersonelEkle(adi, soyadi, telefon, adres, email, Cinsiyet, medeniHal, dogumYeri, dogumtarih, isegirisTarihi);
            if (insertResult.Contains("Eklendi"))
            {
                return RedirectToAction("PersonelListesi");
            }
            ViewBag.sonuc = insertResult;
            return View();
        }

        public ActionResult PersonelSil(int silId)
        {
            var GelenPers = pers_manage.PersonelListesi().FirstOrDefault(k => k.PersonellerID == silId);
            return View(GelenPers);
        }
        [HttpPost,ActionName("PersonelSil")]
        public ActionResult Delete(int silId)
        {
            return View();
        }

        [HttpGet]//bir şey yazılmadığında HttpGet olarak kabul eder
        public ActionResult PersonelGuncelle(int guncelleId)
        {
            var tiklanan = pers_manage.PersonelListesi().FirstOrDefault(k => k.PersonellerID == guncelleId);

            return View(tiklanan);
        }

        [HttpPost]
        public ActionResult PersonelGuncelle(Personeller ekle)
        {
            string updResult = pers_manage.PersonelGuncelle(ekle);

            //return RedirectToAction("PersonelListesi");
            var liste = pers_manage.PersonelListesi().FirstOrDefault(k => k.PersonellerID == ekle.PersonellerID);
            return View(liste);
        }
    }
}
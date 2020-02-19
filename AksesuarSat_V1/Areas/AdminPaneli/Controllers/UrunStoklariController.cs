using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AksesuarSat_V1.Models;
using AksesuarSat_V1.Areas.AdminPaneli.Models;
using PagedList;

namespace AksesuarSat_V1.Areas.AdminPaneli.Controllers
{
    public class UrunStoklariController : Controller
    {
        // GET: AdminPaneli/UrunStoklari
        UrunStoklariManage urunstok_mng = new UrunStoklariManage();
        UrunManage urun_mng = new UrunManage();
        public ActionResult UrunStoklariListesi(int? sayfaNo)
        {
            int numara = sayfaNo ?? 1;
            var liste = urunstok_mng.UrunStoklariListesi().ToPagedList(numara, 10);
            return View(liste);
        }
        public ActionResult UrunStokEkle()
        {
            List<SelectListItem> sanalList = new List<SelectListItem>();
            foreach (var item in urun_mng.UrunListesi())
            {
                sanalList.Add(new SelectListItem
                {
                    //Sanal list in içini de Kategori verileri ile dolduruyorum
                    Text = item.UrunAdi,
                    Value = item.UrunlerID.ToString()//Id değerini alır
                });
            }
            ViewBag.UrunID = sanalList;
            return View();
        }
        [HttpPost]
        public ActionResult UrunStokEkle(int UrunID, DateTime basTarihi, int stokmiktari, string aciklama)
        {
            string insertResult = urunstok_mng.UrunStokEkle(UrunID, basTarihi, stokmiktari, aciklama);
            if (insertResult.Contains("Eklendi") || insertResult.Contains("Güncelendi"))
            {
                return View("UrunStoklariListesi", urunstok_mng.UrunStoklariListesi());
            }
            ViewBag.sonuc = insertResult;
            return View();
        }
        public ActionResult UrunStokDetay(int detayId)
        {
            var detay = urunstok_mng.UrunStoklariListesi().FirstOrDefault(k => k.UrunStoklariID == detayId);
            return View(detay);
        }
        public ActionResult UrunStokSil(int silId)
        {
            var sil = urunstok_mng.UrunStoklariListesi().FirstOrDefault(k => k.UrunStoklariID == silId);
            return View(sil);
        }
        [HttpPost, ActionName("UrunStokSil")]
        public ActionResult DeleteUrunStok(int silId)
        {
            string deleteResult = urunstok_mng.UrunStokSil(silId);
            if (deleteResult.Contains("başarılı"))
            {
                return RedirectToAction("UrunStoklariListesi");
            }
            return View();
        }
        public ActionResult UrunStokGuncelle(int guncelleId)
        {
            var guncelle = urunstok_mng.UrunStoklariListesi().FirstOrDefault(k => k.UrunStoklariID == guncelleId);
            return View(guncelle);
        }
        [HttpPost]
        public ActionResult UrunStokGuncelle(UrunStoklari gnc)
        {
            var guncelle = urunstok_mng.UrunStokGuncelle(gnc);
            return RedirectToAction("UrunStoklariListesi");
        }
    }

}
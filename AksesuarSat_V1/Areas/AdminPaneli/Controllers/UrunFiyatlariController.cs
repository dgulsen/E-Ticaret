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
    public class UrunFiyatlariController : Controller
    {
        UrunFiyatlariManage urunFiyat_mng = new UrunFiyatlariManage();
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();
        UrunManage urun_mng = new UrunManage();
        KategoriManage kategori_mng = new KategoriManage();
        // GET: AdminPaneli/UrunFiyatlari
        public ActionResult UrunFiyatlariIndex()
        {
            return View();
        }

        #region LİSTE
        public ActionResult UrunFiyatlariListesi(int? sayfaNo)
        {
            int numara = sayfaNo ?? 1;
            return View(urunFiyat_mng.UrunFiyatlariListesi().ToPagedList(numara, 10));
        }
        #endregion

        #region DETAY
        public ActionResult UrunFiyatlariDetay(int urunfiyatlariId)
        {
            return View(db.UrunFiyatlari.FirstOrDefault(i => i.UrunFiyatlariID == urunfiyatlariId));
        }
        #endregion

        #region SİL
        [HttpGet]
        public ActionResult UrunFiyatlariSil(int urunfiyatlariId)
        {
            return View(db.UrunFiyatlari.FirstOrDefault(i => i.UrunFiyatlariID == urunfiyatlariId));
        }


        [HttpPost, ActionName("UrunFiyatlariSil")]
        public ActionResult delete(int urunfiyatlariId)
        {
            var deleteResult = urunFiyat_mng.UrunFiyatiSil(urunfiyatlariId);
            if (deleteResult.Contains("silinme"))
            {
                return RedirectToAction("UrunFiyatlariListesi");
            }
            return View();
        }

        #endregion

        #region EKLE
        [HttpGet]
        public ActionResult UrunFiyatEkleIndex()
        {
            List<SelectListItem> UrunList = new List<SelectListItem>();
            foreach (var item in urun_mng.UrunListesi())
            {
                UrunList.Add(new SelectListItem
                {
                    Text = item.UrunAdi,//Adi
                    Value = item.UrunlerID.ToString()//ID değerini alır
                });
            }
            ViewBag.UrunID = UrunList;
            return View();
        }

        [HttpPost]
        public ActionResult UrunFiyatEkleIndex(decimal urunAlisFiyati, decimal urunSatisFiyati, string fiyatBaslangicTarihi, string fiyatBitisTarihi, string aciklama, int urunId)
        {
            var insertResult = urunFiyat_mng.UrunFiyatiEkle(urunAlisFiyati, urunSatisFiyati, fiyatBaslangicTarihi, fiyatBitisTarihi, aciklama, urunId);
            if (insertResult.Contains("eklenmiştir"))
            {
                return RedirectToAction("UrunFiyatlariListesi");
            }
            return View();

        }

        #endregion

        #region GÜNCELLE
        [HttpPost]
        public ActionResult UrunFiyatGuncelle(UrunFiyatlari ekle)
        {
            var updateResult = urunFiyat_mng.UrunFiyatiGuncelle(ekle);
            var urunFiyatListe = db.UrunFiyatlari.FirstOrDefault(i => i.UrunFiyatlariID == ekle.UrunFiyatlariID);
            //return View(urunFiyatListe);
            if (updateResult.Contains("şekilde"))
            {
                return RedirectToAction("UrunFiyatlariListesi");
            }
            return View();
        }

        [HttpGet]
        public ActionResult UrunFiyatGuncelle(int guncelleid)
        {
            List<SelectListItem> UrunList = new List<SelectListItem>();
            int urunid = urunFiyat_mng.UrunFiyatlariListesi().FirstOrDefault(i => i.UrunFiyatlariID == guncelleid).Urunler.UrunlerID;
            ViewBag.ad = urunFiyat_mng.UrunFiyatlariListesi().FirstOrDefault(i => i.UrunFiyatlariID == guncelleid).Urunler.UrunAdi;
            foreach (var item in urun_mng.UrunListesi())
            {
                UrunList.Add(new SelectListItem
                {
                    Text = item.UrunAdi,
                    Value = item.UrunlerID.ToString(),
                    Selected = (item.UrunlerID == urunid ? true : false),
                });
            }
            ViewBag.UrunID = UrunList;
            return View(urunFiyat_mng.UrunFiyatlariListesi().FirstOrDefault(i => i.UrunFiyatlariID == guncelleid));
        }

        #endregion


    }
}
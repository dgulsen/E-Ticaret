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

    public class ReklamController : Controller
    {
        ReklamManage reklam_mng = new ReklamManage();
        // GET: AdminPaneli/Reklam
        [HttpGet]
        public ActionResult ReklamEkleIndex()
        {
            return View();
        }
        public ActionResult ReklamListesi(int? sayfaNo)
        {
            int numara = sayfaNo ?? 1;
            var liste = reklam_mng.ReklamListesi().ToPagedList(numara, 5);
            return View(liste);
        }
        [HttpPost]
        public ActionResult ReklamEkleIndex(string reklamaciklama, DateTime reklambaslangictarihi, DateTime reklambitistarihi, string resim1, string resim2, string resim3)
        {
            string insertReklam = reklam_mng.ReklamEkle(reklamaciklama, reklambaslangictarihi, reklambitistarihi, resim1, resim2, resim3);
            if (insertReklam.Contains("Eklendi"))
            {
                return RedirectToAction("ReklamListesi");
            }
            ViewBag.sonuc = insertReklam;
            return View();
        }

        public ActionResult ReklamDetay(int detayId)
        {
            var detay = reklam_mng.ReklamListesi().FirstOrDefault(k => k.ReklamID == detayId);
            return View(detay);
        }

        public ActionResult ReklamSil(int silId)
        {
            var sil = reklam_mng.ReklamListesi().FirstOrDefault(k => k.ReklamID == silId);
            return View(sil);
        }
        [HttpPost, ActionName("ReklamSil")]//UrunSil metodunun Post işlemi DeleteUrun metodu üzerinde gerçekleşeceği anlamına gelir.
        public ActionResult DeletePersonel(int silId)
        {
            string deletemsg = reklam_mng.ReklamSil(silId);
            if (deletemsg.Contains("Başarıyla"))
            {
                return RedirectToAction("ReklamListesi");//RedirectToAction metodu çağırır, View() view sayfasını çağırır.
            }
            return View();
        }

        [HttpGet]
        public ActionResult ReklamGuncelle(int guncelleId)
        {
            var veri = reklam_mng.ReklamListesi().FirstOrDefault(k => k.ReklamID == guncelleId);
            return View(veri);
        }

        [HttpPost]
        public ActionResult PersonelGuncelle(Reklamlar ekle)
        {
            string updResult = reklam_mng.ReklamGuncelle(ekle);
            //return View();
            return RedirectToAction("ReklamListesi");

            //var liste = personel_mng.PersonelListesi().FirstOrDefault(k => k.PersonellerID == ekle.PersonellerID);
            //return View(liste);
            //Yukarıdaki 2 satır da alternatif bir yol olarak kullanılabilir.
        }
    }


}
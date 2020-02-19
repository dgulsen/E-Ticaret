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

    public class indirimlerController : Controller
    {
        // GET: AdminPaneli/indirimler
        public ActionResult Index()
        {
            return View();
        }
        indirimlerManage indir_mng = new indirimlerManage();

        public ActionResult indirimListesi(int? sayfaNo)
        {
            int numara = sayfaNo ?? 1;
            var liste = indir_mng.indirimListesi().ToPagedList(numara, 10);
            return View(liste);
        }

        public ActionResult indirimDropDownIndex()
        {
            List<SelectListItem> SanalList = new List<SelectListItem>();

            foreach (var item in indir_mng.indirimTurListesi())
            {
                SanalList.Add(new SelectListItem
                {
                    Text = item.indirimTurAdi,
                    Value = item.indirimTurleriID.ToString()
                });

            }
            ViewBag.indirimTurAdi = SanalList;
            return View();
        }

        public ActionResult indirimDetay(int detayId)
        {
            var detay = indir_mng.indirimListesi().FirstOrDefault(k => k.indirimlerID == detayId);
            return View(detay);
        }

        public ActionResult indirimSil(int silId)
        {
            var sil = indir_mng.indirimListesi().FirstOrDefault(k => k.indirimlerID == silId);
            return View(sil);
        }

        [HttpPost, ActionName("indirimSil")]
        public ActionResult Deleteindirim(int silId)
        {
            string deletemsg = indir_mng.indirimSil(silId);
            if (deletemsg.Contains("Başarılı"))
            {
                return RedirectToAction("indirimListesi");
            }
            return View();
        }
        public ActionResult indirimGuncelle(int guncelleId)
        {
            var tiklanan = indir_mng.indirimListesi().FirstOrDefault(k => k.indirimlerID == guncelleId);
            return View(tiklanan);
        }

        [HttpPost]
        public ActionResult indirimGuncelle(indirimler guncelle)
        {
            string UpdateResult = indir_mng.indirimGuncelle(guncelle);
            var liste = indir_mng.indirimListesi().FirstOrDefault(k => k.indirimlerID == guncelle.indirimlerID);

            return View(liste);
        }

        [HttpGet]
        public ActionResult indirimEkleIndex()
        {
            ViewBag.TurListesi = indir_mng.indirimTurListesi();
            return View();
        }

        [HttpPost]
        public ActionResult indirimEkleIndex(string indirimTurAdi, string fiyatSekli, DateTime baslamaTarih, DateTime? bitisTarih, decimal miktar, bool durumu, string Aciklama)
        {
            DateTime SonTarih = bitisTarih ?? DateTime.MinValue;
            string insertResult = indir_mng.indirimEkle(indirimTurAdi, fiyatSekli, baslamaTarih, SonTarih, miktar, durumu, Aciklama);
            if (insertResult.Contains("Eklendi"))
            {
                return RedirectToAction("indirimListesi");
            }
            ViewBag.sonuc = insertResult;
            return View();
        }
    }
}
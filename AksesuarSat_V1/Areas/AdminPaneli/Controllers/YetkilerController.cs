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
    public class YetkilerController : Controller
    {
        // GET: AdminPaneli/Yetkiler
        public ActionResult Index()
        {
            return View();
        }

        YetkilerManage yetki_mng = new YetkilerManage();
        public ActionResult YetkiListesi(int? sayfaNo)
        {
            int numara = sayfaNo ?? 1;
            var liste = yetki_mng.YetkilerListesi().ToPagedList(numara, 5);
            return View(liste);
        }

        public ActionResult YetkiSil(int silId)
        {
            var sil = yetki_mng.YetkilerListesi().FirstOrDefault(k => k.YetkilerID == silId);
            return View(sil);
        }

        [HttpPost, ActionName("YetkiSil")]
        public ActionResult DeleteYetki(int silId)
        {
            string deletemsg = yetki_mng.YetkiSil(silId);
            if (deletemsg.Contains("başarılı"))
            {
                return RedirectToAction("YetkiListesi");
            }
            return View();
        }
        PersonelManage per_mng = new PersonelManage();

        [HttpGet]
        public ActionResult YetkiGuncelle(int guncelleId)
        {

            var veri = yetki_mng.YetkilerListesi().FirstOrDefault(k => k.YetkilerID == guncelleId);
            //return View(veri);

            List<SelectListItem> SanalList = new List<SelectListItem>();
            foreach (var item in per_mng.PersonelListesi())
            {
                SanalList.Add(new SelectListItem
                {
                    Text = item.Adi,
                    Value = item.PersonellerID.ToString()
                });
            }
            ViewBag.PersonelID = SanalList;
            return View(veri);

        }

        [HttpPost]
        public ActionResult YetkiGuncelle(Yetkiler ekle)
        {
            string updResult = yetki_mng.YetkiGuncelle(ekle);
            //var liste = yetki_mng.YetkilerListesi().FirstOrDefault(k => k.YetkilerID == ekle.YetkilerID);
            return RedirectToAction("YetkiListesi");
        }

        [HttpGet]
        public ActionResult YetkiEkle()
        {
            ViewBag.PersonelID = PersonelDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult YetkiEkle(Yetkiler nesne)
        {
            string insertResult = yetki_mng.YetkiEkle(nesne.YetkiAdi, nesne.PersonelID);


            if (insertResult.Contains("Eklendi"))
            {
                return RedirectToAction("YetkiListesi", yetki_mng.YetkilerListesi());
            }
            ViewBag.sonuc = insertResult;

            ViewBag.PersonelID = PersonelDropDown();
            return RedirectToAction("YetkiListesi", yetki_mng.YetkilerListesi());

        }


        public List<SelectListItem> PersonelDropDown()
        {
            List<SelectListItem> SanalList = new List<SelectListItem>();
            foreach (var item in per_mng.PersonelListesi())
            {
                SanalList.Add(new SelectListItem
                {
                    Text = item.Adi,
                    Value = item.PersonellerID.ToString()
                });
            }
            //ViewBag.sonuc = SanalList;
            return SanalList;
        }

    }
}
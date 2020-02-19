using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AksesuarSat_V1.Areas.AdminPaneli.Models;
using PagedList;//PagedList.MVC NugetPacket ile ekleme yapılan kütüphaneden alındı

namespace AksesuarSat_V1.Areas.AdminPaneli.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: AdminPaneli/Urunler
        public ActionResult Index()
        {
            return View();
        }
        UrunManage urun_mng = new UrunManage();
        KategoriManage kategori_mng = new KategoriManage();
        public ActionResult UrunListesi(int? sayfaNo)
        {
            //soru işareti null olabilir anlamına gelmektedir.
            int numara = sayfaNo??1;
            //??=> null olduğunda sayfa numarasını 1 olarak ata anlamına gelmektedir.
            var liste = urun_mng.UrunListesi().ToPagedList(numara, 5);//listeyi 5 satırla gönderiyorum
            //JQuery sayfalandırma yapılarına göre avantajları ve dezavantajları mevcut
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/AdminPaneli/Views/Urunler/_UrunListPartial.cshtml", liste);
            }
            return View(liste);
        }

        [HttpGet]
        public ActionResult UrunEkleIndex()
        {
            ViewBag.KategoriListesi = kategori_mng.KategoriListesi();
            //KategoriParentID 0 olan kategoriler Ana Kategori olarak belirlenmiş yapılardır.
            return View();
        }

        [HttpPost]//ekleme post işlemi için 
        public ActionResult UrunEkleIndex(string adi,int KategoriId,decimal fiyat,DateTime tarih,string aciklama)
        {
          string insertResult= urun_mng.UrunEkle(adi, KategoriId, fiyat, tarih, aciklama);
            if (insertResult.Contains("Eklendi"))
            {
                return View("UrunListesi", urun_mng.UrunListesi());
            }
            ViewBag.sonuc = insertResult;
            return View();
            
        }

        public ActionResult UrunDetay(int detayId)
        {
            var detay = urun_mng.UrunListesi().FirstOrDefault(k => k.UrunlerID == detayId);
            return View(detay);
        }

        public ActionResult UrunSil(int silId)
        {
            return View(urun_mng.UrunListesi().FirstOrDefault(k=>k.UrunlerID==silId));
        }

        [HttpPost,ActionName("UrunSil")]//UrunSil metodunun Post işlemi DeleteUrun metodu üzerinde gerçekleşeceği anlamına gelir.
        public ActionResult DeleteUrun(int silId)
        {
            string deletemsg = urun_mng.UrunSil(silId);
            if (deletemsg.Contains("başarılı"))
            {
                return RedirectToAction("UrunListesi");//RedirectToAction metotu çağırır,View() view sayfasını çağırır
            }
            return View();

        }

        public ActionResult UrunGuncelle(int guncelleId)
        {
            List<SelectListItem> kategorilist = new List<SelectListItem>();

            int kategori_Id = urun_mng.UrunListesi().FirstOrDefault(k => k.UrunlerID == guncelleId).Kategoriler.KategorilerID;

            ViewBag.Adikat = urun_mng.UrunListesi().FirstOrDefault(k => k.UrunlerID == guncelleId).Kategoriler.KategoriAdi; 

            //foreach ile  deki kategorileri listemize ekliyoruz
            foreach (var item in kategori_mng.KategoriListesi())
            {   //Text = isim 
                //Value = ID kısmıdır
                kategorilist.Add(new SelectListItem { Text = item.KategoriAdi, Value = item.KategorilerID.ToString(),
                    Selected = (item.KategorilerID == kategori_Id ? true : false),
                });
            }
            ViewBag.KategoriID = kategorilist;

            var veri = urun_mng.UrunListesi().FirstOrDefault(k => k.UrunlerID == guncelleId);
            return View(veri);
        }

        [HttpPost]
        public ActionResult ResimYukle(int urunId,HttpPostedFileBase[] TiklananResimler)
        {

            //web.config altında key adı resimdosyasi olan alana git.
            //using System.Configuration; ekle
            string KlasorYap = ConfigurationManager.AppSettings["resimdosyasi"];
            string eldeedilenYol = Server.MapPath(KlasorYap);
            //using System.IO;
            bool sonuc = Directory.Exists(eldeedilenYol);
            if (sonuc==false)
            {
                Directory.CreateDirectory(eldeedilenYol);
                //klasör yoksa oluştur
            }
            #region  Tanımm
            //HttpPostedFileBase[] TiklananResimler=> resimleri dizi gibi tutmasını sağlar
            //return View();
            //ekleme olduktan sonra view yerine HttpStatusCodeResult yapısını tanımlayarakresim üzerinde onay ikonu çıkmaısını sağlayacktır.
            //8fdbae05-4d99-4901-8635-6c316d7316d1
            //string resimKodu_k = $"Resim-{urunId}_{urun_mng.UrunListesi().FirstOrDefault().Kategoriler.KategorilerID}{ Guid.NewGuid()}";
            #endregion

            foreach (var item in TiklananResimler)
            {
                string uzantiAl = item.ContentType.Split('/')[1];
     string resimKodu = $"Resim-{urunId}_{ Guid.NewGuid()}.{uzantiAl}";
                //Resim-10_c24cce50-7e5a-4c9f-8183-d691179488c2
                //d/resimler/14-dgf-14j.png
                string sonHali = eldeedilenYol + "/" + resimKodu;
                item.SaveAs(sonHali);//resimler klasöründe kaydet
                bool ekle = resim_mng.ResimEkle(urunId, resimKodu,item.FileName, null);
            }

            //using System.Net;
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        ResimManage resim_mng = new ResimManage();

        public ActionResult ResimSayfasi(int urunId)
        {
            return View(urun_mng.UrunListesi().FirstOrDefault(k=>k.UrunlerID==urunId));
        }

        //public ActionResult UrunResimleriPartial(int resimId)
        public ActionResult ResimListesi(int urunId)
        {
         var liste= resim_mng.UrununResimleri(urunId);
            //aldığı parametrelere göre overload metottur.
            return PartialView("UrunResimleriPartial", liste);
        }

        public ActionResult ResimSil(int silinecekResimId)
        {
            bool deletePicture = resim_mng.ResimSil(silinecekResimId);

            return View();
        }
    }
}
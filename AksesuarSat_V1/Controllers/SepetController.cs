using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Controllers
{
    public class SepetController : Controller
    {
        // GET: Sepet
        public ActionResult SepetListesi()
        {
            List<SepetSanalClass> sepet = new List<SepetSanalClass>();
            var sepeteGidecek =(List<Urunler>)Session["sepet"];

            foreach (var item in sepeteGidecek)
            {
                var urunBilgisi = sepet.Where(k => k.urun_Id == item.UrunlerID).FirstOrDefault();
                if (urunBilgisi!=null)//ürün daha önce eklenmişse
                {
                    //sepette aynıürün daha önce eklendiyse yapılacak işlemler
                    urunBilgisi.adet = urunBilgisi.adet + 1;
                    urunBilgisi.toplam_fiyat = urunBilgisi.toplam_fiyat + item.BirimFiyat;
                }
                else//ürün ilk kez sepete eklenirken
                {
                    //yeni bir ürün sepete eklendiğinde aşağıdaki işlemler yapılacak
                    SepetSanalClass ekle = new SepetSanalClass();//ilk eklenece ürün
                    ekle.urun_Id = item.UrunlerID;
                    ekle.urun_adi = item.UrunAdi;
                    ekle.adet = 1;
                    ekle.urun_fiyat = item.BirimFiyat;
                    #region indirim işlemleri
                    //***********************************************
                    //if (true)
                    //{

                    //}
                    //ekle.indirim = item.UrunlerID;
                    //*********************************************** 
                    #endregion
                    ekle.toplam_fiyat = item.BirimFiyat;
                    ekle.resim = item.Resimler.FirstOrDefault().Resim;
                    sepet.Add(ekle);

                }
            }

            return View(sepet);
        }

        public ActionResult SepetListesi1()
        {
            return View();
        }

        public class SepetSanalClass
        {
            public int urun_Id { get; set; }
            public string urun_adi { get; set; }
            public decimal urun_fiyat { get; set; }
            public decimal toplam_fiyat { get; set; }
            public decimal indirim { get; set; }
            public string resim { get; set; }
            public int adet { get; set; }
            
        }

    }
}
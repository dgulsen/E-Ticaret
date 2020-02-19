using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;
using FakeData;


namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    public class UrunManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();
        Random rnd = new Random();
        public List<Urunler> UrunListesi()
        {
            return db.Urunler.ToList();
        }

        public string UrunEkle(string urunadi, int urunkategoriId, decimal urunfiyat, DateTime uruneklenmetarihi, string aciklama)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(urunadi) && urunfiyat > 0 && urunkategoriId > 0)
                {
                    if (VarmiUrun(urunadi))
                    {
                        Urunler ekle = new Urunler();
                        ekle.UrunAdi = urunadi;
                        ekle.KategoriID = urunkategoriId;
                        ekle.BirimFiyat = urunfiyat;
                        ekle.EklenmeTarihi = uruneklenmetarihi;
                        ekle.Aciklama = aciklama;

                        db.Urunler.Add(ekle);
                        if (db.SaveChanges() > 0)
                        {
                            return "Eklendi";
                        }
                        return "eklenirken hata oluştu";
                    }
                    return "Aynı ürün adı mevcut";
                }
                return "Ürün alanlarında boşluk değeri girdiniz";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool VarmiUrun(string adi)
        {
            return db.Urunler.Any(k => k.UrunAdi != adi);
        }

        public string UrunSil(int urunId)
        {
            try
            {
                var silinecekId = db.Urunler.FirstOrDefault(k => k.UrunlerID == urunId);
                db.Urunler.Remove(silinecekId);
                if (db.SaveChanges() > 0)
                {

                    return "Silme başarılı";
                }
                return "Silme olmadı";
            }
            catch (Exception rt)
            {
                return rt.Message;
            }
        }

        public void SiparisDetayFakeData()
        {
            double[] indirim = { 0.01, 0.1, 0.015, 0.02, 0.09, 0.08 };

            for (int i = 0; i < 8000; i++)
            {
                var adet = db.SiparisDetaylar.Count();
                if (adet<800)
                {
                    try
                    {
                        SiparisDetaylar ekle = new SiparisDetaylar();
                        int sID;
                        int uID;
                        bool varmi;
                        do
                        {
                            sID = rnd.Next(1, 2000);
                            uID = rnd.Next(1, 180);
                            varmi = db.SiparisDetaylar.Any(k => k.SiparisID == sID && k.UrunID == uID);
                        } while (varmi);

                        ekle.SiparisID = sID;
                        ekle.UrunID = uID;

                        ekle.BirimFiyat = (decimal)(NumberData.GetDouble() * 100);
                        ekle.Miktar = rnd.Next(1, 8);
                        ekle.indirimID = rnd.Next(1, 10);
                        db.SiparisDetaylar.Add(ekle);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    break;
                }
            }
        }

        public void SiparisFakeData()
        {
            var sayi = db.Siparisler.Count();
            if (sayi < 2000)
            {
                for (int i = 0; i < 1500; i++)
                {
                    Siparisler ekle = new Siparisler();
                    ekle.UyeID = rnd.Next(1, 2000);
                    ekle.SiparisTarihi = DateTimeData.GetDatetime(Convert.ToDateTime("01.01.2005"), DateTime.Now);
                    ekle.SiparisTeslimTarihi = ekle.SiparisTarihi.AddDays(rnd.Next(3, 30));
                    ekle.Tutar = (decimal)rnd.NextDouble();
                    ekle.KargoTutari = (decimal)rnd.NextDouble();
                    ekle.Aciklama = TextData.GetSentences(1);
                    db.Siparisler.Add(ekle);
                    db.SaveChanges();
                }
            }

        }
    }
}
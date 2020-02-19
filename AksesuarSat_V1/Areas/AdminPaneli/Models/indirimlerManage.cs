using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;


namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    public class indirimlerManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();

        public List<indirimler> indirimListesi()
        {
            return db.indirimler.ToList();
        }

        public List<indirimTurleri> indirimTurListesi()
        {
            return db.indirimTurleri.ToList();
        }

        public string indirimSil(int indirimId)
        {

            try
            {
                var silinecekId = db.indirimler.FirstOrDefault(k => k.indirimlerID == indirimId);
                db.indirimler.Remove(silinecekId);
                if (db.SaveChanges() > 0)
                {
                    return "Başarılı";
                }
                return "Başarısız";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string indirimGuncelle(indirimler gnc)
        {
            var upt = db.indirimler.FirstOrDefault(k => k.indirimlerID == gnc.indirimlerID);

            db.Entry(upt).CurrentValues.SetValues(gnc);
            if (db.SaveChanges() > 0)
            {
                return "Başarılı";
            }
            return "Başarısız";
        }

        public string indirimEkle(string indirimTurAdi, string fiyatSekli, DateTime baslamaTarih, DateTime bitisTarih, decimal miktar, bool durumu, string Aciklama)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(indirimTurAdi))
                {
                    indirimler ekle = new indirimler();
                    ekle.indirimTurAdi = indirimTurAdi;
                    ekle.indirimFiyatSekli = fiyatSekli;
                    ekle.BaslamaTarihi = baslamaTarih;
                    ekle.BitisTarihi = bitisTarih;
                    ekle.indirimMiktari = miktar;
                    ekle.Durumu = durumu;
                    ekle.Aciklama = Aciklama;
                    db.indirimler.Add(ekle);
                    if (db.SaveChanges() > 0)
                    {
                        return "Eklendi";
                    }
                    return "Hata";
                }
                return "Bilgileri Kontrol Ediniz.";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

    }
}
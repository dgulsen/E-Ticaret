using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    public class ReklamManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();

        public List<Reklamlar> ReklamListesi()
        {
            return db.Reklamlar.ToList();
        }

        public string ReklamEkle(string reklamaciklama, DateTime reklambaslangictarihi, DateTime reklambitistarihi, string resim1, string resim2, string resim3)
        {
            try
            {

                if (VarmiReklam(reklamaciklama))
                {
                    if (!string.IsNullOrWhiteSpace(reklamaciklama))
                    {
                        Reklamlar ekle = new Reklamlar();
                        ekle.ReklamAciklama = reklamaciklama;
                        ekle.ReklamBaslangicTarihi = reklambaslangictarihi;
                        ekle.ReklamBitisTarihi = reklambitistarihi;
                        ekle.Resim1 = resim1;
                        ekle.Resim2 = resim2;
                        ekle.Resim3 = resim3;

                        db.Reklamlar.Add(ekle);
                        if (db.SaveChanges() > 0)
                        {
                            return "Eklendi";
                        }
                        return "Eklenirken hata oluştu";
                    }
                    return "Aynı reklam mevcut";
                }
                return "Reklam Alanlarında Boşluk Değeri Giriniz";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public bool VarmiReklam(string reklamaciklama)
        {
            var varmi = db.Reklamlar.Where(k => k.ReklamAciklama == reklamaciklama).FirstOrDefault();
            if (varmi != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string ReklamSil(int reklamId)
        {
            try
            {
                var silinecekId = db.Reklamlar.FirstOrDefault(k => k.ReklamID == reklamId);
                db.Reklamlar.Remove(silinecekId);
                if (db.SaveChanges() > 0)
                {

                    return "Başarıyla Silindi";
                }
                return "Silme işlemi Başarısız";
            }
            catch (Exception rt)
            {
                return rt.Message;

            }
        }

        public string ReklamGuncelle(Reklamlar gnc)
        {
            //Nesne olarak parametre verdik.Nesne olarak gelen değerleri tek bir parametre ile alıyoruz.

            var upt = db.Reklamlar.FirstOrDefault(k => k.ReklamID == gnc.ReklamID);
            db.Entry(upt).CurrentValues.SetValues(gnc);
            //tutulan ID değerine sahip personel(upt) tablosuna giriş yapıldığında(Entry) varsayılan değerleri ile (CurrentValues) girilen yeni değerleri (gnc) güncelle (SetValues)
            if (db.SaveChanges() > 0)
            {
                return "Güncellendi";
            }
            return "Başarısız";
        }
    }
}
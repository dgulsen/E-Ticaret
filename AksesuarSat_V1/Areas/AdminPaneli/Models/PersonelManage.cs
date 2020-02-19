using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    
    public class PersonelManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();
        public List<Personeller> PersonelListesi()
        {
            return db.Personeller.ToList();
        }

        public string PersonelEkle(string adi, string soyadi, string telefon, string adres, string email, string Cinsiyet, string medeniHali, string dogumYeri, DateTime dogumtarih, DateTime isegirisTarihi)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(adi) && (!string.IsNullOrWhiteSpace(soyadi)) &&
                    (!string.IsNullOrWhiteSpace(telefon)))
                {
                    if (VarmiPersonel(adi, soyadi, telefon))
                    {
                        Personeller ekle = new Personeller();
                        ekle.Adi = adi;
                        ekle.Soyadi = soyadi;
                        ekle.Telefon = telefon;
                        ekle.Adres = adres;
                        ekle.Email = email;
                        ekle.Cinsiyet = Cinsiyet;
                        ekle.MedeniHali = medeniHali;
                        ekle.DogumYeri = dogumYeri;
                        ekle.DogumTarihi = dogumtarih;
                        ekle.IseGirisTarihi = isegirisTarihi;
                        db.Personeller.Add(ekle);

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
        public bool VarmiPersonel(string adi, string soyadi, string telefon)
        {
            var varmi = db.Personeller.Where(k => k.Adi == adi && k.Soyadi == soyadi && k.Telefon == telefon).FirstOrDefault();
            if (varmi != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string PersonelGuncelle(Personeller gnc)
        {
            //Nesne olarak parametre verdik.Nesne olarak gelen değerleri tek bir parametre ile alıyoruz
            var upt = db.Personeller.FirstOrDefault(k => k.PersonellerID == gnc.PersonellerID);

            //upt.Adi = gnc.Adi;

            db.Entry(upt).CurrentValues.SetValues(gnc);
            //tutulan ID değerine sahip personel(upt) tablosuna giriş yapıldığında (Entry)varsayılan değerleri ile (CurrentValues) girilen yeni değerleri(gnc) güncelle(SetValues).
            if (db.SaveChanges()>0)
            {
                return "Güncellendi";
            }
            return "Başarısız";
        }

    }
}
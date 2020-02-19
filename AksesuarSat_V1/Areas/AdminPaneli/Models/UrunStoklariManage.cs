using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{

    public class UrunStoklariManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();
        public List<UrunStoklari> UrunStoklariListesi()
        {
            return db.UrunStoklari.ToList();
        }
        public string UrunStokEkle(int UrunID, DateTime basTarihi, int stokmiktari, string aciklama)
        {
            UrunStoklari ekle = new UrunStoklari();
            ekle.UrunID = UrunID;
            ekle.BaslangicTarih = basTarihi;
            ekle.StokMiktari = stokmiktari;
            ekle.KalanUrun = stokmiktari;
            ekle.Aciklama = aciklama;

            db.UrunStoklari.Add(ekle);
            if (db.SaveChanges() > 0)
            {
                return "Eklendi";
            }
            return "Eklerken hata oluştu";

        }
        public string UrunStokSil(int silId)
        {
            var silinecekId = db.UrunStoklari.FirstOrDefault(k => k.UrunStoklariID == silId);
            db.UrunStoklari.Remove(silinecekId);
            if (db.SaveChanges() > 0)
            {
                return "Ürün stoğu başarılı bir şekilde silindi";
            }
            return "Silme işlemi başarısız";
        }
        public string UrunStokGuncelle(UrunStoklari guncelle)
        {
            var upt = db.UrunStoklari.FirstOrDefault(k => k.UrunStoklariID == guncelle.UrunStoklariID);
            db.Entry(upt).CurrentValues.SetValues(guncelle);
            if (db.SaveChanges() > 0)
            {
                return "Ürün stoğu başarılı bir şekilde güncellendi.";
            }
            return "Güncellerken hata oluştu.";
        }
    }

}
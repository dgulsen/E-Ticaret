using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    public class UrunFiyatlariManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();

        #region LİSTE
        public List<UrunFiyatlari> UrunFiyatlariListesi()
        {
            return db.UrunFiyatlari.ToList();
        }
        #endregion

        #region SİL
        public string UrunFiyatiSil(int urunFiyatlariId)
        {
            try
            {
                var sil = db.UrunFiyatlari.FirstOrDefault(i => i.UrunFiyatlariID == urunFiyatlariId);
                db.UrunFiyatlari.Remove(sil);
                if (db.SaveChanges() > 0)
                {
                    return "Başarıyla silinme işleminiz gerçekleşmiştir.";
                }
                return "Silme sırasında bir hata meydana geldi";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        #endregion

        #region EKLE
        public string UrunFiyatiEkle(decimal urunAlisFiyati, decimal urunSatisFiyati, string fiyatBaslangicTarihi, string fiyatBitisTarihi, string aciklama, int urunId)
        {
            try
            {
                if (urunId > 0 && urunSatisFiyati > 0 && urunAlisFiyati > 0 && urunSatisFiyati > urunAlisFiyati)
                {
                    UrunFiyatlari ekle = new UrunFiyatlari();
                    ekle.UrunAlisFiyati = urunAlisFiyati;
                    ekle.UrunSatisFiyati = urunSatisFiyati;
                    ekle.FiyatBaslangicTarihi = Convert.ToDateTime(fiyatBaslangicTarihi);
                    ekle.FiyatBitisTarihi = Convert.ToDateTime(fiyatBitisTarihi);
                    ekle.Aciklama = aciklama;
                    ekle.UrunID = urunId;
                    db.UrunFiyatlari.Add(ekle);
                    if (db.SaveChanges() > 0)
                    {
                        return "Ürün fiyatı başarılı şekilde eklenmiştir.";
                    }
                    return "Ekleme sırasında bir hata meydana geldi";

                }
                return "Böyle bir UrunId mevcut değil";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        #endregion

        #region GÜNCELLE
        public string UrunFiyatiGuncelle(UrunFiyatlari urf)
        {
            try
            {
                var guncelle = db.UrunFiyatlari.FirstOrDefault(i => i.UrunFiyatlariID == urf.UrunFiyatlariID);
                db.Entry(guncelle).CurrentValues.SetValues(urf);
                if (db.SaveChanges() > 0)
                {
                    return "Ürün başarılı şekilde güncellenmiştir.";
                }
                return "Güncelleme esnasında bir hata meydana geldi";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        #endregion


    }

}
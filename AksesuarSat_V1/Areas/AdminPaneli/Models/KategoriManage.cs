using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    public class KategoriManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();
        public List<Kategoriler> KategoriListesi()
        {
            return db.Kategoriler.Where(k => k.KategoriParentID != 0).ToList();
        }

        public string KategoriEkle(string adi, int parentId,string aciklama)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(adi))
                {
                    if (KategoriVarmiSorgusu(adi, parentId))
                    {
                        Kategoriler ekle = new Kategoriler();
                        ekle.KategoriAdi = adi;
                        ekle.KategoriParentID = parentId;
                        ekle.KategoriAciklama = aciklama;

                        db.Kategoriler.Add(ekle);
                        if (db.SaveChanges()>0)
                        {
                            return "Eklendi";
                        }
                        return "Hata oluştu";
                    }
                    return "Aynı kategori mevcut";
                }
                return "Kategori adi boş geçilemez";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public bool KategoriVarmiSorgusu(string adi, int parentId)
        {
            var varmi = db.Kategoriler.Where(k => k.KategoriAdi == adi && k.KategoriParentID == parentId).FirstOrDefault();
            if (varmi == null)
            {
                return true;
            }
            return false;


        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    public class ResimManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();

        public List<Resimler> UrununResimleri(int urunId)
        {
            return db.Resimler.Where(k => k.UrunID == urunId).ToList();
        }
        public bool ResimEkle(int urunId,string resim,string resimadi,string aciklama)
        {
            try
            {
                Resimler ekle = new Resimler();
                ekle.Resim = resim;
                ekle.ResimAdi = resimadi;
                ekle.Aciklama = aciklama;
                ekle.UrunID = urunId;
                db.Resimler.Add(ekle);
                if (db.SaveChanges()>0)
                {
                    return true;
                }
                return false;                    
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool  ResimSil(int resimlerId)
        {
            try
            {
                var sil = db.Resimler.FirstOrDefault(k => k.ResimlerID == resimlerId);
                db.Resimler.Remove(sil);

                if (db.SaveChanges()>0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
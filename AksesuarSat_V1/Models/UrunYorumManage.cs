using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AksesuarSat_V1.Models
{
    public class UrunYorumManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();
        public List<Yorumlar> YorumListesi(int urunId)
        {
            return db.Yorumlar.Where(k => k.UrunID == urunId && k.YorumUstID==0).OrderByDescending(k=>k.YorumlarID).ToList();
        }

        public List<Yorumlar> YorumaYorum(int yorumId)
        {
            return db.Yorumlar.Where(k =>k.YorumUstID==yorumId).ToList();
        }

        //public Yorumlar NesneYorum(int urunId)
        //{
        //    Yorumlar nesne = db.Yorumlar.Where(k => k.UrunID == urunId).;
        //    return db.Yorumlar.Where()
        //}

        public bool YorumKaydet(Yorumlar nesne)
        {
            db.Yorumlar.Add(nesne);
            if (db.SaveChanges()>0)
            {
                return true;
            }
            return false;
        }

    }
}
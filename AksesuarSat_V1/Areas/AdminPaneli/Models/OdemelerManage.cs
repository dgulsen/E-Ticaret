using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    public class OdemelerManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();

        public List<Odemeler> OdemeListesi()
        {
            return db.Odemeler.ToList();
        }

        public List<Siparisler> SiparisListesi()
        {
            return db.Siparisler.ToList();
        }
        public string OdemeSil(int OdemeID)
        {
            var delete = db.Odemeler.Where(k => k.OdemelerID == OdemeID).FirstOrDefault();

            if (delete != null)
            {

                db.Odemeler.Remove(delete);
                if (db.SaveChanges() > 0)
                {
                    return "başarılı asdasd asda";
                }
                return "Gerçekleşmedi";
            }
            return "Id null";


        }


        public string Guncelle(Odemeler guncelle)
        {
            // Nesne olarak parametre verdik. NEsne olarak gelen degerleri tek bir parametre ile alıyoruz
            var x = db.Odemeler.Where(k => k.OdemelerID == guncelle.OdemelerID).FirstOrDefault();



            db.Entry(x).CurrentValues.SetValues(guncelle);
            //tutulan Id değerine sahip personel(upt) tablosuna giris yapildiginda (Entry)varsayilan degerler ile (CurrentValues) girilen yeni degerleri (gnc) guncelle(setValues)
            if (db.SaveChanges() > 0)
            {
                return "Personel bilgileri güncellendi";
            }
            return "Kayıtta hata olustu";
        }
    }
}

using AksesuarSat_V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    public class UyelerManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();
        public List<Uyeler> UyeListesi()
        {
            return db.Uyeler.ToList();
        }
        public List<Uyeler> UyeListesi(UyelerPartial nesne)
        {
            return db.Uyeler.Where(k=>(string.IsNullOrEmpty(nesne.UyeAdi)|| k.UyeAdi.Contains(nesne.UyeAdi))&&(string.IsNullOrEmpty(nesne.UyeSoyadi) || k.UyeSoyadi.Contains(nesne.UyeSoyadi)) ).ToList();
        }
        public string UyeGuncelle(Uyeler gnc)
        {
            var update = db.Uyeler.FirstOrDefault(k => k.UyelerID == gnc.UyelerID);
            db.Entry(update).CurrentValues.SetValues(gnc);
            if (db.SaveChanges() > 0)
            {
                return "Güncellendi";
            }
            return "Başarısız";
        }
        public string UyeSil(int uyeId)
        {
            try
            {
                var silinecekId = db.Uyeler.FirstOrDefault(k => k.UyelerID == uyeId);

                db.Uyeler.Remove(silinecekId);
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
    }

   public partial  class UyelerPartial:Uyeler
    {
        //Uyeler
        public int? SayfaNumarasi { get; set; }
        //using PagedList;
        public IPagedList<Uyeler> Uye_Listesi { get; set; }
    }
}


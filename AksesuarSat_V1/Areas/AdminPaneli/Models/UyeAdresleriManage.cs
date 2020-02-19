using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    public class UyeAdresleriManage
    {
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();



        public List<sp_UyeAdresSayisi_Result> UyeAdresListesi()
        {
            //var liste = db.UyeAdresSayisi().ToList();
            return db.sp_UyeAdresSayisi().ToList();
        }
        public List<UyeAdresleri> UyeAdres()
        {
            return db.UyeAdresleri.ToList();
        }

        public List<UyeAdresleri> UyeAdres(int uyeId)
        {
            return db.UyeAdresleri.Where(k => k.UyeID == uyeId).ToList();
        }

    }
}
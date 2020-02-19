
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AksesuarSat_V1.Models;

    namespace AksesuarSat_V1.Areas.AdminPaneli.Models
    {
        public class YetkilerManage
        {
            OyunTicaretDBEntities db = new OyunTicaretDBEntities();
            public List<Yetkiler> YetkilerListesi()
            {
                return db.Yetkiler.ToList();
            }

            public string YetkiSil(int yetkiId)
            {
                try
                {
                    var silinecekId = db.Yetkiler.FirstOrDefault(k => k.YetkilerID == yetkiId);
                    db.Yetkiler.Remove(silinecekId);
                    if (db.SaveChanges() > 0)
                    {
                        return "Silme başarılı.";
                    }
                    return "Silme başarısız...";
                }
                catch (Exception rt)
                {
                    return rt.Message;
                }
            }


            public string YetkiGuncelle(Yetkiler gnc)
            {

                var upt = db.Yetkiler.FirstOrDefault(k => k.YetkilerID == gnc.YetkilerID);

                db.Entry(upt).CurrentValues.SetValues(gnc);

                if (db.SaveChanges() > 0)
                {
                    return "Güncellendi";
                }
                return "Başarısız.";

            }

            public string YetkiEkle(string yetkiadi, int personelid)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(yetkiadi) && personelid > 0)
                    {

                        Yetkiler ekle = new Yetkiler();
                        ekle.YetkiAdi = yetkiadi;
                        ekle.PersonelID = personelid;


                        db.Yetkiler.Add(ekle);
                        if (db.SaveChanges() > 0)
                        {
                            return "Eklendi";
                        }
                        return "eklenirken hata oluştu";

                    }
                    return "Yetki alanlarında boşluk değeri girdiniz";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

        }
    }
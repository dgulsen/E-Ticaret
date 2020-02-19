using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        OyunTicaretDBEntities db = new OyunTicaretDBEntities();
        // GET: Urunler
        public ActionResult UrunIndex()
        {
            return View(db.Urunler.ToList());
        }

      
        public ActionResult UrunDetay()
        {
            int geleneData =Convert.ToInt32( RouteData.Values["id"]);
            //RouteData ile gelen verinin id değerini  App_Start klasörü altında RouteConfig sayfasında varsayılan id değerine göre değer alır. Bu değeri alıp kullanıyoruz
            
            return View(db.Urunler.ToList().FirstOrDefault(k=>k.UrunlerID==geleneData));
        }

        public ActionResult YorumKaydet(int urunId,string txtbaslik,string email,string txticerik)
        {
            Yorumlar yorumNesne = new Yorumlar();
            yorumNesne.UrunID = urunId;
            yorumNesne.YorumBaslik = txtbaslik; 
            yorumNesne.YorumIcerik = txticerik;
            yorumNesne.YorumTarih = DateTime.Now;
            var uyeYorum = db.Uyeler.FirstOrDefault(k => k.Email == email);
            if (uyeYorum!=null)
            {
                yorumNesne.UyeID = uyeYorum.UyelerID;
            }
            
            UrunYorumManage yor_mng = new UrunYorumManage();
            yor_mng.YorumKaydet(yorumNesne);
            var list = yor_mng.YorumListesi(urunId);
            return PartialView("~/Views/Urun/_YorumListPartial.cshtml",list);
        }
    }
}
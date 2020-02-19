using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;


namespace AksesuarSat_V1
{
    /// <summary>
    /// Summary description for SepeteEkle
    /// </summary>
    public class SepeteEkle : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {
        //System.Web.SessionState.IRequiresSessionState Neden eklendi??
        public void ProcessRequest(HttpContext prmicerik)
        {
            //GenericHandler tanımı 200108 de yazılacak
            //HttpContext=> sayfanın adres kısmın tutar,bu adres alanında yer alan ve bize sepet işlemlerinde lazım olan Session["sepet"] kullanacağız
            //Generic Handler sayfası aldığı parametre ile sayfa çalışırken işelmler yapılmasını, bu işlemleri veritabanında kaydetmeden tarayıcıda tutulmasını sağlar. Veritabanına veri gönderilmediğinden performans iyileştiricidir.
            var x =  prmicerik.Session["sepet"];
            OyunTicaretDBEntities db = new OyunTicaretDBEntities();
            if (x==null)//sepette hiç ürün yoksa
            {
                List<Urunler> liste = new List<Urunler>();
                int Id =Convert.ToInt32(prmicerik.Request.Form["UrunlerId"]);
                //HttpContext ile gelen istek (Request) bir form taşır ve bu form string olan parametresine Id verdik. bu parametre sepete ekleye tıklandığında gelen ürünId değerini tutar
                var urun = db.Urunler.Where(k => k.UrunlerID == Id).FirstOrDefault();
                liste.Add(urun);
                prmicerik.Session["sepet"] = liste;
            }
            else
            {
                List<Urunler> liste = new List<Urunler>();
                //**************************
                //sepet boşdeğilse aşağıdaki listeye tıklanan ürün eklenir.
                liste = (List<Urunler>)prmicerik.Session["sepet"];
                //***************************************
                int Id = Convert.ToInt32(prmicerik.Request.Form["UrunlerId"]);
                var urun = db.Urunler.Where(k => k.UrunlerID ==Id).FirstOrDefault();
                liste.Add(urun);
                prmicerik.Session["sepet"] = liste;
            }

            var sepetUrunleri =(List<Urunler>) prmicerik.Session["sepet"];
            int urunSayisi = sepetUrunleri.Count();
            decimal toplam = sepetUrunleri.Sum(k => k.BirimFiyat);

            prmicerik.Response.ContentType = "text/plain";
            string sonuc = string.Format("{0}",urunSayisi);

            prmicerik.Response.Write(sonuc);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AksesuarSat_V1.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Models
{
    public class LoginManage
    {

        OyunTicaretDBEntities db = new OyunTicaretDBEntities();
        public Uyeler LoginIn(string adi, string sifre)
        {
            var sorgu = db.Uyeler.Where(k => (k.NickName == adi || k.Email == adi) && k.Sifre == sifre).FirstOrDefault();


            return sorgu;
        }
    }
}
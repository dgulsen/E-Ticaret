using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AksesuarSat_V1.Areas.AdminPaneli.Models;

namespace AksesuarSat_V1.Areas.AdminPaneli.Controllers
{
    public class LoginController : Controller
    {

        LoginManage log_mng = new LoginManage();
        // GET: AdminPaneli/Login
        public ActionResult LoginIndex()
        {
          
            return View();
        }

        [HttpPost]
        public ActionResult LoginIndex(string KullaniciAdi,string Sifre )
        {

          var giris=log_mng.LoginIn(KullaniciAdi, Sifre);
            if (giris!=null)
            {
               //Session["Değişken adı"]=> Session bütün sayfalarda çağrılabilir. Özellikle login işlemlerinde çok kullanılan bir kod yapısıdır.Kullanıcı ile ilgili bütün bilgileri çekebiliriz ve istenilen sayfada kullanılabilir.
               //AdminLayout sayfasında kullanıcı adı ve kulanıcı email hesabı için iki session alıyorum
             
              Session["adi"] = giris.UyeAdi;
               Session["email"] = giris.Email;
                return RedirectToAction("DefaultIndex", "Default");
            }
            ViewBag.mesaj = "Hatalı Kullanıcı adı veya şifre";
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AksesuarSat_V1.Controllers
{
    /*KULLANILAN TEMPLATE linki
     * https://colorlib.com/wp/template/e-shop/     
         
         */
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        public ActionResult AnasayfaIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AnasayfaIndex(string yeni)
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
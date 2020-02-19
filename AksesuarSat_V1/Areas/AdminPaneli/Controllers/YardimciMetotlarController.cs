using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AksesuarSat_V1.Areas.AdminPaneli.Models;


namespace AksesuarSat_V1.Areas.AdminPaneli.Controllers
{
    public class YardimciMetotlarController : Controller
    {
        KategoriManage kat_mng = new KategoriManage();
        // GET: AdminPaneli/YardimciMetotlar
        public ActionResult DropDownListIndex()
        {
            //Sanal list oluşturduk
            List<SelectListItem> SanalList = new List<SelectListItem>();

            foreach (var item in kat_mng.KategoriListesi())
            {

                SanalList.Add(new SelectListItem
                {
                    //Sanal list in içini de Kategori verileri ile dloduruyorum
                    Text = item.KategoriAdi,//Adi
                    Value = item.KategorilerID.ToString()//Id değerini alır
                });
            }

            ViewBag.KategoriID = SanalList;
            return View();
        }
    }
}
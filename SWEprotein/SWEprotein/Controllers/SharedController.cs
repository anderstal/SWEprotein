using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEprotein.Models;

namespace SWEprotein.Controllers
{
    public class SharedController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        //Visar hur många varor det är i kundkorgen+total kostnad
        public ActionResult _CartSmall()
        {

            if (Session["cartList"] != null)
            {
                var cartSmallList = ((List<tbProduct>) Session["cartList"]).Distinct().ToList();

                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
                ViewBag.cartSum = ((List<tbProduct>)Session["cartList"]).Sum(prod => prod.iPrice * prod.iCount) + ":-";

                return View();
            }
            ViewBag.cartCount = "0";
            ViewBag.cartSum = "0:-";
            return View();
        }
        public ActionResult _TopList()
        {

            //Välj ut alla produkter i databasen och sortera på antal sålda produkter, flest -> minst
            List<tbProduct> productList = (from p in db.tbProducts
                                           select p).OrderByDescending(p => p.iItemsSold).Take(12).ToList();



            //Skicka med listan på top 5 populära produkter till vyn Shared/_PopProductsPartial.cshtml
            return View(productList);
        }



    }
}

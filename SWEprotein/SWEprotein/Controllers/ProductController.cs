using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SWEprotein.Models;

namespace SWEprotein.Controllers
{
    public class ProductController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
     
           
      
        public ActionResult Index(string searchString, int? pagination)
        {

            var productList = db.tbProducts.OrderBy(c => c.iID).Take(16).ToList();
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            if (pagination.HasValue)
            {
                switch (pagination)
                {
                    case (0):
                    {
                         
                        break;
                    }
                    case (1):
                    {
                        productList = db.tbProducts.OrderBy(c => c.iID).Skip(12).Take(12).ToList();
                        break;
                    }
                    case (2):
                        {
                            productList = db.tbProducts.OrderBy(c => c.iID).Skip(24).Take(12).ToList();
                            break;
                        }
                    case (3):
                        {
                            productList = db.tbProducts.OrderBy(c => c.iID).Skip(36).Take(12).ToList();
                            break;
                        }
                    case (4):
                        {
                            productList = db.tbProducts.OrderBy(c => c.iID).Skip(48).Take(12).ToList();
                            break;
                        }
                    case (5):
                        {
                            productList = db.tbProducts.OrderBy(c => c.iID).Skip(60).Take(12).ToList();
                            break;
                        }
                    case (6):
                        {
                            productList = db.tbProducts.OrderBy(c => c.iID).Skip(72).Take(12).ToList();
                            pagination++;
                            break;
                        }
                }
            }

            if (searchString != null)
            {
                productList = (from prod in db.tbProducts.Where(c => c.sName.Contains(searchString)) select prod).ToList();
                return View(productList);
            }
           
            return View(productList);
        }


        public ActionResult Produkt(int id)
        {
            var singleProduct = from s in db.tbProducts.Where(c => c.iID == id) select s;
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            return View(singleProduct);
        }

        public ActionResult Sortiment(int id)
        {
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            var typeList = db.tbProducts.Where(c => c.iProductType == id).ToList();
            return View("Index", typeList);

        }

        public ActionResult Topplistan()
        {
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            var topList = db.tbProducts.OrderBy(c => c.iItemsSold).Take(8).ToList();
            return View(topList);
        }
    }
}

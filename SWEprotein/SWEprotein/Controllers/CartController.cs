using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using SWEprotein.Models;

namespace SWEprotein.Controllers
{
    public class CartController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        List<tbProduct> basket = new List<tbProduct>();

        //Kundkorg.cshtml, listar vad som finns i kundkorgen.
        public ActionResult Kundkorg()
        {
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            if (Session["cartList"] == null)
            {
                return View("EmptyCart");
            }
            if (((List<tbProduct>)Session["cartList"]).Count == 0)
            {
                return View("EmptyCart");
            }
            var cartList = ((List<tbProduct>)Session["cartList"]);

            return View(cartList);
        }

        //Tar bort produkt från kundkorgen
        public ActionResult CartRemove(int? id)
        {
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            ((List<tbProduct>)Session["cartList"]).RemoveAll(c => c.iID == id);
            return RedirectToAction("Kundkorg");
        }
        //Lägger till produkt i kundkorgen
        public ActionResult CartAdd(int? id)
        {
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            var findProduct = (from f in db.tbProducts.Where(c => c.iID == id) select f).FirstOrDefault();

            if (Session["cartList"] == null)
            {
                Session["cartList"] = basket;
            }

            //Kollar om produkten redan finns i listan.
            bool checkCartList = ((List<tbProduct>)Session["cartList"]).Any(c => c.iID == id);
            if (checkCartList == false)
            {
                ((List<tbProduct>)Session["cartList"]).Add(findProduct);
                return RedirectToAction("Index", "Product");
            }
            foreach (tbProduct prod in ((List<tbProduct>)Session["cartList"]).Where(c => c.iID == id))
            {
                prod.iCount++;
            }
            return RedirectToAction("Index", "Product");
          


        }

        public ActionResult CheckOut()
        {
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            //string AgentID; //mitt konto/integration
            //string Key; //md5, mitt konto/integration
            //string Description = "SWEProtein";
            //string SellerEmail = "gymuser1@gmail.com";
            //int payson_totalsumma = ((List<tbProduct>) Session["cartList"]).Sum(c => c.iPrice);
            //string BuyerEmail = ((tbUser) Session["activeUser"]).sEmail;
            //int Cost = payson_totalsumma;
            //int ExtraCost; //t.ex. frakten
            //string OkUrl; //betalningen lyckas
            //string CancelUrl;
            //int RefNr = ((tbUser) Session["activeUser"]).iID;
            //string GuaranteeOffered = "1";
            //string MD5string = SellerEmail + ":" + Cost + ":" + ExtraCost + ":" + OkUrl + ":" + GuaranteeOffered
            //string MD5Hash = MD5(MD5string);

            var order = new tbOrder
            {
                iUserID = 2, //Byt till Session["login"].ID
                iStatus = 1,
                iSum = ((List<tbProduct>)Session["cartList"]).Sum(prod => prod.iPrice * prod.iCount),
                dtOrderDate = DateTime.Now

            };

            db.tbOrders.InsertOnSubmit(order);
            db.SubmitChanges();
            foreach (tbProduct prod in ((List<tbProduct>)Session["cartList"]))
            {
                var prodOrder = new tbProductOrder
                {
                    iOrderID = order.iID,
                    iProductID = prod.iID,
                    iQuantity = prod.iCount,
                    iPrice = prod.iPrice
                };
                db.tbProductOrders.InsertOnSubmit(prodOrder);

            }
            db.SubmitChanges();
            return View(); //Gå till för "färdig" betalning
        }



    }
}

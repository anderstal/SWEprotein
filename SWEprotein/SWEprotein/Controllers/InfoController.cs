using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEprotein.Models;

namespace SWEprotein.Controllers
{
    public class InfoController : Controller
    {
        //
        // GET: /Information/



        public ActionResult KostScheman()
        {
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            return View();
        }

        public ActionResult Info()
        {
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            return View();
        }

        public ActionResult Erbjudanden()
        {
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Sum(c => c.iCount);
            }
            return View();
        }
    }
}

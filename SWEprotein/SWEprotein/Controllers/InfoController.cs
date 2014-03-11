using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWEprotein.Controllers
{
    public class InfoController : Controller
    {
        //
        // GET: /Information/



        public ActionResult KostScheman()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }
    }
}

using MVCCompressResponseFilter.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCompressResponseFilter.Controllers
{
    public class HomeController : Controller
    {
        [Compress]
        public ActionResult Compressed()
        {
            return View();
        }

        public ActionResult NotCompressed()
        {
            return View();
        }
    }
}

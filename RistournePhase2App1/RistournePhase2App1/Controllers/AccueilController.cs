using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RistournePhase2App1.Controllers
{
    public class AccueilController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult APropos()
        {
            ViewBag.Message = "Notre Mission.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Nos Informations";

            return View();
        }

    }
}

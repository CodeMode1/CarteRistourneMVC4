using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RistournePhase2App1.Controllers
{
    public class LogOutController : Controller
    {
        //
        // GET: /LogOut/

        public ActionResult LogOut()
        {
            Session.Remove("usagerCourriel");
            return RedirectToAction("Index", "Accueil");
        }

    }
}

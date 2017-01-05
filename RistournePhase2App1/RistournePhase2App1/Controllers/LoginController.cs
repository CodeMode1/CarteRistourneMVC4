using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RistournePhase2App1.Models;

namespace RistournePhase2App1.Controllers
{
    public class LoginController : Controller
    {
        private RistPhase2Db db = new RistPhase2Db();
        //pour garder track du nombre d'echec pour un meme utilisateur (ne pas remettre le compteur a 0 a chaque appelle de fonction)
        private static int echecLogin = 0;

        // GET: /Login/
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.oublieMotPasse = false;
            return View();
        }

        //POST: /Login/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Supporteur model)
        {
            var erreurLogin = "";
            ViewBag.oublieMotPasse = false;
            if (model.Courriel == null || model.Courriel == "")
            {
                erreurLogin = "Le courriel est incorrect.";
                ModelState.AddModelError("", erreurLogin);
            }
            if (model.MotPasse == null || model.MotPasse == "")
            {
                erreurLogin = "Le mot de passe est incorrect.";
                ModelState.AddModelError("", erreurLogin);
            }         
            if (ModelState.IsValid)
            {
                var supporteurMP =
                    (from supporteur in db.Supporteurs
                    where supporteur.Courriel.Equals(model.Courriel)
                    select supporteur.MotPasse).FirstOrDefault();

                if (!string.IsNullOrEmpty(supporteurMP) && (supporteurMP == model.MotPasse))
                {
                    Session["usagerCourriel"] = model.Courriel;
                    Session.Timeout = 60;
                    //redirect à la section pour Supporteur seulement, (choisir oragnisme etc)
                    return RedirectToAction("Index", "Accueil");
                    //return Content("Bienvenue" + " " + Session["usagerCourriel"]);
                }
                else
                {
                    ViewBag.oublieMotPasse = true;
                }   
            }
            erreurLogin = "Le nom du supporteur ou le mot de passe est incorrect.";
            ModelState.AddModelError("", erreurLogin);
            echecLogin++;
            if (echecLogin > 2)
            {
                echecLogin = 0;
                return RedirectToAction("LogOut", "LogOut");
            }
            
            return View(model);
        }

    }
}

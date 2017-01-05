using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RistournePhase2App1.Models;
using System.Web.Mvc.Html;

namespace RistournePhase2App1.Controllers
{
    public class ResetMotPasseController : Controller
    {
        private RistPhase2Db db = new RistPhase2Db();
        //
        // GET: /ResetMotPasse/ResetMotPasse

        [AllowAnonymous]
        public ActionResult ResetMotPasse()
        {
            return View();
        }

        // POST: /ResetMotPasse/ResetMotPasse
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetMotPasse(Supporteur model)
        {
            var erreureMessage = "";
            ViewBag.repQuestionInvalide = "";
            if (model.Courriel == null || model.Courriel == "")
            {
                erreureMessage = "Le courriel est invalide. Réessayez";
                ModelState.AddModelError("", erreureMessage);
            }
            if (ModelState.IsValid)
            {
                var Courriel =
                    (from supporteur in db.Supporteurs
                     where supporteur.Courriel.Equals(model.Courriel)
                     select supporteur.Courriel).FirstOrDefault();

                if (!string.IsNullOrEmpty(Courriel) && (Courriel == model.Courriel))
                {
                    return RedirectToAction("RepQuestionSec", "ResetMotPasse", new { courriel = Courriel });
                    //return RedirectToAction("ResetMotPasse", "ResetMotPasse");
                    
                }
                else
                {
                    ViewBag.repQuestionInvalide = "La courriel est invalide. Réesseyez.";
                }
            }
            erreureMessage = "Login Impossible";
            ModelState.AddModelError("", erreureMessage);
            return View(model);
        }

        //
        // GET: /ResetMotPasse/RepQuestionSec

        [AllowAnonymous]
        public ActionResult RepQuestionSec(string courriel)
        {
            Supporteur model = new Supporteur();
            var choixQuestion =
                (from supporteur in db.Supporteurs
                 where supporteur.Courriel.Equals(courriel)
                 select supporteur.ChoixQuestion).FirstOrDefault();

            model.Courriel = courriel;
            model.ChoixQuestion = choixQuestion;
            return View(model);
        }

        // POST: /ResetMotPasse/RepQuestionSec
        [HttpPost]
        [AllowAnonymous]
        public ActionResult RepQuestionSec(Supporteur model)
        {
            var erreureMessage = "";
            if (model.ReponseQuestion == null || model.ReponseQuestion == "")
            {
                erreureMessage = "La réponse est invalide. Réessayez";
                ModelState.AddModelError("", erreureMessage);
            }
            if (ModelState.IsValid)
            {
                var repQuestion =
                     (from supporteur in db.Supporteurs
                      where supporteur.Courriel.Equals(model.Courriel)
                      select supporteur.ReponseQuestion).FirstOrDefault();

                if (!string.IsNullOrEmpty(repQuestion) && (repQuestion == model.ReponseQuestion))
                {
                    //accueil pour l'instant. Redirect vers la section pour les supporteurs, car login réussi.
                    return RedirectToAction("ChangeMotPasse", "ResetMotPasse", new { courriel = model.Courriel });
                }
                else
                {
                    erreureMessage = "La réponse à la question de sécurité est invalide. Réesseyez.";
                }
            }
            erreureMessage = "Login Impossible";
            ModelState.AddModelError("", erreureMessage);
            return View(model);

        }

        // GET: /ResetMotPasse/ChangeMotPasse/courriel
        public ActionResult ChangeMotPasse(string courriel)
        {
            Supporteur supporteur =
                (from supp in db.Supporteurs
                 where supp.Courriel.Equals(courriel)
                 select supp).FirstOrDefault();
            if (supporteur == null)
            {
                return HttpNotFound();
            }
            supporteur.MotPasse = "";
            return View(supporteur);
        }

        // POST: /ResetMotPasse/ChangeMotPasse/supporteur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeMotPasse(Supporteur supporteur)
        {
            Supporteur suppChangeMP =
               (from supp in db.Supporteurs
                where supp.Id == supporteur.Id
                select supp).FirstOrDefault();
            if (ModelState.IsValid)
            {
                suppChangeMP.MotPasse = supporteur.MotPasse;
                db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            return View(suppChangeMP);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}

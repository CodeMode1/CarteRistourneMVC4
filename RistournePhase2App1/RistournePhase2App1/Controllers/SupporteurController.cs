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
    public class SupporteurController : Controller
    {
        private RistPhase2Db db = new RistPhase2Db();

        // GET: /Supporteur/Details/id
        public ActionResult Details(int id = 0)
        {
            Supporteur supporteur = db.Supporteurs.Find(id);
            if (supporteur == null)
            {
                return HttpNotFound();
            }
            return View(supporteur);
        }

        // GET: /Supporteur/Create
        public ActionResult Create()
        {
            var listeQuestions =
                (from questions in db.Questions
                select questions).ToList();
            List<string> questionsBd = new List<string>();
            foreach (var question in listeQuestions)
            {
                questionsBd.Add(question.ChoixQuestion);
            }
            ViewBag.Questions = questionsBd;
            return View();
        }

        // POST: /Supporteur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supporteur supporteur)
        {
            if (ModelState.IsValid)
            {
                var erreureMessage = "";
                try
                {
                    Supporteur nouveauSupporteur =
                        (from supporteurBd in db.Supporteurs
                         where supporteurBd.Courriel.ToLower().Equals(supporteur.Courriel.ToLower())
                         select supporteurBd).FirstOrDefault();
                    //si il n'eciste pas déjà ce courriel, on l'insère dans la bd.
                    if (nouveauSupporteur == null)
                    {
                        //insérer le supporteur
                        db.Supporteurs.Add(supporteur);
                        db.SaveChanges();
                        // redirection à la section juste pour les supporteurs, index pour linstant
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        var listeQuestions =
                            (from questions in db.Questions
                            select questions).ToList();
                        List<string> questionsBd = new List<string>();
                        foreach (var question in listeQuestions)
                        {
                            questionsBd.Add(question.ChoixQuestion);
                        }
                        ViewBag.Questions = questionsBd;

                        erreureMessage = "Courriel Supporteur existe déjà. SVP Entrez un autre Courriel.";
                        ModelState.AddModelError("", erreureMessage);
                    }             
                }
                catch 
                {
                    erreureMessage = "Création du compte impossible. SVP Réesseyez ou contactez l'admin.";
                    ModelState.AddModelError("", erreureMessage);
                    return HttpNotFound();
                }
            }
            return View(supporteur);
        }

        // GET: /Supporteur/Edit/id
        public ActionResult Edit(int id = 0)
        {
            Supporteur supporteur = db.Supporteurs.Find(id);
            if (supporteur == null)
            {
                return HttpNotFound();
            }
            return View(supporteur);
        }

        // POST: /Supporteur/Edit/supporteur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supporteur supporteur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supporteur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
            }
            return View(supporteur);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

       
    }
}
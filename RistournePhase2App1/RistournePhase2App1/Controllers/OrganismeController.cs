using RistournePhase2App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using PagedList;

namespace RistournePhase2App1.Controllers
{
    public class OrganismeController : Controller
    {
        private RistPhase2Db db = new RistPhase2Db();

        public ActionResult Autocomplete(string term)
        {
            var model =
                db.Organismes
                .Where(org => org.Nom.StartsWith(term))
                .Take(10)
                .Select(org => new
                {
                    label = org.Nom
                });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: /Organisme/
        public ActionResult Index(string termeRecherche = null, int page = 1)
        {
            var model = 
                db.Organismes.Include("Campagnes")
                .OrderBy(org => org.Nom)
                .Where(org => termeRecherche == null || org.Nom.StartsWith(termeRecherche))
                .ToPagedList(page, 10);

            /*déterminer si la requête au serveur est une requete asynchrone, si oui rerender seulement les résultats recherches, 
             *  sinon retourne full view.
                est indiquer par un flag dans le http header.
             */
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Organismes", model);
            }

            return View(model);
        }

        // get choisir campagne pour 1 organisme
        public ActionResult ChoisirCampagne(int id)
        {
            Organisme organisme =
                (from org in db.Organismes.Include(org => org.Campagnes)
                 where org.Id == id
                 select org).FirstOrDefault();
            return View(organisme);
        }

        // get
        public ActionResult ChoisirParticipant(int id)
        {
            CampagneFinancement campagne =
                (from camp in db.CampagneFinancements.Include(camp => camp.Participants)
                 where camp.Id == id
                 select camp).FirstOrDefault();
            return View(campagne);

        }

        // get ajouter le participant choisi dans la collection de participant du supporteur logué.
        public ActionResult AjouterAMesParticipants(int id) 
        {
            var userCourriel = Session["usagerCourriel"].ToString();
            Participant participant =
                (from part in db.Participants
                 where part.Id == id
                 select part).FirstOrDefault();
            if (participant != null)
            {
                //ajouter le participant à la collection de participants du supporteur loggué.
                Supporteur supporteur =
                    (from supp in db.Supporteurs.Include(supp => supp.Participants)
                     where supp.Courriel.ToLower().Equals(userCourriel.ToLower())
                     select supp).FirstOrDefault();
                supporteur.Participants.Add(participant);
                db.SaveChanges();
            }
            // redirection à la section du supporteur ou il peut voir tous ses participants à lui. Pour y faire des commandes.
            return RedirectToAction("Index", "Organisme");
        }

        // get créer un participant pour une campagne spécifique (id)
        public ActionResult CreerParticipant(int id)
        {
            return View();
        }

        //post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreerParticipant(int id, Participant participant)
        {
            if (ModelState.IsValid)
            {
                var erreureMessage = "";
                try
                {
                    Participant nouveauParticipant =
                        (from part in db.Participants
                         where part.Courriel.ToLower().Equals(participant.Courriel.ToLower())
                         select part).FirstOrDefault();
                    //si il n'eciste pas déjà ce courriel, on l'insère dans la bd.
                    if (nouveauParticipant == null)
                    {
                        var userCourriel = Session["usagerCourriel"].ToString();
                        //insérer le supporteur
                        db.Participants.Add(participant);
                        //ajouter le participant à sa campagne respective.
                        CampagneFinancement campagne =
                            (from camp in db.CampagneFinancements.Include(camp => camp.Participants)
                            where camp.Id == id
                            select camp).FirstOrDefault();
                        campagne.Participants.Add(participant);
                        //ajouter le participant à la collection de participants du supporteur loggué.
                        Supporteur supporteur =
                            (from supp in db.Supporteurs.Include(supp => supp.Participants)
                             where supp.Courriel.ToLower().Equals(userCourriel.ToLower())
                             select supp).FirstOrDefault();
                        supporteur.Participants.Add(participant);
                        db.SaveChanges();
                        // redirection à la section du supporteur ou il peut voir tous ses participants à lui. Pour y faire des commandes.
                        return RedirectToAction("Index", "Organisme");
                    }

                }
                catch
                {
                    erreureMessage = "Création du compte impossible. SVP Réesseyez ou contactez l'admin.";
                    ModelState.AddModelError("", erreureMessage);
                    return HttpNotFound();
                }
            }
            return View(participant);
        }

    }
}

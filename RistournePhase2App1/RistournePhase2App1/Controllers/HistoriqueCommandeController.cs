using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RistournePhase2App1.Models;
using System.Data;
using System.Data.Entity;

namespace RistournePhase2App1.Controllers
{
    public class HistoriqueCommandeController : Controller
    {
        private RistPhase2Db db = new RistPhase2Db();
        // GET: /HistoriqueCommande/

        public ActionResult Index()
        {
            // chercher user logue
            var userCourriel = Session["usagerCourriel"].ToString();
            // chercher toutes les commandes du user logue pour les afficher
            List<Commande> model =
                db.Commandes
                .Where(comm => comm.Supporteur.Courriel.ToLower().Equals(userCourriel.ToLower()))
                .OrderByDescending(comm => comm.DateCree)
                .Select(comm => comm)
                .Take(20)
                .ToList();
            return View(model);
        }

    }
}

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
    public class FactureController : Controller
    {
        private RistPhase2Db db = new RistPhase2Db();
        //
        // GET: /Facture/

        public ActionResult Index(int commandeId = 0)
        {
            //courriel supp logue
            var userCourriel = Session["usagerCourriel"].ToString();
            Commande commande =
                //include campagne (1 seule) voir schema bd
                (from comm in db.Commandes.Include(comm => comm.Campagnes)
                 where comm.Id == commandeId
                 select comm).FirstOrDefault();
            var model =
                (from item in db.Items.Include(item => item.Commande).Include(item => item.Produit)
                 where item.CommandeId == commandeId
                 select new FactureViewModel {
                     Id = item.Id,
                     Quantite = item.Quantite,
                     Produit = item.Produit.Nom,
                     PrixUnitaire = item.Produit.PrixUnitaire,
                     Montant = item.Quantite * item.Produit.PrixUnitaire,
                     Ristourne = item.Produit.Rabais,
                     RistourneMontant = (item.Quantite * item.Produit.PrixUnitaire) * ((double)item.Produit.Rabais / (double)100)
                 });
            ViewBag.NomCampagne = commande.Campagnes.Nom;
            ViewBag.CommandeId = commandeId;

            ViewBag.NoFacture = "Commande" + commande.Id;
            string format = "yyyy-M-d";
            ViewBag.Date = (DateTime.Now).ToString(format);
            ViewBag.DateLimite = (commande.DateLimite).ToString(format);
            ViewBag.DateLivraison = (commande.DateLivraison).ToString(format);
            ViewBag.Supporteur = commande.CreePar;
            ViewBag.ModePaiement = commande.ModePaiement;
            ViewBag.Suivi = commande.Suivi;
            ViewBag.LieuLivraison = commande.LieuLivraison;

            ViewBag.MontantTotal = db.Items.Where(item => item.CommandeId == commandeId).Sum(item => item.Quantite * item.Produit.PrixUnitaire);
            ViewBag.RistourneTotal = (double)(db.Items.Where(item => item.CommandeId == commandeId).Sum((item => (item.Quantite * item.Produit.PrixUnitaire) * ((decimal)item.Produit.Rabais / (decimal)100))));

            ViewBag.monNbCommandes = db.Commandes.Where(comm => comm.Supporteur.Courriel == userCourriel).Count();

            return View(model);
        }

    }
}

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
    public class DashboardController : Controller
    {
        private RistPhase2Db db = new RistPhase2Db();

        // GET: /Dashboard/

        public ActionResult Index()
        {
            if (Session["usagerCourriel"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var userCourriel = Session["usagerCourriel"].ToString();
            Supporteur model =
                db.Supporteurs
                .Where(supp => supp.Courriel.ToLower().Equals(userCourriel.ToLower()))
                .Include(supp => supp.Participants.Select(part => part.Campagnes))
                .Select(supp => supp)
                .FirstOrDefault();
            return View(model);
        }

        //creer commande pour une campagne
        // id = id de la campagne
        public ActionResult Commande(int id)
        {
            List<char> listeSuiviComm = new List<char>();
            listeSuiviComm.Add(Convert.ToChar("O"));
            listeSuiviComm.Add(Convert.ToChar("N"));

            List<string> listeModePaiement = new List<string>();
            listeModePaiement.Add("Visa");
            listeModePaiement.Add("Mastercard");
            listeModePaiement.Add("Débit");
            ViewBag.listePaiements = listeModePaiement;
            ViewBag.listeSuivi = listeSuiviComm;
            ViewBag.campagneId = id;
            return View();
        }

        //post 
        // id  = id campagne
        //sauver une nouvelle commande pour la campagne et le supporteur logue.
        //setter les infos par défaut comme le nom de la commande, creer par...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Commande(int id, Commande commande)
        {
            var userCourriel = Session["usagerCourriel"].ToString();
            if (ModelState.IsValid)
            {
                var erreureMessage = "";
                try
                {
                    CampagneFinancement campagneCommande =
                        (from camp in db.CampagneFinancements.Include(camp => camp.Commandes)
                        where camp.Id == id
                        select camp).FirstOrDefault();
                    Supporteur supporteurLogue =
                        (from supp in db.Supporteurs.Include(supp => supp.Commandes)
                         where supp.Courriel.ToLower().Equals(userCourriel.ToLower())
                         select supp).FirstOrDefault();
                    if (campagneCommande != null)
                    {
                        commande.CreePar = userCourriel;
                        commande.Nom = "Commande" + userCourriel + (DateTime.Now).ToString("g");
                        commande.Supporteur = supporteurLogue;
                        // erreur : refere a une campagne, donc pas de S :
                        commande.Campagnes = campagneCommande;
                        campagneCommande.Commandes.Add(commande);
                        supporteurLogue.Commandes.Add(commande);
                        db.SaveChanges();

                        List<string> listeModePaiement = new List<string>();
                        listeModePaiement.Add("Visa");
                        listeModePaiement.Add("Mastercard");
                        listeModePaiement.Add("Débit");
                        ViewBag.listePaiements = listeModePaiement;

                        List<char> listeSuiviComm = new List<char>();
                        listeSuiviComm.Add(Convert.ToChar("O"));
                        listeSuiviComm.Add(Convert.ToChar("N"));
                        ViewBag.listeSuivi = listeSuiviComm;

                        ViewBag.campagneNom = campagneCommande.Nom;
                    }
                    else
                    {
                        erreureMessage = "Ne peut pas créer la commande. Réessayez.";
                        ModelState.AddModelError("", erreureMessage);
                    }
                }
                catch
                {
                    erreureMessage = "Création de la commande impossible. SVP Réesseyez ou contactez l'admin.";
                    ModelState.AddModelError("", erreureMessage);
                    return HttpNotFound();
                }
            }     
            ViewBag.campagneId = id;
            ViewBag.commandeId = commande.Id;
            return View(commande);         
        }

        // id = id de la commande
        public ActionResult AjoutItems(int id = 0)
        {
            //courriel supp logue
            var userCourriel = Session["usagerCourriel"].ToString();
            //chercher tous les items pour la commande (id)
            List<Item> model =
                (from items in db.Items.Include(items => items.Commande).Include(items => items.Produit)
                 where items.CommandeId == id
                 select items).ToList();
            ViewBag.monNbCommandes = db.Commandes.Where(comm => comm.Supporteur.Courriel == userCourriel).Count();
            ViewBag.listeProduits = db.Produits.ToList();
            ViewBag.CommandeId = id;
            return View(model);
        }

        // id = id du produit
        public ActionResult ChoisirProduit(int id = 0, int commandeId = 0)
        {
            Produit produit =
                (from prod in db.Produits.Include(prod => prod.Items)
                 where prod.Id == id
                 select prod).FirstOrDefault();
            Commande commande =
                (from comm in db.Commandes.Include(comm => comm.Items)
                 where comm.Id == commandeId
                 select comm).FirstOrDefault();
            if (produit != null && commande != null)
            {
                Item nouveauItem = new Item();
                nouveauItem.Produit = produit;
                nouveauItem.ProduitId = produit.Id;
                nouveauItem.Nom = "Item" + produit.Nom + commandeId;
                nouveauItem.Montant = produit.PrixUnitaire;
                // defaut quantite 1, pour etre editer dans Edit de AjoutItems
                nouveauItem.Quantite = 1;
                nouveauItem.Commande = commande;
                nouveauItem.CommandeId = commandeId;
                produit.Items.Add(nouveauItem);
                commande.Items.Add(nouveauItem);
                db.SaveChanges();
            }
            ViewBag.listeProduits = db.Produits.ToList();
            ViewBag.CommandeId = commandeId;
            //redirect a AjoutItems aavec le id de la commande, pour choisir d'autre produits, ne retourne pas une vue.
            return RedirectToAction("AjoutItems", "Dashboard", new { id = commandeId });
        }


        // GET: /Item/Edit/id
        public ActionResult Edit(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /Item/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View(item);
        }

        // GET: /Item/Delete/id

        public ActionResult Delete(int id, int commandeId)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /Item/Delete/id

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int commandeId)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("AjoutItems", "Dashboard", new { id = commandeId });
        }

        // GET: /Item/Details/id

        public ActionResult Details(int id = 0)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }       

    }
}

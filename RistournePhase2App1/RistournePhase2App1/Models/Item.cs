using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RistournePhase2App1.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Editable(false)]
        public string Nom { get; set; }
        [Required]
        public int Quantite { get; set; }
        public int Montant { get; set; }
        // foreign key
        public int ProduitId { get; set; }
        public Produit Produit { get; set; }
        // foreign key
        public int CommandeId { get; set; }
        public Commande Commande { get; set; }
    }
}
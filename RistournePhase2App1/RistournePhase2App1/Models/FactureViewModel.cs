using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RistournePhase2App1.Models
{
    public class FactureViewModel
    {
        public int Id { get; set; }
        public int Quantite { get; set; }
        public string Produit { get; set; }
        public int PrixUnitaire { get; set; }
        public int Montant { get; set; }
        public int Ristourne { get; set; }
        public double RistourneMontant { get; set; }
    }
}
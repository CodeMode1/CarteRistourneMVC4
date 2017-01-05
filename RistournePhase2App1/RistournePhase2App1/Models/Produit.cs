using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RistournePhase2App1.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Rabais %")]
        public int Rabais { get; set; }
        [Display(Name = "Prix Unitaire")]
        public int PrixUnitaire { get; set; }
        public string imageUrl { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
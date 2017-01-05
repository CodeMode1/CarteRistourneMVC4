using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RistournePhase2App1.Models
{
    public class Commande
    {
        public Commande()
        {
            DateTime now =  DateTime.Now;
            this.DateCree = now;
            //1mois
            this.DateLimite = now.AddDays(29);
            //2mois
            this.DateLivraison = now.AddDays(58);
        }

        public int Id { get; set; }
        [Editable(false)]
        public string Nom { get; set; }
        [Display(Name = "Mode Paiement")]
        [Required]
        public string ModePaiement { get; set; }
        [Editable(false)]
        [Display(Name = "Date Limite")]
        [DataType(DataType.DateTime)]
        public DateTime DateLimite { get; set; }
        public char Suivi { get; set; }
        [Editable(false)]
        [Display(Name = "Date Livraison")]
        [DataType(DataType.DateTime)]
        public DateTime DateLivraison { get; set; }
        [Editable(false)]
        [Display(Name = "Date Créée")]
        [DataType(DataType.DateTime)]
        public DateTime DateCree { get; set; }
        [Editable(false)]
        [Display(Name = "Créé Par")]
        public string CreePar { get; set; }
        [Display(Name = "Lieu Livraison")]
        [Required]
        public string LieuLivraison { get; set; }
        public Supporteur Supporteur { get; set; }
        public CampagneFinancement Campagnes { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
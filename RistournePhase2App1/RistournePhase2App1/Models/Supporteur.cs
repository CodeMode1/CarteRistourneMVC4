using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace RistournePhase2App1.Models
{
    public class Supporteur
    {
        public int Id { get; set; }

        [Display(Name = "Nom Supporteur")]
        public string Nom { get; set; }

        [Display(Name = "Prénom Supporteur")]
        public string Prenom { get; set; }

        [StringLength(100, ErrorMessage = "Le {0} doit être au moins {2} charactères de long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de Passe")]
        public string MotPasse { get; set; }

        [Required]
        public string Courriel { get; set; }

        public string Rue { get; set; }
        public string Ville { get; set; }
        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }
        [Display(Name = "Choisir une question : ")]
        public string ChoixQuestion { get; set; }
        [Display(Name = "Entrez votre réponse : ")]
        public string ReponseQuestion { get; set; }
        [Display(Name = "Se Souvenir de Moi ?")]
        public bool SouvenirMoi { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public ICollection<CampagneFinancement> Campagnes { get; set; }
        public ICollection<Commande> Commandes { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RistournePhase2App1.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Courriel { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }
        public ICollection<CampagneFinancement> Campagnes { get; set; }
        public ICollection<Organisme> Organismes { get; set; }
        public ICollection<Supporteur> Supporteurs { get; set; }
    }
}
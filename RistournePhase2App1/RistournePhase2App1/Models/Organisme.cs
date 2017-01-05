using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RistournePhase2App1.Models
{
    public class Organisme
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }
        [Display(Name = "Télécopieur")]
        public string Telecopieur { get; set; }
        public ICollection<CampagneFinancement> Campagnes { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
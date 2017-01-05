using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RistournePhase2App1.Models
{
    public class CampagneFinancement
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public Organisme Organisme { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public ICollection<Commande> Commandes { get; set; }
        public ICollection<Supporteur> Supporteurs { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RistournePhase2App1.Models
{
    public class Question
    {
        public int Id { get; set; }
        [Display(Name = "Choix Question")]
        [StringLength(100, ErrorMessage = "Le {0} doit être au moins {2} charactères de long.", MinimumLength = 3)]
        public string ChoixQuestion { get; set; }
    }
}
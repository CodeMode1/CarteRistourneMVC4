using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RistournePhase2App1.Models
{
    public class RistPhase2Db : DbContext
    {
        public RistPhase2Db()
            : base("DefaultConnection")
        {
        }

        public DbSet<Supporteur> Supporteurs { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Organisme> Organismes { get; set; }
        public DbSet<CampagneFinancement> CampagneFinancements { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
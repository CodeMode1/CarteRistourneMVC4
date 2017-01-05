namespace RistournePhase2App1.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RistournePhase2App1.Models.RistPhase2Db>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RistournePhase2App1.Models.RistPhase2Db context)
        {
            //donnée de population initiale de la BD
            // a chaque fois que le schema de la bd change (class c#) cette methode est appelée à chaque update de la BD
            //si ChoixQuestion n'existe pas il sera créé avec ses valeurs sinon il sera updaté.
            context.Produits.AddOrUpdate(
                m => m.Nom,
                new Produit { Nom = "Sports Aux Puces", Rabais = 9, PrixUnitaire = 25, imageUrl="../../Images/carteSportpuces.png" },
                new Produit { Nom = "L'Herbier", Rabais = 12, PrixUnitaire = 100, imageUrl = "../../Images/carteHerbier.png" },
                new Produit { Nom = "Esso", Rabais = 2, PrixUnitaire = 25, imageUrl = "../../Images/carteEsso.png" },
                new Produit { Nom = "IGA", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteIga.png" },
                new Produit { Nom = "Loblaws", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteLoblaws.png" },
                new Produit { Nom = "Maxi", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteMaxi.png" },
                new Produit { Nom = "Brunet", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteBrunet.png" },
                new Produit { Nom = "Petro-Canada", Rabais = 2, PrixUnitaire = 25, imageUrl = "../../Images/cartePetrocanada.png" },
                new Produit { Nom = "Provigo", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteProvigo.png" },
                new Produit { Nom = "Ultramar", Rabais = 2, PrixUnitaire = 25, imageUrl = "../../Images/carteUltramar.png" },
                new Produit { Nom = "SAQ", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteSaq.png" },
                new Produit { Nom = "Sobeys", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteSobeys.png" },
                new Produit { Nom = "Pasquier", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/cartePasquier.png" },
                new Produit { Nom = "Rona", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteRona.png" },
                new Produit { Nom = "Réno Dépôt", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteRenoDepot.png" },
                new Produit { Nom = "Canadian Tire", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteCanadianTire.png" },
                new Produit { Nom = "Metro", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteMetro.png" },
                new Produit { Nom = "Super C", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteSuperc.png" },
                new Produit { Nom = "Cineplex", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteCineplex.png" },
                new Produit { Nom = "Marcil", Rabais = 3, PrixUnitaire = 25, imageUrl = "../../Images/carteMarcil.png" }
            );
            context.Questions.AddOrUpdate(
                p => p.ChoixQuestion,
                new Question { ChoixQuestion = "Quel est le nom de votre mère?" },
                new Question { ChoixQuestion = "Quel est le prénom de votre neveu le plus agé?" },
                new Question { ChoixQuestion = "Quel est votre animal préféré?" },
                new Question { ChoixQuestion = "Quel est le prénom de votre grand-mère maternelle?" },
                new Question { ChoixQuestion = "Quel est le prénom de votre grand-mère paternelle?" },
                new Question { ChoixQuestion = "Quel est le prénom de votre dernier animal de compagnie?" }
            );

            if (!context.Organismes.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    context.Organismes.AddOrUpdate(org => org.Nom,
                        new Organisme { Nom = i.ToString() + "Org", Rue = i.ToString() + "Rue", Ville = "Montréal", CodePostal = "H" + i.ToString() + "S0B5", Telephone = i.ToString() + "14-345-3212", Telecopieur = i.ToString() + "434232" });
                }
            }

            if (!context.Participants.Any())
            {
                var participants = new List<Participant>
                {
                    new Participant { Nom = "Lapierre", Prenom = "Jean", Courriel = "Jean@Jean.com", Rue = "12343 Fournier", Ville = "Montréal", CodePostal = "H2s 7B4", Telephone = "534-232-1212" },
                    new Participant { Nom = "DuBertier", Prenom = "Pierre", Courriel = "Pierre@Pierre.com", Rue = "13343 Mexr", Ville = "Laval", CodePostal = "B2s 7B4", Telephone = "521-232-1562" }, 
                    new Participant { Nom = "Valerty", Prenom = "Laurie", Courriel = "Laurie@Laurie.com", Rue = "4343 DesChamps", Ville = "Montréal", CodePostal = "C2s 7B4", Telephone = "584-212-1452" },
                    new Participant { Nom = "Perto", Prenom = "Sarah", Courriel = "Sarah@Sarah.com", Rue = "2323 Dupré", Ville = "Laval", CodePostal = "D2s 7B4", Telephone = "534-432-1542" },
                    new Participant { Nom = "Sochi", Prenom = "Dominique", Courriel = "Dominique@Dominique.com", Rue = "2222 Derty", Ville = "Montréal", CodePostal = "E2s 7B4", Telephone = "534-442-1416" },
                    new Participant { Nom = "Mert", Prenom = "Benoit", Courriel = "Benoit@Benoit.com", Rue = "6565 maxwell", Ville = "Laval", CodePostal = "L2s 7B4", Telephone = "534-243-1254" },
                    new Participant { Nom = "Locas", Prenom = "Lara", Courriel = "Lara@Lara.com", Rue = "5435 Mertier", Ville = "Montréal", CodePostal = "M2s 7B4", Telephone = "534-452-1436" },
                    new Participant { Nom = "Perty", Prenom = "Mory", Courriel = "Mory@Mory.com", Rue = "6365 Tery", Ville = "Laval", CodePostal = "B2s 7B4", Telephone = "544-213-2254" }
                };

                var OrganismeCommunautaire = new Organisme { Nom = "Centre Communautaire", Rue = "15432 Palate", Ville = "Montréal", CodePostal = "M3B 3V2", Telephone = "514-356-2323", Telecopieur = "55455366" };


                context.CampagneFinancements.AddOrUpdate(
                    p => p.Nom,
                    new CampagneFinancement
                    {
                        Nom = "Campagne Soccer",
                        Organisme = new Organisme { Nom = "Centre Sportif Bennington", Rue = "1332 Montier", Ville = "Québec", CodePostal = "S3B 3V2", Telephone = "514-345-2343", Telecopieur = "5355366" },
                        Participants = new List<Participant>() { participants[0], participants[1], participants[2], participants[3] }
                    },
                    new CampagneFinancement
                    {
                        Nom = "Campagne Cours Math",
                        Organisme = new Organisme { Nom = "École Vallée", Rue = "454 Bernier", Ville = "Montréal", CodePostal = "S3P 1V2", Telephone = "514-545-1143", Telecopieur = "4355366" },
                        Participants = new List<Participant>() { participants[4], participants[5], participants[6] }
                    },
                    new CampagneFinancement
                    {
                        Nom = "Campagne Enfants Soleil",
                        Organisme = new Organisme { Nom = "Entraide Des Amis", Rue = "2555 LaPlace", Ville = "Laval", CodePostal = "P3B 3V2", Telephone = "514-645-3343", Telecopieur = "1335366" },
                        Participants = new List<Participant>() { participants[7], participants[0], participants[1], participants[2] }
                    },
                    new CampagneFinancement
                    {
                        Nom = "Campagne Tableaux Japonais",
                        Organisme = new Organisme { Nom = "Centre de l'Asie", Rue = "5432 Polier", Ville = "Montréal", CodePostal = "S3B 3V2", Telephone = "514-225-2143", Telecopieur = "2355366" },
                        Participants = new List<Participant>() { participants[3], participants[4], participants[5], participants[6], participants[7] }
                    },
                    new CampagneFinancement
                    {
                        Nom = "Campagne Loisirs Été",
                        Organisme = new Organisme { Nom = "Centre Communautaire Lentier", Rue = "161 LaGrange", Ville = "Laval", CodePostal = "I3B 3V2", Telephone = "514-345-7343", Telecopieur = "3355366" },
                        Participants = new List<Participant>() { participants[0], participants[1], participants[2], participants[3], participants[4], participants[5], participants[6] }
                    },
                    new CampagneFinancement
                    {
                        Nom = "Campagne D'antan",
                        Organisme = new Organisme { Nom = "Centre Les Amis", Rue = "4363 Laurier", Ville = "Sorel", CodePostal = "L3B 3V2", Telephone = "514-435-8343", Telecopieur = "4355366" },
                        Participants = participants
                    },
                    new CampagneFinancement
                    {
                        Nom = "Campagne Club Lecture",
                        Organisme = new Organisme { Nom = "Librairie Le Temps", Rue = "4633 Bois", Ville = "Laval", CodePostal = "L9B 3V0", Telephone = "514-444-8343", Telecopieur = "43775366" },
                        Participants = participants
                    },
                    new CampagneFinancement { Nom = "Campagne Club Karate", Organisme = OrganismeCommunautaire, Participants = participants },
                    new CampagneFinancement { Nom = "Campagne Club Badminton", Organisme = OrganismeCommunautaire, Participants = participants }
                );
            }

        }
    }
}

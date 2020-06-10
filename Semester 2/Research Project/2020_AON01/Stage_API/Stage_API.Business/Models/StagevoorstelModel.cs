using Stage_API.Domain.Classes;
using Stage_API.Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stage_API.Business.Models
{
    public class StagevoorstelModel
    {
        //Everyone
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string BedrijfsNaam { get; set; }
        public string Titel { get; set; }
        public string Adres { get; set; }
        public string StagePostcode { get; set; }
        public string Gemeente { get; set; }
        public int StageITMedewerkers { get; set; }
        public string[] AfstudeerrichtingVoorkeur { get; set; }
        public string OpdrachtOmschrijving { get; set; }
        public string[] Omgeving { get; set; }
        public string OmgevingOmschrijving { get; set; }
        public string Randvoorwaarden { get; set; }
        public string Onderzoeksthema { get; set; }
        public string[] Verwachtingen { get; set; }
        public int AantalGewensteStagiairs { get; set; }
        public string GereserveerdeStudenten { get; set; }
        public string Bemerkingen { get; set; }
        public int Periode { get; set; }
        //Reviewer & Bedrijf & Coordinator
        public int StudentenFavorietenCount { get; set; }
        public BeoordelingStatus Status { get; set; }
        public ICollection<Comment> Comments { get; set; }

        //Reviewer & Coordinator
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Reviewer> ReviewersToegewezen { get; set; }
        public ICollection<Reviewer> ReviewersFavorieten { get; set; }


        //Student & Coordinator
        public ICollection<Student> StudentenFavorieten { get; set; }
        public ICollection<Student> StudentenToegewezen { get; set; }


        public StagevoorstelModel()
        {

        }

        public StagevoorstelModel(Stagevoorstel stagevoorstel, string role)
        {
            Id = stagevoorstel.Id;
            Date = stagevoorstel.Date;
            BedrijfsNaam = stagevoorstel.Bedrijf.Naam;
            Titel = stagevoorstel.Titel;
            Adres = stagevoorstel.Adres;
            StagePostcode = stagevoorstel.StagePostcode;
            Gemeente = stagevoorstel.Gemeente;
            StageITMedewerkers = stagevoorstel.StageITMedewerkers;
            AfstudeerrichtingVoorkeur = stagevoorstel.AfstudeerrichtingVoorkeur;
            OpdrachtOmschrijving = stagevoorstel.OpdrachtOmschrijving;
            Omgeving = stagevoorstel.Omgeving;
            OmgevingOmschrijving = stagevoorstel.OmgevingOmschrijving;
            Randvoorwaarden = stagevoorstel.Randvoorwaarden;
            Onderzoeksthema = stagevoorstel.Onderzoeksthema;
            Verwachtingen = stagevoorstel.Verwachtingen;
            AantalGewensteStagiairs = stagevoorstel.AantalGewensteStagiairs;
            GereserveerdeStudenten = stagevoorstel.GereserveerdeStudenten;
            Bemerkingen = stagevoorstel.Bemerkingen;
            Periode = stagevoorstel.Periode;
            Status = stagevoorstel.Status;

            if (role != "student")
            {
                StudentenFavorietenCount = stagevoorstel.StudentenFavorieten.Count;
                Comments = stagevoorstel.Comments;
            }

            if (role == "reviewer" || role == "coordinator")
            {
                Reviews = stagevoorstel.Reviews;
                ReviewersFavorieten = stagevoorstel.ReviewersFavorieten?.Select(r => r.Reviewer).ToList();
                ReviewersToegewezen = stagevoorstel.ReviewersToegewezen?.Select(r => r.Reviewer).ToList();
            }

            if (role != "coordinator") return;
            StudentenToegewezen = stagevoorstel.StudentenToegewezen;
            StudentenFavorieten = stagevoorstel.StudentenFavorieten?.Select(s => s.Student).ToList();
        }

        public Stagevoorstel Combine(Stagevoorstel originalVoorstel)
        {
            return new Stagevoorstel
            {
                Id = Id,
                Titel = Titel,
                Adres = originalVoorstel.Adres,
                Date = originalVoorstel.Date,
                BedrijfId = originalVoorstel.BedrijfId,
                StagePostcode = originalVoorstel.StagePostcode,
                Gemeente = originalVoorstel.Gemeente,
                StageITMedewerkers = originalVoorstel.StageITMedewerkers,
                AfstudeerrichtingVoorkeur = AfstudeerrichtingVoorkeur,
                OpdrachtOmschrijving = OpdrachtOmschrijving,
                Omgeving = Omgeving,
                OmgevingOmschrijving = OmgevingOmschrijving,
                Randvoorwaarden = Randvoorwaarden,
                Onderzoeksthema = Onderzoeksthema,
                Verwachtingen = Verwachtingen,
                AantalGewensteStagiairs = AantalGewensteStagiairs,
                Bemerkingen = Bemerkingen,
                Periode = Periode,
                Status = originalVoorstel.Status
            };
        }

        public Stagevoorstel Make(in int userId)
        {
            return new Stagevoorstel
            {
                Date = DateTime.Now,
                BedrijfId = userId,
                Titel = Titel,
                AfstudeerrichtingVoorkeur = AfstudeerrichtingVoorkeur,
                OpdrachtOmschrijving = OpdrachtOmschrijving,
                Omgeving = Omgeving,
                OmgevingOmschrijving = OmgevingOmschrijving,
                Randvoorwaarden = Randvoorwaarden,
                Onderzoeksthema = Onderzoeksthema,
                Verwachtingen = Verwachtingen,
                AantalGewensteStagiairs = AantalGewensteStagiairs,
                GereserveerdeStudenten = GereserveerdeStudenten,
                Bemerkingen = Bemerkingen,
                Periode = Periode,
                Status = Status
            };
        }
    }
}

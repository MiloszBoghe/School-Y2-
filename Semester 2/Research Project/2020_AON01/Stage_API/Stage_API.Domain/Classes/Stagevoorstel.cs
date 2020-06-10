using Stage_API.Domain.enums;
using Stage_API.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Stage_API.Domain.Classes
{
    public class Stagevoorstel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Bedrijf Bedrijf { get; set; }
        [IgnoreDataMember]
        public int? BedrijfId { get; set; }
        /*HIER IS DE MANY-MANY*/
        public ICollection<ReviewerStagevoorstelFavoriet> ReviewersFavorieten { get; set; }
        public ICollection<ReviewerStagevoorstelToegewezen> ReviewersToegewezen { get; set; }
        public ICollection<StudentStagevoorstelFavoriet> StudentenFavorieten { get; set; }
        public ICollection<Student> StudentenToegewezen { get; set; }
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
        //Bedrijf is gecontacteerd door student(en) en wenst deze opdracht enkel aan deze student(en) aan te bieden.
        public string GereserveerdeStudenten { get; set; }
        public string Bemerkingen { get; set; }
        //Semester 1 (oktober - januari) OF Semester 2 (februari - juni)
        public int Periode { get; set; }
        public BeoordelingStatus Status { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Stage_API.Domain.Classes
{
    public class Bedrijf : User
    {
        //Algemeen
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        //Totaal aantal medewerkers in het bedrijf
        public int AantalMedewerkers { get; set; }
        public int AantalITMedewerkers { get; set; }
        //Aantal personen die de student technisch kunnen begeleiden bij het uitwerken van zijn stageopdracht  
        public int AantalBegeleidendeMedewerkers { get; set; }
        //Contactpersoon
        public Contactpersoon Contactpersoon { get; set; }
        [IgnoreDataMember]
        public int ContactpersoonId { get; set; }
        //Bedrijfspromotor
        public Bedrijfspromotor Bedrijfspromotor { get; set; }
        [IgnoreDataMember]
        public int BedrijfspromotorId { get; set; }
        public ICollection<Stagevoorstel> Stagevoorstellen { get; set; }
    }
}
using Stage_API.Domain.Classes;
using System.ComponentModel.DataAnnotations;

namespace Stage_API.AuthenticationModels
{
    public class RegisterBedrijfModel
    {
        public string Naam { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public int AantalMedewerkers { get; set; }
        public int AantalITMedewerkers { get; set; }
        public int AantalBegeleidendeMedewerkers { get; set; }
        public Contactpersoon Contactpersoon { get; set; }
        [Required]
        public Bedrijfspromotor Bedrijfspromotor { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}

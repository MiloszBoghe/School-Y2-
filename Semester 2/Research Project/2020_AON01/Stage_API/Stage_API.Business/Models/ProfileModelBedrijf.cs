using Stage_API.Domain.Classes;
using System.ComponentModel.DataAnnotations;

namespace Stage_API.Business.Models
{
    public class ProfileModelBedrijf : ProfileModel
    {
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Gemeente { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public int AantalMedewerkers { get; set; }
        [Required]
        public int AantalITMedewerkers { get; set; }
        [Required]
        public int AantalBegeleidendeMedewerkers { get; set; }
        [Required]
        public Contactpersoon Contactpersoon { get; set; }
        [Required]
        public Bedrijfspromotor Bedrijfspromotor { get; set; }


        public ProfileModelBedrijf()
        {

        }
        public ProfileModelBedrijf(Bedrijf entity) : base(entity)
        {
            Adres = entity.Adres;
            Gemeente = entity.Gemeente;
            Postcode = entity.Postcode;
            AantalMedewerkers = entity.AantalMedewerkers;
            AantalITMedewerkers = entity.AantalITMedewerkers;
            AantalBegeleidendeMedewerkers = entity.AantalBegeleidendeMedewerkers;
            Contactpersoon = entity.Contactpersoon;
            Bedrijfspromotor = entity.Bedrijfspromotor;
        }
    }
}

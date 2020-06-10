using System.ComponentModel.DataAnnotations;

namespace Stage_API.Domain.Classes
{
    //Deze persoon zal de stagedocumenten ondertekenen
    public class Contactpersoon
    {
        public int Id { get; set; }
        [Required]
        public string Titel { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Telefoonnummer { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
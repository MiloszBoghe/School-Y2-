using System.ComponentModel.DataAnnotations;

namespace Stage_API.Domain.Classes
{
    public class Bedrijfspromotor
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
        public string Email { get; set; }

    }
}
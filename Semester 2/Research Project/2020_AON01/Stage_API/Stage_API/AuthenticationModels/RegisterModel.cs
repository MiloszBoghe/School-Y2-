using System.ComponentModel.DataAnnotations;

namespace Stage_API.AuthenticationModels
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
    }
}

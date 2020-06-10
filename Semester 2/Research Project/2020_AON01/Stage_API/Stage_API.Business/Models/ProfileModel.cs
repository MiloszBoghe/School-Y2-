using Stage_API.Domain;
using System.ComponentModel.DataAnnotations;

namespace Stage_API.Business.Models
{
    public class ProfileModel
    {
        [Required]
        public int Id { get; set; }
        public string Voornaam { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsBedrijf { get; set; }
        public ProfileModel()
        {

        }
        public ProfileModel(User entity)
        {
            Id = entity.Id;
            Voornaam = entity.Voornaam;
            Naam = entity.Naam;
            Email = entity.Email;
            UserName = entity.Email;
            IsBedrijf = entity.IsBedrijf;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PasswordApp.Web.Models
{
    public class EntryEditViewModel
    {
        [Required]
        public string Password { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }
    }
}
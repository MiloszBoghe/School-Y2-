using System;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood2.Models
{
    public class RegisterModel
    {
        public DateTime DateOfBirth { get; set; }

        [Required] [EmailAddress] 
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

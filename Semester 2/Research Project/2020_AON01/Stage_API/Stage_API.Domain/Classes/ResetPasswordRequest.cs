using System;
using System.ComponentModel.DataAnnotations;


namespace Stage_API.Domain.Classes
{
    public class ResetPasswordRequest
    {
        [Required]
        public Guid PasswordResetToken { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime ResetRequestDateTime { get; set; }

        public bool IsConsumed { get; set; }

    }
}

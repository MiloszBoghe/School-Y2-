using Stage_API.Domain;
using Stage_API.Domain.Classes;

namespace Stage_API.Business.Services.PasswordReset
{
    public interface IPasswordResetService
    {
        public void SendResetToken(User user, ResetPasswordRequest resetPasswordRequest);
    }
}

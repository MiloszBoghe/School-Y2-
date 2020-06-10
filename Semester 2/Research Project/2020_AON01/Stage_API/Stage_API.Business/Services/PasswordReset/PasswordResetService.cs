using Stage_API.Domain;
using Stage_API.Domain.Classes;
using IMailService = Stage_API.Business.Services.Mail.MailService.IMailService;

namespace Stage_API.Business.Services.PasswordReset
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IMailService _mailService;

        public PasswordResetService(IMailService mailService)
        {
            _mailService = mailService;
        }
        public void SendResetToken(User user, ResetPasswordRequest resetPasswordRequest)
        {
            _mailService.SendMail(user, resetPasswordRequest);
        }
    }
}

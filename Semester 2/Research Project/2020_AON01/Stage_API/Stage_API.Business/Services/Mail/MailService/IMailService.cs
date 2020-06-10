using Stage_API.Domain;
using Stage_API.Domain.Classes;
using Stage_API.Domain.enums;

namespace Stage_API.Business.Services.Mail.MailService
{
    public interface IMailService
    {
        public void SendMail(Stagevoorstel stagevoorstel, BeoordelingStatus oldStatus);
        public void SendMail(Review review, BeoordelingStatus oldStatus);
        public void SendMail(User user, ResetPasswordRequest resetPasswordRequest);
    }
}

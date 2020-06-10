using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Stage_API.Business.Services.Mail.Configuration;
using Stage_API.Business.Services.Mail.Mail;
using Stage_API.Domain;
using Stage_API.Domain.Classes;
using Stage_API.Domain.enums;
using System;
using System.Linq;

namespace Stage_API.Business.Services.Mail.MailService
{
    public class MailService : IMailService
    {
        private readonly IMailConfiguration _eMailConfiguration;

        public MailService(IMailConfiguration mailConfiguration)
        {
            _eMailConfiguration = mailConfiguration;
        }

        public void SendMail(Stagevoorstel stagevoorstel, BeoordelingStatus oldStatus)
        {
            if ((oldStatus == stagevoorstel.Status)) return;
            var mail = EmailMessage.FormMail(stagevoorstel);
            Send(mail);
        }
        public void SendMail(Review review, BeoordelingStatus oldStatus)
        {
            if ((oldStatus == review.Status)) return;
            var mail = EmailMessage.FormMail(review);
            Send(mail);
        }

        public void SendMail(User user, ResetPasswordRequest resetPasswordRequest)
        {
            var mail = EmailMessage.FormMail(user, resetPasswordRequest);
            Send(mail);
        }

        private void Send(EmailMessage mail)
        {
            var mailAdres = new MailboxAddress("API", _eMailConfiguration.SmtpUsername);


            var message = new MimeMessage();
            message.To.AddRange(mail.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.Add(mailAdres);
            message.Subject = mail.Subject;

            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = mail.Content
            };
            try
            {
                using (var emailClient = new SmtpClient())  //Belangrijke smtp client van mailkit
                {
                    emailClient.Connect(_eMailConfiguration.SmtpServer, _eMailConfiguration.SmtpPort, true); //self signed ssl op het moment
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    emailClient.Authenticate(_eMailConfiguration.SmtpUsername, _eMailConfiguration.SmtpPassword);
                    emailClient.Send(message);
                    emailClient.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
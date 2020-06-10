using Stage_API.Domain;
using Stage_API.Domain.Classes;
using Stage_API.Domain.enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stage_API.Business.Services.Mail.Mail
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public static EmailMessage FormMail(Stagevoorstel stagevoorstel)
        {
            var mailMessage = new EmailMessage();
            var currentReview = stagevoorstel.Reviews.Last();
            var mailContentBuilder = new StringBuilder();

            mailMessage.ToAddresses.Add(new EmailAddress
            {
                Name = stagevoorstel.Bedrijf.Contactpersoon.Naam,
                Address = "kaanozdemi@gmail.com" // dit moet hard gecodeerd blijven voor testing purposes!!
            });

            if (stagevoorstel.Status == (BeoordelingStatus)2)
            {
                mailMessage.Subject = $"Uw Stagevoorstel {stagevoorstel.Titel} is goed gekeurd";
            }
            else
            {
                mailMessage.Subject = $"Er is Feedback achtergelaten op {stagevoorstel.Titel}";

            }

            mailContentBuilder.Append($"Beste {stagevoorstel.Bedrijf.Contactpersoon.Naam}\n\n" +
                                      $"{currentReview.Reviewer.Voornaam} {currentReview.Reviewer.Naam} heeft feedback achtergelaten op review: {stagevoorstel.Titel}.\n" +
                                      $"Feedback \n{currentReview.Text}\n\n" +
                                      $"Deze Email dient enkel ter herinering\n" +
                                      $"U hoeft niet te antwoorden op deze mail\n" +
                                      $"Met vriendelijke groeten\n\n" +
                                      $"Het PXL StageTeam" +
                                      $"Elfde-Liniestraat 23\r\n3500 HASSELT\r\ntel. + 32 11 77 58 46\r\ngsm + 32 484 35 49 12");

            mailMessage.Content = mailContentBuilder.ToString();

            return mailMessage;
        }
        public static EmailMessage FormMail(Review review)
        {
            var mailMessage = new EmailMessage();
            var extraMessage = "";
            var mailContentBuilder = new StringBuilder();

            mailMessage.ToAddresses.Add(new EmailAddress
            {
                Name = review.Reviewer.Naam,
                Address = "kaanozdemi@gmail.com"
            });

            if (review.Status == (BeoordelingStatus)2)
            {
                mailMessage.Subject = $"Uw Review voor {review.Stagevoorstel.Titel} is goedgekeurd";
                extraMessage = $"goedgekeurd en doorgestuurd naar {review.Stagevoorstel.Bedrijf.Contactpersoon.Voornaam} {review.Stagevoorstel.Bedrijf.Contactpersoon.Naam}";
            }
            else if (review.Status == (BeoordelingStatus)1)
            {
                mailMessage.Subject = $"Er is Feedback achtergelaten op uw review voor {review.Stagevoorstel.Titel}";
                extraMessage = "Is afgekeurd, gelieve deze nog eens te herzien";

            }

            mailContentBuilder.Append($"Beste {review.Reviewer.Naam}\n\n" +
                                      $"Uw Feedback voor {review.Stagevoorstel.Titel} is {extraMessage}\n\n" +
                                      $"Deze Email dient enkel ter herinering\n" +
                                      $"U hoeft niet te antwoorden op deze mail\n\n" +
                                      $"Met vriendelijke groeten\n\n" +
                                      $"Het PXL StageTeam\n\n" +
                                      $"Elfde-Liniestraat 23\r\n3500 HASSELT\r\ntel. + 32 11 77 58 46\r\ngsm + 32 484 35 49 12");

            mailMessage.Content = mailContentBuilder.ToString();

            return mailMessage;
        }

        public static EmailMessage FormMail(User user, ResetPasswordRequest resetPasswordRequest)
        {
            var mailMessage = new EmailMessage();
            var mailContentBuilder = new StringBuilder();
            var url = "http://localhost:4200/resetPassword?token=" + resetPasswordRequest.PasswordResetToken;

            mailMessage.ToAddresses.Add(new EmailAddress
            {
                Name = user.Naam,
                Address = "kaanozdemi@gmail.com"
            });

            mailMessage.Subject = $"Password reset voor PXL stage";

            mailContentBuilder.Append($"Beste {user.Naam}\n\n" +
                                      $"U heeft op {resetPasswordRequest.ResetRequestDateTime.ToString("dd/MM/yyyy")} een wachtwoord reset aangevraagt\n\n" +
                                      $"Voor veiligheidsreden is deze maar 20 min geldig\n\n" +
                                      $"{url}\n" +
                                      $"Als u deze wachtwoord niet heeft aangevraagd mag u deze mail negeren\n\n" +
                                      $"U hoeft niet te antwoorden op deze mail\n\n" +
                                      $"Met vriendelijke groeten\n\n" +
                                      $"Het PXL StageTeam\n\n" +
                                      $"Elfde-Liniestraat 23\r\n3500 HASSELT\r\ntel. + 32 11 77 58 46\r\ngsm + 32 484 35 49 12");

            mailMessage.Content = mailContentBuilder.ToString();
            return mailMessage;
        }
    }
}

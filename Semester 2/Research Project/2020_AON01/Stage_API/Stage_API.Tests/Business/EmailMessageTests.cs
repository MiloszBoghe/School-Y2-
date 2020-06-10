using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Stage_API.Business.Services.Mail.Mail;
using Stage_API.Data;
using Stage_API.Domain.Classes;

namespace Stage_API.Tests.Business
{
    public class EmailMessageTests
    {

        private StageContext _context;
        [SetUp]
        public void SetUp()
        {
            _context = TestHelper.Context;
        }

        [Test]
        [TestCase(1)]
        [TestCase(7)]
        public void FormMail_Stagevoorstel_Returns_mailMessage_Correctly(int stagevoorstelId)
        {
            //Arrange
            var stagevoorstel = _context.Stagevoorstellen
                .Include(s=>s.Reviews).ThenInclude(r=>r.Reviewer)
                .Include(s=>s.Bedrijf.Contactpersoon)
                .First(s=>s.Id==stagevoorstelId);

            //Act 
            var mailMessage = EmailMessage.FormMail(stagevoorstel);

            //Assert
            Assert.NotNull(mailMessage);
            Assert.AreEqual(1,mailMessage.ToAddresses.Count);
            Assert.False(string.IsNullOrEmpty(mailMessage.Content));
            Assert.False(string.IsNullOrEmpty(mailMessage.Subject));
            Assert.False(string.IsNullOrEmpty(mailMessage.ToAddresses.ElementAt(0).Address));
            Assert.False(string.IsNullOrEmpty(mailMessage.ToAddresses.ElementAt(0).Name));
        }

        [Test]
        [TestCase(1)]
        [TestCase(8)]
        public void FormMail_Review_Returns_mailMessage_Correctly(int reviewId)
        {
            //Arrange
            var stagevoorstel = _context.Reviews
                .Include(s => s.Reviewer)
                .Include(s => s.Stagevoorstel.Bedrijf.Contactpersoon)
                .First(s => s.Id == reviewId);

            //Act 
            var mailMessage = EmailMessage.FormMail(stagevoorstel);

            //Assert
            Assert.NotNull(mailMessage);
            Assert.AreEqual(1, mailMessage.ToAddresses.Count);
            Assert.False(string.IsNullOrEmpty(mailMessage.Content));
            Assert.False(string.IsNullOrEmpty(mailMessage.Subject));
            Assert.False(string.IsNullOrEmpty(mailMessage.ToAddresses.ElementAt(0).Address));
            Assert.False(string.IsNullOrEmpty(mailMessage.ToAddresses.ElementAt(0).Name));
        }

        [Test]
        public void FormMail_ResetPassword_Returns_mailMessage_Correctly()
        {
            //Arrange
            var user = _context.Users.Find(7);
            var resetPasswordRequest = new ResetPasswordRequest{PasswordResetToken = Guid.NewGuid(), ResetRequestDateTime = DateTime.Now};
            //Act 
            var mailMessage = EmailMessage.FormMail(user,resetPasswordRequest);

            //Assert
            Assert.NotNull(mailMessage);
            Assert.AreEqual(1, mailMessage.ToAddresses.Count);
            Assert.False(string.IsNullOrEmpty(mailMessage.Content));
            Assert.False(string.IsNullOrEmpty(mailMessage.Subject));
            Assert.False(string.IsNullOrEmpty(mailMessage.ToAddresses.ElementAt(0).Address));
            Assert.False(string.IsNullOrEmpty(mailMessage.ToAddresses.ElementAt(0).Name));
        }
    }
}

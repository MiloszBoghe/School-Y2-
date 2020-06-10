using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Stage_API.Business.Services.Mail.MailService;
using Stage_API.Data;
using Stage_API.Data.Repositories;
using Stage_API.Domain.Classes;
using Stage_API.Domain.enums;
using System;
using System.Linq;

namespace Stage_API.Tests.Repositories
{
    public class ReviewRepoTests
    {
        private readonly StageContext _context = TestHelper.Context;
        private ReviewRepository _reviewRepository;
        private Mock<IMailService> _mailMock;

        [SetUp]
        public void Setup()
        {
            _mailMock = new Mock<IMailService>();
            _reviewRepository = new ReviewRepository(_context, _mailMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.DetachEntries();
        }

        [Test]
        public void GetReviewsByVoorstelId_Returns_CorrectReviews()
        {
            //Arrange
            var reviews = _reviewRepository.GetReviewsByVoorstel(1).ToList();
            foreach (var review in reviews)
            {
                _context.Entry(review).Reference(r => r.Stagevoorstel).Load();
            }

            //Assert
            Assert.NotNull(reviews);
            Assert.That(reviews.Count() == 1);
            Assert.AreEqual(reviews.ElementAt(0).Stagevoorstel.Id, 1);
        }

        [Test]
        public void PatchStatus_UpdatesReviewStatus_Correctly()
        {
            //Arrange
            var review = _context.Reviews.AsNoTracking().FirstOrDefault(r => r.Status == 0);
            _mailMock.Setup(mail => mail.SendMail(review, review.Status));

            //Act
            _reviewRepository.PatchStatus(review.Id, 2);

            //Assert
            var updatedReview = _context.Reviews.AsNoTracking().FirstOrDefault(r => r.Id == review.Id);
            Assert.NotNull(updatedReview);
            Assert.AreNotEqual(updatedReview.Status, review.Status);
            Assert.AreEqual(updatedReview.Status, BeoordelingStatus.Goedgekeurd);
        }

        [Test]
        public void PatchStatus_ReturnsFalse_IfReviewDoesNotExist()
        {
            //Act & Assert
            Assert.False(_reviewRepository.PatchStatus(100, 2));
        }

        [Test]
        public void AddReview_AddsReview_Correctly()
        {
            //Arrange
            const string text = "Goedgekeurd!TestTest";
            var review = new Review
            {
                Date = DateTime.Now,
                ReviewerId = 1,
                StagevoorstelId = 1,
                Status = BeoordelingStatus.Goedgekeurd,
                Text = text
            };

            //Act
            _reviewRepository.Add(review);
            var addedReview = _context.Reviews.Where(r => r.Text == text).Include(r => r.Stagevoorstel).Include(r => r.Reviewer).FirstOrDefault();

            //Assert
            Assert.NotNull(addedReview);

            //Undo
            _context.Reviews.Remove(addedReview);
            _context.SaveChanges();
        }

    }
}
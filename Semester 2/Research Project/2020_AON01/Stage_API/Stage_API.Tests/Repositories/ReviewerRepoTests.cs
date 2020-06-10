using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Stage_API.Data;
using Stage_API.Data.Repositories;
using Stage_API.Domain.Classes;
using Stage_API.Domain.Relations;
using System.Collections.Generic;
using System.Linq;
using Stage_API.Business.Models;

namespace Stage_API.Tests.Repositories
{
    public class ReviewerRepoTests
    {
        private readonly StageContext _context = TestHelper.Context;
        private ReviewerRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new ReviewerRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.DetachEntries();
        }

        #region GetAll
        [Test]
        public void GetAllReviewers_Return_ReviewersCollection()
        {
            var reviewers = _repository.GetAll();
            Assert.AreEqual(typeof(List<Reviewer>), reviewers.GetType());
            Assert.That(reviewers.Count() == 4);
        }

        [Test]
        public void GetAllReviewersIncludingToegewezenVoorstellen_Returns_ReviewersCollectionWithToegewezenVoorstellen()
        {
            var reviewers = _repository.GetAll(r => r.ToegewezenVoorstellen);

            Assert.AreEqual(typeof(List<Reviewer>), reviewers.GetType());
            Assert.That(reviewers.Count() == 4);
            foreach (var reviewer in reviewers)
            {
                Assert.NotNull(reviewer.ToegewezenVoorstellen);
            }
        }

        [Test]
        public void GetAllReviewersIncludingFavorieteVoorstellen_Returns_ReviewersCollectionWithFavorieteVoorstellen()
        {
            var reviewers = _repository.GetAll(r => r.VoorkeurVoorstellen);

            Assert.AreEqual(typeof(List<Reviewer>), reviewers.GetType());
            Assert.That(reviewers.Count() == 4);
            foreach (var reviewer in reviewers)
            {
                Assert.NotNull(reviewer.VoorkeurVoorstellen);
            }
        }

        #endregion

        #region GetById
        [Test]
        public void GetReviewerWithWrongId_10_Returns_Null()
        {
            var reviewer = _repository.GetById(10);
            Assert.Null(reviewer);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void GetReviewerById_Returns_ReviewerWithCorrectId(int id)
        {
            var reviewer = _repository.GetById(id);
            Assert.NotNull(reviewer);
            Assert.AreEqual(typeof(Reviewer), reviewer.GetType());
            Assert.AreEqual(reviewer.Id, id);
        }

        [Test]
        public void GetReviewerWithId2_Returns_CorrectReviewerIncludingToegewezenVoorstellen()
        {
            var reviewer = _repository.GetById(2);
            Assert.NotNull(reviewer);
            Assert.AreEqual(typeof(Reviewer), reviewer.GetType());
            Assert.AreEqual(2, reviewer.Id);
            Assert.NotNull(reviewer.ToegewezenVoorstellen);
            Assert.AreEqual(2, reviewer.ToegewezenVoorstellen.Count);
        }

        [Test]
        public void GetReviewerWithId2_Returns_CorrectReviewerIncludingFavorieteVoorstellen()
        {
            var reviewer = _repository.GetById(2, r => r.VoorkeurVoorstellen);
            Assert.NotNull(reviewer);
            Assert.AreEqual(typeof(Reviewer), reviewer.GetType());
            Assert.AreEqual(2, reviewer.Id);
            Assert.NotNull(reviewer.VoorkeurVoorstellen);
            Assert.AreEqual(1, reviewer.VoorkeurVoorstellen.First().StagevoorstelId);
        }

        #endregion

        #region updateTests

        [Test]
        public void UpdateFavorietenReviewer_ReturnsFalse_IfReviewerDoesNotExist()
        {
            //Act & Assert
            Assert.False(_repository.UpdateFavorieten(100, null));
        }

        [Test]
        public void UpdateFavorietenReviewer_ClearAllFavorites_WorksCorrectly()
        {
            //Arrange
            var reviewer = _context.Reviewers.Where(s => s.Id == 3)
                .Include(s => s.VoorkeurVoorstellen).ThenInclude(s => s.Stagevoorstel.Bedrijf)
                .Include(s => s.VoorkeurVoorstellen).ThenInclude(s => s.Stagevoorstel.StudentenFavorieten)
                .AsNoTracking().FirstOrDefault();
            var reviewerModel = new ReviewerModel(reviewer, "reviewer");


            //Act
            reviewerModel.VoorkeurVoorstellen = new List<StagevoorstelModel>();
            _repository.UpdateFavorieten(3, reviewerModel);

            //Assert
            var updatedReviewer = _context.Reviewers.Where(s => s.Id == 3)
                .Include(s => s.VoorkeurVoorstellen).AsNoTracking().FirstOrDefault();

            Assert.NotNull(updatedReviewer);
            Assert.NotNull(updatedReviewer.VoorkeurVoorstellen);
            Assert.IsEmpty(updatedReviewer.VoorkeurVoorstellen);
        }

        [Test]
        public void UpdateFavorietenReviewer_WorksCorrectly()
        {
            //Arrange
            var reviewer = _context.Reviewers.Where(s => s.Id == 2)
                    .Include(s => s.VoorkeurVoorstellen).ThenInclude(s => s.Stagevoorstel.Bedrijf)
                    .Include(s => s.VoorkeurVoorstellen).ThenInclude(s => s.Stagevoorstel.StudentenFavorieten)
                    .AsNoTracking().FirstOrDefault();
            var reviewerModel = new ReviewerModel(reviewer, "reviewer");

            var count = reviewer.VoorkeurVoorstellen.Count;
            var voorstelToAdd = new StagevoorstelModel(_context.Stagevoorstellen.Include(s => s.Bedrijf).Include(s => s.StudentenFavorieten).AsNoTracking().First(s => s.Id == 2), "reviewer");

            //Act
            reviewerModel.VoorkeurVoorstellen.Add(voorstelToAdd);
            _repository.UpdateFavorieten(2, reviewerModel);

            //Assert
            var updatedReviewer = _context.Reviewers.Where(s => s.Id == 2)
                .Include(s => s.VoorkeurVoorstellen).FirstOrDefault();
            Assert.NotNull(updatedReviewer);
            Assert.NotNull(updatedReviewer.VoorkeurVoorstellen);
            Assert.AreEqual(count + 1, updatedReviewer.VoorkeurVoorstellen.Count);
            //Assert that favorite stagevoorstel added is the correct stagevoorstel.
            Assert.AreEqual(voorstelToAdd.Id, updatedReviewer.VoorkeurVoorstellen.ElementAt(1).StagevoorstelId);
        }

        [Test]
        public void UpdateFavorietenReviewer_Change_WorksCorrectly()
        {
            //Arrange
            var reviewer = _context.Reviewers.Where(s => s.Id == 4)
                .Include(s => s.VoorkeurVoorstellen).ThenInclude(s => s.Stagevoorstel.Bedrijf)
                .Include(s => s.VoorkeurVoorstellen).ThenInclude(s => s.Stagevoorstel.StudentenFavorieten)
                .AsNoTracking().FirstOrDefault();

            var reviewerModel = new ReviewerModel(reviewer, "reviewer");

            var count = reviewer.VoorkeurVoorstellen.Count;
            var voorstelToAdd = new StagevoorstelModel(_context.Stagevoorstellen.Include(s=>s.Bedrijf).Include(s=>s.StudentenFavorieten).AsNoTracking().First(s => s.Id == 4), "reviewer");

            //Act
            reviewerModel.VoorkeurVoorstellen = new List<StagevoorstelModel>() { voorstelToAdd };
            _repository.UpdateFavorieten(4, reviewerModel);

            //Assert
            var updatedReviewer = _context.Reviewers.Where(s => s.Id == 4)
                .Include(s => s.VoorkeurVoorstellen).AsNoTracking().FirstOrDefault();
            Assert.NotNull(updatedReviewer);
            Assert.NotNull(updatedReviewer.VoorkeurVoorstellen);
            Assert.AreEqual(count, updatedReviewer.VoorkeurVoorstellen.Count);
        }
        
        #endregion

    }
}
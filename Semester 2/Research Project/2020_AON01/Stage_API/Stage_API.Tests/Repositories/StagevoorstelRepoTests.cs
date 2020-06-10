using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Stage_API.Business.Services.Mail.MailService;
using Stage_API.Data;
using Stage_API.Data.Repositories;
using Stage_API.Domain.Classes;
using Stage_API.Domain.enums;
using Stage_API.Domain.Relations;
using System.Linq;

namespace Stage_API.Tests.Repositories
{
    public class StagevoorstelRepoTests
    {
        private readonly StageContext _context = TestHelper.Context;
        private StagevoorstelRepository _stagevoorstelRepository;
        private Mock<IMailService> _mailMock;

        [SetUp]
        public void Setup()
        {
            _mailMock = new Mock<IMailService>();
            _stagevoorstelRepository = new StagevoorstelRepository(_context, _mailMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.DetachEntries();
        }

        [Test]
        public void PatchStatus_ReturnsFalseIfVoorstelDoesNotExist()
        {
            Assert.False(_stagevoorstelRepository.PatchStatus(0, 2));
        }

        [Test]
        public void PatchStatus_WorksCorrectly()
        {
            //Arrange & Act
            var stagevoorstel = _context.Stagevoorstellen.AsNoTracking().FirstOrDefault(r => r.Status == 0);
            var works = _stagevoorstelRepository.PatchStatus(stagevoorstel.Id, 2);
            var updatedVoorstel = _context.Stagevoorstellen.AsNoTracking().FirstOrDefault(s => s.Id == stagevoorstel.Id);

            //Assert
            Assert.True(works);
            Assert.NotNull(updatedVoorstel);
            Assert.AreNotEqual(updatedVoorstel.Status, stagevoorstel.Status);
            Assert.AreEqual(updatedVoorstel.Status, BeoordelingStatus.Goedgekeurd);
        }

        [Test]
        public void PatchStatus_SendsMailIfBeoordeeld()
        {
            //Arrange
            var stagevoorstel = _context.Stagevoorstellen.AsNoTracking().FirstOrDefault(r => r.Status == 0);

            //Act
            _stagevoorstelRepository.PatchStatus(stagevoorstel.Id, 2);

            //Assert
            Assert.AreEqual(1, _mailMock.Invocations.Count);
        }

        [Test]
        public void PatchStatus_DoesNotSendsMailIfNotBeoordeeld()
        {
            //Arrange
            var stagevoorstel = _context.Stagevoorstellen.AsNoTracking().FirstOrDefault(r => r.Status == BeoordelingStatus.NietBeoordeeld);

            //Act
            _stagevoorstelRepository.PatchStatus(stagevoorstel.Id, 0);

            //Assert
            Assert.AreEqual(0, _mailMock.Invocations.Count);
        }

        [Test]
        public void AddVoorstel_WorksCorrectly()
        {
            //Arrange
            var newVoorstel = new Stagevoorstel
            {
                BedrijfId = 12,
                Titel = "TestvoorstelTest",
                AfstudeerrichtingVoorkeur = new[] { "applicatieOntwikkeling" },
                OpdrachtOmschrijving = "De stagiair zal werken aan interne software toepassingen.",
                Omgeving = new[] { "dotNet", "web", "mobile" },
                OmgevingOmschrijving = "/",
                Randvoorwaarden = "Rijbewijs",
                Onderzoeksthema = "/",
                Verwachtingen = new[] { "CV" },
                AantalGewensteStagiairs = 2,
                GereserveerdeStudenten = "/",
                Bemerkingen = "/",
                Periode = 2,
                Status = BeoordelingStatus.NietBeoordeeld,
            };

            //Act
            _stagevoorstelRepository.Add(newVoorstel);

            //Assert
            var addedVoorstel = _context.Stagevoorstellen.FirstOrDefault(s => s.Titel == "TestvoorstelTest");
            Assert.True(addedVoorstel != null);

            //Undo
            _context.Stagevoorstellen.Remove(addedVoorstel);
            _context.SaveChanges();
        }

        [Test]
        public void UpdatingProperties_WorksCorrectly()
        {
            //Arrange
            var voorstel = _context.Stagevoorstellen.AsNoTracking().FirstOrDefault(s => s.Id == 7);
            var oldTitel = voorstel.Titel;
            var newTitel = "TestTitelTest";
            voorstel.Titel = newTitel;

            //Act
            var updateWorks = _stagevoorstelRepository.Update(7, voorstel);
            var updatedVoorstel = _stagevoorstelRepository.GetById(7);
            //Assert
            Assert.AreNotEqual(oldTitel, newTitel);
            Assert.True(updateWorks);
            Assert.AreEqual(newTitel, updatedVoorstel.Titel);
        }

        [Test]
        public void AddingReviewersToegewezen_WorksCorrectly()
        {

            //Arrange
            var voorstel = _context.Stagevoorstellen.AsNoTracking()
                .Include(s => s.ReviewersToegewezen)
                .Include(s => s.StudentenToegewezen)
                .FirstOrDefault(s => s.Id == 7);

            var reviewerCount = voorstel.ReviewersToegewezen.Count;
            var relationToAdd = new ReviewerStagevoorstelToegewezen { ReviewerId = 2, StagevoorstelId = 7 };
            voorstel.ReviewersToegewezen.Add(relationToAdd);
            //Act
            var updateWorks = _stagevoorstelRepository.Update(7, voorstel);
            var updatedVoorstel = _stagevoorstelRepository.GetById(7);
            //Assert
            Assert.True(updateWorks);
            Assert.AreEqual(reviewerCount + 1, updatedVoorstel.ReviewersToegewezen.Count);

            //Undo
            _context.ToegewezenStagevoorstellenReviewer.Remove(relationToAdd);
            _context.SaveChanges();
        }

        [Test]
        public void RemovingReviewersToegewezen_WorksCorrectly()
        {
            //Arrange
            var voorstel = _context.Stagevoorstellen.AsNoTracking()
                .Include(s => s.ReviewersToegewezen)
                .Include(s => s.StudentenToegewezen)
                .FirstOrDefault(s => s.Id == 7);

            var reviewerCount = voorstel.ReviewersToegewezen.Count;
            var relationToRemove = voorstel.ReviewersToegewezen.First();
            voorstel.ReviewersToegewezen.Remove(relationToRemove);

            //Act
            var updateWorks = _stagevoorstelRepository.Update(7, voorstel);
            _context.DetachEntries();
            var updatedVoorstel = _stagevoorstelRepository.GetById(7);
            //Assert
            Assert.True(updateWorks);
            Assert.AreEqual(reviewerCount - 1, updatedVoorstel.ReviewersToegewezen.Count);
        }

        [Test]
        public void ChangeReviewersToegewezen_WorksCorrectly()
        {
            //Arrange
            var voorstel = _context.Stagevoorstellen.AsNoTracking()
                .Include(s => s.ReviewersToegewezen)
                .Include(s => s.StudentenToegewezen)
                .First(s => s.Id == 1);

            var reviewerCount = voorstel.ReviewersToegewezen.Count;
            var relationToRemove = voorstel.ReviewersToegewezen.First();
            var relationToAdd = new ReviewerStagevoorstelToegewezen { ReviewerId = 4, StagevoorstelId = 1 };
            voorstel.ReviewersToegewezen.Remove(relationToRemove);
            voorstel.ReviewersToegewezen.Add(relationToAdd);
            //Act
            var updateWorks = _stagevoorstelRepository.Update(1, voorstel);
            _context.DetachEntries();
            var updatedVoorstel = _stagevoorstelRepository.GetById(1);
            //Assert
            Assert.True(updateWorks);
            Assert.AreEqual(reviewerCount, updatedVoorstel.ReviewersToegewezen.Count);
            Assert.AreEqual(relationToAdd, updatedVoorstel.ReviewersToegewezen.First());
        }

        [Test]
        public void RemoveMultipleStagevoorstellen_WorksCorrectly()
        {
            //Arrange
            var voorstellen = new[]
                {new Stagevoorstel {Titel = "TestVoorstel1"}, new Stagevoorstel {Titel = "TestVoorstel2"}};
            _context.Stagevoorstellen.AddRange(voorstellen);
            _context.SaveChanges();
            _context.DetachEntries();

            //Act
            _stagevoorstelRepository.RemoveRange(voorstellen);
            var voorstel1 = _context.Stagevoorstellen.FirstOrDefault(s => s.Titel == "TestVoorstel1");
            var voorstel2 = _context.Stagevoorstellen.FirstOrDefault(s => s.Titel == "TestVoorstel2");
            //Assert
            Assert.Null(voorstel1);
            Assert.Null(voorstel2);
        }

    }
}
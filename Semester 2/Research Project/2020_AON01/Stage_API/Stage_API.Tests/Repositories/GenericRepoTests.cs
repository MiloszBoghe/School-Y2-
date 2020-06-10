using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Stage_API.Data;
using Stage_API.Data.Repositories;
using Stage_API.Domain.Classes;
using System.Linq;

namespace Stage_API.Tests.Repositories
{

    public class GenericRepoTests
    {
        private readonly StageContext _context = TestHelper.Context;
        private BedrijfRepository _bedrijfRepository;
        private ReviewerRepository _reviewerRepository;
        private CommentRepository _commentRepository;
        private UserRepository _userRepository;
        [SetUp]
        public void Setup()
        {
            _bedrijfRepository = new BedrijfRepository(_context);
            _reviewerRepository = new ReviewerRepository(_context);
            _userRepository = new UserRepository(_context);
            _commentRepository = new CommentRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.DetachEntries();
        }

        [Test]
        public void GetAll_Returns_All()
        {
            var bedrijven = _bedrijfRepository.GetAll();
            Assert.AreEqual(bedrijven.Count(), 4);
        }

        [Test]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(7)]
        [TestCase(12)]
        public void GetById_Returns_CorrectId(int id)
        {
            var user = _userRepository.GetById(id);
            Assert.AreEqual(user.Id, id);
        }

        [Test]
        public void Wrong_GetById_ReturnsNull()
        {
            var user = _userRepository.GetById(100);
            Assert.IsNull(user);
        }

        [Test]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(7)]
        [TestCase(11)]
        public void GetProfile(int id)
        {
            var user = _userRepository.GetProfile(id);
            if (id > 10)
            {
                var bedrijf = user as Bedrijf;
                Assert.NotNull(bedrijf);
                Assert.NotNull(bedrijf.Contactpersoon);
            }
            Assert.NotNull(user);
            Assert.AreEqual(user.Id, id);
        }

        [Test]
        public void Find_Works_Correctly()
        {
            const string companyName = "cegeka";
            const string name = "marijke";
            var bedrijf = _bedrijfRepository.Find(b => b.Naam == companyName).FirstOrDefault();
            var reviewer = _reviewerRepository.Find(r => r.Voornaam == name).FirstOrDefault();

            Assert.NotNull(reviewer);
            Assert.NotNull(bedrijf);
            Assert.That(bedrijf.Naam.ToLower() == companyName);
            Assert.That(reviewer.Voornaam.ToLower() == name);
        }


        [Test]
        public void Add_Works_Correctly()
        {
            var reviewer = new Reviewer { Voornaam = "jan" };
            _reviewerRepository.Add(reviewer);
            var newReviewer = _reviewerRepository.Find(r => r.Voornaam == "jan").FirstOrDefault();
            Assert.NotNull(newReviewer);
            Assert.AreEqual(newReviewer.Voornaam, "jan");

            //undo
            _context.Reviewers.Remove(reviewer);
            _context.SaveChanges();
        }

        [Test]
        public void Add_Fails_IfMissingRequiredProperties()
        {
            var bedrijf = new Bedrijf { Naam = "SpaceX" };
            Assert.Throws<DbUpdateException>(() => _bedrijfRepository.Add(bedrijf));
        }

        [Test]
        public void Update_Works_Correctly()
        {
            //Arrange
            const string voornaam = "Ekjiram";
            var reviewer = _context.Reviewers.Find(1);
            reviewer.Voornaam = voornaam;

            //Act
            _reviewerRepository.Update(1, reviewer);
            var updatedReviewer = _context.Reviewers.Find(1);

            //Assert
            Assert.AreEqual(reviewer, updatedReviewer);
            Assert.AreEqual(voornaam, updatedReviewer.Voornaam);

        }

        [Test]
        public void Remove_Works_Correctly()
        {
            var comment = _context.Comments.Find(2);
            _commentRepository.Remove(comment);
            var removedComment = _commentRepository.Find(c => c.Id == 2).FirstOrDefault();

            Assert.IsNull(removedComment);

        }


        [Test]
        public void Remove_Works_Correctly_ForBedrijf()
        {
            var promotor = new Bedrijfspromotor
            { Email = "test#mail.com", Naam = "lol", Titel = "dhr", Telefoonnummer = "51654165", Voornaam = "john" };
            var contactpersoon = new Contactpersoon
            { Email = "test2@mail.com", Titel = "dhr", Naam = "Jan", Voornaam = "Jan", Telefoonnummer = "464654" };
            var bedrijf = new Bedrijf { Bedrijfspromotor = promotor, Contactpersoon = contactpersoon };
            var stagevoorstel = new Stagevoorstel {Bedrijf = bedrijf, Titel = "loool"};
            _context.Stagevoorstellen.Add(stagevoorstel);
            _context.SaveChanges();
            _bedrijfRepository.Remove(bedrijf);

            var removedBedrijf = _bedrijfRepository.Find(b => b.Id == bedrijf.Id).FirstOrDefault();
            Assert.IsNull(removedBedrijf);
        }

        [Test]
        public void Includes_Work_Correctly()
        {
            var bedrijven = _bedrijfRepository.GetAll(b => b.Contactpersoon).ToList();
            var bedrijf = _bedrijfRepository.GetById(11, b => b.Contactpersoon);

            Assert.NotNull(bedrijf.Contactpersoon);
            foreach (var b in bedrijven)
            {
                Assert.NotNull(b.Contactpersoon);
            }
        }
    }
}

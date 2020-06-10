using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Stage_API.Business.Models;
using Stage_API.Data;
using Stage_API.Data.Repositories;
using System.Linq;

namespace Stage_API.Tests.Repositories
{
    public class UserRepoTests
    {
        private readonly StageContext _context = TestHelper.Context;
        private UserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            _userRepository = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.DetachEntries();
        }

        [Test]
        public void UpdateUser_ReturnsFalse_IfUserDoesNotExist()
        {
            //Act & Assert
            Assert.False(_userRepository.UpdateUser(100, null));
        }

        [Test]
        public void UpdateBedrijf_ReturnsFalse_IfUserDoesNotExist()
        {
            //Act & Assert
            Assert.False(_userRepository.UpdateBedrijf(100, null));
        }

        [Test]
        public void UpdateUser_WorksCorrectly()
        {
            //Arrange
            var user = new ProfileModel(_context.Users.AsNoTracking().First(u => u.Id == 7));
            var originalNaam = user.Naam;
            const string newNaam = "Flintstone";
            user.Naam = newNaam;
            //Act & Assert
            Assert.True(_userRepository.UpdateUser(7, user));
            Assert.AreNotEqual(originalNaam, newNaam);
            var newUser = _userRepository.GetById(7);
            Assert.AreEqual(newNaam, newUser.Naam);
        }

        [Test]
        public void UpdateBedrijf_WorksCorrectly()
        {
            //Arrange
            var bedrijf = new ProfileModelBedrijf(_context.Bedrijven.AsNoTracking().First(u => u.Id == 11));
            var originalNaam = bedrijf.Naam;
            const string newNaam = "Haribo";
            bedrijf.Naam = newNaam;
            //Act & Assert
            Assert.True(_userRepository.UpdateBedrijf(11, bedrijf));
            Assert.AreNotEqual(originalNaam, newNaam);
            var newBedrijf = _userRepository.GetById(11);
            Assert.AreEqual(newNaam, newBedrijf.Naam);
        }



        [Test]
        public void PatchEmailConfirmed_ReturnsFalse_IfUserDoesNotExist()
        {
            //Act & Assert
            Assert.False(_userRepository.PatchEmailConfirmed(100, false));
        }

        [Test]
        public void PatchEmailConfirmed_WorksCorrectly()
        {
            //Arrange, Act and Assert:
            var setFalseWorks = _userRepository.PatchEmailConfirmed(11, false);
            Assert.True(setFalseWorks);
            var user = _context.Users.AsNoTracking().First(u => u.Id == 11);
            Assert.False(user.EmailConfirmed);

            var setTrueWorks = _userRepository.PatchEmailConfirmed(11, true);
            Assert.True(setTrueWorks);
            user = _context.Users.AsNoTracking().First(u => u.Id == 11);
            Assert.True(user.EmailConfirmed);
        }



    }
}

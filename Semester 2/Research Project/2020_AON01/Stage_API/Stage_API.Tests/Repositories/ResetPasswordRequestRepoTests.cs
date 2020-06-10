using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Stage_API.Business.Services.PasswordReset;
using Stage_API.Data;
using Stage_API.Data.Repositories;
using Stage_API.Domain;
using Stage_API.Domain.Classes;

namespace Stage_API.Tests.Repositories
{
    public class ResetPasswordRequestRepoTests
    {
        private readonly StageContext _context = TestHelper.Context;
        private ResetPasswordRequestRepository _passwordRequestRepository;
        private Mock<IPasswordResetService> _resetMock;
        [SetUp]
        public void Setup()
        {
            _resetMock = new Mock<IPasswordResetService>();
            _passwordRequestRepository = new ResetPasswordRequestRepository(_context, _resetMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.DetachEntries();
        }

        [Test]
        public void Add()
        {
            //Arrange
            var user = new User{Email = "Test@mail.com"};
            
            //Act
            _passwordRequestRepository.Add(user);
            var request = _context.ResetPasswordRequests.ToList().Last();
            //Assert
            Assert.AreEqual(1, _resetMock.Invocations.Count);
            Assert.AreEqual(request.Email,user.Email);
        }


    }
}

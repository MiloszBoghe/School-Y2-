using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Stage_API.Business.Authorization;
using Stage_API.Business.Interfaces;
using Stage_API.Controllers;
using Stage_API.Data;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;
using System.Linq;
using Stage_API.Business.Models;

namespace Stage_API.Tests.Controllers
{
    public class ReviewersControllerTests
    {
        private Mock<IReviewerRepository> _reviewerRepoMock;
        private Mock<IRequestHelper> _helperMock;
        private ReviewersController _reviewersController;
        private readonly StageContext _context = TestHelper.Context;

        [SetUp]
        public void SetUp()
        {
            _reviewerRepoMock = new Mock<IReviewerRepository>();
            _helperMock = new Mock<IRequestHelper>();
            _reviewersController = new ReviewersController(_reviewerRepoMock.Object, _helperMock.Object);
        }

        [Test]
        public void GetReviewers_ReturnsOk_If_Coordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _reviewerRepoMock.Setup(repository => repository.GetAll());

            //Act
            var result = _reviewersController.GetReviewers();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _reviewerRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetReviewers_ReturnsUnAuthorized_If_NotCoordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            //Act
            var result = _reviewersController.GetReviewers();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _reviewerRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void GetReviewerById_ReturnsOk_If_Self()
        {
            //Arrange
            var reviewer = _context.Reviewers.AsNoTracking().FirstOrDefault(r => r.Id == 2);
            var user = new UserObject { IsCoordinator = false, Id = 2 };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _reviewerRepoMock.Setup(repository => repository.GetById(2)).Returns(reviewer);

            //Act
            var result = _reviewersController.GetReviewer(2);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _reviewerRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetReviewerById_ReturnsOk_If_Coordinator()
        {
            //Arrange
            var reviewer = _context.Reviewers.AsNoTracking().FirstOrDefault(r => r.Id == 2);
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _reviewerRepoMock.Setup(repository => repository.GetById(2)).Returns(reviewer);

            //Act
            var result = _reviewersController.GetReviewer(2);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _reviewerRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetReviewerById_ReturnsNotFound_If_ReviewerIsNull()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _reviewerRepoMock.Setup(repository => repository.GetById(99)).Returns((Reviewer)null);

            //Act
            var result = _reviewersController.GetReviewer(99);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _reviewerRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        [TestCase(2, 3, false)]
        [TestCase(3, 4, false)]
        public void GetReviewerById_ReturnsUnAuthorized_If_NotSelfOrCoordinator(int userId, int reviewerId, bool isCoordinator)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = isCoordinator, Id = userId };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            //Act
            var result = _reviewersController.GetReviewer(reviewerId);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _reviewerRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }


        [Test]
        [TestCase(2, 2, false)]
        [TestCase(3, 3, false)]
        public void PutReviewer_ReturnsNoContent_If_EverythingOk_AsReviewer(int reviewerId, int id, bool isCoordinator)
        {
            //Arrange
            var reviewer = new ReviewerModel { Id = reviewerId };
            var user = new UserObject { IsCoordinator = isCoordinator, Id = reviewerId };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _reviewerRepoMock.Setup(repository => repository.UpdateFavorieten(id, reviewer)).Returns(true);

            //Act
            var result = _reviewersController.PutReviewer(id, reviewer);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _reviewerRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }


        [Test]
        [TestCase(4, 3)]
        [TestCase(3, 2)]
        [TestCase(2, 1)]
        public void PutReviewer_ReturnsUnAuthorized_If_NotSelf(int reviewerId, int id)
        {
            //Arrange
            var reviewer = new ReviewerModel { Id = reviewerId };
            var user = new UserObject { Id = id };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            //Act
            var result = _reviewersController.PutReviewer(reviewerId, reviewer);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _reviewerRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }
        
    }
}

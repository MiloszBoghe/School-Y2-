using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Stage_API.Business.Authorization;
using Stage_API.Business.Interfaces;
using Stage_API.Controllers;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;
using System.Collections.Generic;

namespace Stage_API.Tests.Controllers
{
    public class ReviewsControllerTests
    {
        private Mock<IReviewRepository> _reviewRepoMock;
        private Mock<IRequestHelper> _helperMock;
        private ReviewsController _reviewsController;

        [SetUp]
        public void SetUp()
        {
            _reviewRepoMock = new Mock<IReviewRepository>();
            _helperMock = new Mock<IRequestHelper>();
            _reviewsController = new ReviewsController(_reviewRepoMock.Object, _helperMock.Object);
        }


        [Test]
        [TestCase(true, "")]
        [TestCase(false, "reviewer")]
        public void GetReviewsByVoorstel_ReturnsOk_If_EverythingOk(bool isCoordinator, string role)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = isCoordinator, Role = role };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _reviewRepoMock.Setup(repository => repository.GetReviewsByVoorstel(1)).Returns(new List<Review>());

            //Act
            var result = _reviewsController.GetReviewsByVoorstel(1);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _reviewRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        [TestCase(false, "bedrijf")]
        [TestCase(false, "student")]
        [TestCase(false, "")]
        public void GetReviewsByVoorstel_ReturnsUnAuthorized_If_NotReviewerOrCoordinator(bool isCoordinator, string role)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = isCoordinator, Role = role };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _reviewsController.GetReviewsByVoorstel(1);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _reviewRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void GetReviewsByVoorstel_ReturnsNotFound_If_StagevoorstelDoesNotExist()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _reviewRepoMock.Setup(repository => repository.GetReviewsByVoorstel(10000)).Returns((List<Review>)null);

            //Act
            var result = _reviewsController.GetReviewsByVoorstel(10000);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _reviewRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        [TestCase(false, "bedrijf")]
        [TestCase(false, "student")]
        [TestCase(false, "")]
        public void PatchReview_ReturnsUnAuthorized_If_NotCoordinatorOrReviewer(bool isCoordinator, string role)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = isCoordinator, Role = role };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _reviewsController.PatchReview(1, 2);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _reviewRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        [TestCase(true, "")]
        [TestCase(false, "reviewer")]
        public void PatchReview_ReturnsNoContent_If_EverythingOk(bool isCoordinator, string role)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = isCoordinator, Role = role };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _reviewsController.PatchReview(1, 2);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _reviewRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        [TestCase("bedrijf")]
        [TestCase("student")]
        [TestCase("")]
        public void PostReview_ReturnsUnAuthorized_If_NotReviewerOrCoordinator(string role)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false, Role = role };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _reviewsController.PostReview(null);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _reviewRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        [TestCase(false, "reviewer")]
        [TestCase(true, "")]
        public void PostReview_ReturnsOk_If_ReviewerOrCoordinator(bool isCoordinator, string role)
        {
            //Arrange
            var review = new Review();
            var user = new UserObject { IsCoordinator = isCoordinator, Role = role };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _reviewRepoMock.Setup(repository => repository.Add(review));
            //Act
            var result = _reviewsController.PostReview(review);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _reviewRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public void DeleteReview_ReturnsOk_If_EverythingOk()
        {
            //Arrange
            var review = new Review { Id = 1 };
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _reviewRepoMock.Setup(repository => repository.GetById(1)).Returns(review);
            _reviewRepoMock.Setup(repository => repository.Remove(review));
            //Act
            var result = _reviewsController.DeleteReview(1);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(2, _reviewRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void DeleteReview_ReturnsUnAuthorized_If_NotCoordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _reviewsController.DeleteReview(1);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _reviewRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void DeleteReview_ReturnsNotFound_If_ReviewDoesNotExist()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _reviewRepoMock.Setup(repository => repository.GetById(100));

            //Act
            var result = _reviewsController.DeleteReview(100);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _reviewRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }




    }
}

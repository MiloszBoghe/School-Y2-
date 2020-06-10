using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Stage_API.Business.Authorization;
using Stage_API.Business.Interfaces;
using Stage_API.Controllers;
using Stage_API.Data;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;

namespace Stage_API.Tests.Controllers
{
    public class CommentsControllerTests
    {
        private Mock<ICommentRepository> _commentsRepoMock;
        private Mock<IRequestHelper> _helperMock;
        private CommentsController _commentsController;
        private readonly StageContext _context = TestHelper.Context;

        [SetUp]
        public void SetUp()
        {
            _commentsRepoMock = new Mock<ICommentRepository>();
            _helperMock = new Mock<IRequestHelper>();
            _commentsController = new CommentsController(_commentsRepoMock.Object, _helperMock.Object);
        }

        [Test]
        public void PostComment_ReturnsNoContent_If_CommentIsValid_And_UserIsAuthorized()
        {
            //Arrange
            var user = new UserObject { Role = "coordinator" };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _commentsController.PostComment(new Comment { Text = "lol" });

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _commentsRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void PostComment_ReturnsUnAuthorized_If_Student()
        {
            //Arrange
            var user = new UserObject { Role = "student" };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _commentsController.PostComment(new Comment { Text = "lol" });

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _commentsRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

    }
}

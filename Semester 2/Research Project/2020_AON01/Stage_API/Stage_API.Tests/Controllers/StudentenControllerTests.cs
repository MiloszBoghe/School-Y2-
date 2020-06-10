using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Stage_API.Business.Authorization;
using Stage_API.Business.Interfaces;
using Stage_API.Business.Models;
using Stage_API.Controllers;
using Stage_API.Data;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;
using System.Collections.Generic;
using System.Linq;

namespace Stage_API.Tests.Controllers
{
    public class StudentenControllerTests
    {
        private Mock<IStudentRepository> _studentRepoMock;
        private Mock<IRequestHelper> _helperMock;
        private StudentenController _studentenController;
        private readonly StageContext _context = TestHelper.Context;

        [SetUp]
        public void SetUp()
        {
            _studentRepoMock = new Mock<IStudentRepository>();
            _helperMock = new Mock<IRequestHelper>();
            _studentenController = new StudentenController(_studentRepoMock.Object, _helperMock.Object);
        }

        [Test]
        public void GetStudenten_ReturnsOk_If_EverythingOk()
        {
            //Arrange 
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _studentRepoMock.Setup(repository => repository.GetAll()).Returns(new List<Student>());
            //Act
            var result = _studentenController.GetStudenten();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _studentRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetStudenten_ReturnsUnAuthorized_If_NotCoordinator()
        {
            //Arrange 
            var user = new UserObject { IsCoordinator = false };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _studentenController.GetStudenten();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _studentRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        [TestCase(1, 5, true)]
        [TestCase(5, 5, false)]
        public void GetStudent_ReturnsOk_If_CoordinatorOrSelf(int userId, int studentId, bool isCoordinator)
        {
            //Arrange
            var student = _context.Studenten.Include(s => s.FavorieteOpdrachten)
                .ThenInclude(ssf => ssf.Stagevoorstel).ThenInclude(s => s.Bedrijf)
                .Include(s => s.ToegewezenStageOpdracht).ThenInclude(s => s.Bedrijf)
                .FirstOrDefault(s => s.Id == studentId);
            var user = new UserObject { IsCoordinator = isCoordinator, Id = userId };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _studentRepoMock.Setup(repository => repository.GetById(studentId)).Returns(student);

            //Act
            var result = _studentenController.GetStudent(studentId);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _studentRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetStudent_ReturnsUnAuthorized_If_NotCoordinatorOrSelf()
        {
            //Arrange 
            var user = new UserObject { IsCoordinator = false };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _studentenController.GetStudent(8);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _studentRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void GetStudent_ReturnsNotFound_If_StudentDoesNotExist()
        {
            //Arrange 
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _studentRepoMock.Setup(repository => repository.GetById(99));

            //Act
            var result = _studentenController.GetStudent(99);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _studentRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        [TestCase(6, 7)]
        [TestCase(7, 8)]
        public void PutStudent_ReturnsBadRequest_If_IdsDoNotMatch(int id, int studentId)
        {
            //Arrange
            var student = new StudentModel { Id = studentId };
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _studentenController.PutStudent(id, student);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _studentRepoMock.Invocations.Count);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        [TestCase(true, 1, 99)]
        [TestCase(false, 98, 98)]
        public void PutStudent_ReturnsNotFound_If_StudentDoesNotExist(bool isCoordinator, int userId, int studentId)
        {
            //Arrange
            var student = new StudentModel { Id = studentId };
            var user = new UserObject { IsCoordinator = isCoordinator, Id = userId };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user); if (isCoordinator)
            {
                _studentRepoMock.Setup(repository => repository.UpdateToegewezen(studentId, student)).Returns(false);
            }
            else
            {
                _studentRepoMock.Setup(repository => repository.UpdateFavorieten(studentId, student)).Returns(false);
            }

            //Act
            var result = _studentenController.PutStudent(studentId, student);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _studentRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void PutStudent_ReturnsUnAuthorized_If_NotCoordinatorOrSelf()
        {
            //Arrange
            var student = new StudentModel { Id = 8 };
            var user = new UserObject { IsCoordinator = false, Id = 7 };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _studentenController.PutStudent(8, student);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _studentRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }


        [Test]
        [HttpPut("{id}")]
        public void PutStudent_ReturnsNoContent_If_EverythingOk()
        {
            //Arrange
            var student = new StudentModel { Id = 7 };
            var user = new UserObject { IsCoordinator = false, Id = 7 };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _studentRepoMock.Setup(repository => repository.UpdateFavorieten(7, student)).Returns(true);

            //Act
            var result = _studentenController.PutStudent(7, student);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _studentRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

    }
}

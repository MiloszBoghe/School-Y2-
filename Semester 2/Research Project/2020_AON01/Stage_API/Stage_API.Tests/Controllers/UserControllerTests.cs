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
using Stage_API.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Stage_API.Tests.Controllers
{
    public class UserControllerTests
    {
        private Mock<IUserRepository> _userRepoMock;
        private Mock<IRequestHelper> _helperMock;
        private UsersController _usersController;
        private readonly StageContext _context = TestHelper.Context;

        [SetUp]
        public void SetUp()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _helperMock = new Mock<IRequestHelper>();
            _usersController = new UsersController(_userRepoMock.Object, _helperMock.Object);
        }

        #region GetAll
        [Test]
        public void GetAll_ReturnsUnAuthorized_If_NotCoordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _usersController.GetAllUsers();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void GetAll_ReturnsOk_If_Coordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _userRepoMock.Setup(repository => repository.GetAll()).Returns(new List<User>());
            //Act
            var result = _usersController.GetAllUsers();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);

        }
        #endregion

        #region GetUserProfile
        [Test]
        public void GetUserProfile_ReturnsUnAuthorized_If_NotCoordinatorOrSelf()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false, Id = 9 };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _usersController.GetUserProfile(4);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void GetAll_ReturnsNotFound_If_UserDoesNotExist()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            //Act
            var result = _usersController.GetUserProfile(99);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }

        [Test]
        [TestCase(11, "bedrijf")]
        [TestCase(5, "student")]
        public void GetAll_ReturnsOk_If_Bedrijf_And_EverythingOk(int id, string role)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true, Role = role };
            var profile = role == "bedrijf" ?
                _context.Bedrijven.Include(b => b.Contactpersoon)
                .Include(b => b.Bedrijfspromotor)
                .First(b => b.Id == id)
                :
                (User)_context.Studenten.First(s => s.Id == id);

            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _userRepoMock.Setup(repository => repository.GetProfile(id)).Returns(profile);
            //Act
            var result = _usersController.GetUserProfile(id);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion

        #region UpdateUserProfile

        [Test]
        public void UpdateUserProfile_ReturnsBadRequest_If_voornaamEmpty()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _usersController.UpdateUserProfile(2, new ProfileModel());

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        [TestCase(5, 3)]
        [TestCase(2, 3)]
        public void UpdateUserProfile_ReturnsUnAuthorized_If_NotCoordinatorOrSelf(int userId, int id)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false, Id = userId };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _usersController.UpdateUserProfile(id, new ProfileModel { Voornaam = "test" });

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }


        [Test]
        public void UpdateUserProfile_ReturnsNotFound_If_UserDoesNotExist()
        {
            //Arrange
            var profile = new ProfileModel { Voornaam = "test" };
            var user = new UserObject { IsCoordinator = true };
            _userRepoMock.Setup(repository => repository.UpdateUser(2, profile)).Returns(false);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _usersController.UpdateUserProfile(2, profile);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        [TestCase(true, 5, 3)]
        [TestCase(false, 5, 5)]
        public void UpdateUserProfile_ReturnsNoContent_If_EverythingOk(bool isCoordinator, int userId, int id)
        {
            //Arrange
            var profile = new ProfileModel { Voornaam = "test" };
            var user = new UserObject { IsCoordinator = isCoordinator, Id = userId };
            _userRepoMock.Setup(repository => repository.UpdateUser(id, profile)).Returns(true);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            //Act
            var result = _usersController.UpdateUserProfile(id, profile);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }
        #endregion

        #region UpdateBedrijfProfile
        [Test]
        [TestCase(5, 11)]
        [TestCase(11, 12)]
        public void UpdateBedrijfProfile_ReturnsUnAuthorized_If_NotCoordinatorOrSelf(int userId, int id)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false, Id = userId };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _usersController.UpdateBedrijfProfile(id, new ProfileModelBedrijf());

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }


        [Test]
        public void UpdateBedrijfProfile_ReturnsNotFound_If_UserDoesNotExist()
        {
            //Arrange
            var profile = new ProfileModelBedrijf();
            var user = new UserObject { IsCoordinator = true };
            _userRepoMock.Setup(repository => repository.UpdateBedrijf(2, profile)).Returns(false);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _usersController.UpdateBedrijfProfile(2, profile);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        [TestCase(true, 1, 12)]
        [TestCase(false, 11, 11)]
        public void UpdateBedrijfProfile_ReturnsNoContent_If_EverythingOk(bool isCoordinator, int userId, int id)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = isCoordinator, Id = userId };
            var profile = new ProfileModelBedrijf();
            _userRepoMock.Setup(repository => repository.UpdateBedrijf(id, profile)).Returns(true);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            //Act
            var result = _usersController.UpdateBedrijfProfile(id, profile);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }


        #endregion

        #region GetDeactivatedUsers
        [Test]
        public void GetDeactivatedUsers_ReturnsUnAuthorized_If_NotCoordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _usersController.GetDeactivatedUsers();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void GetDeactivatedUsers_ReturnsOk_If_Coordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true, Id = 1 };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _userRepoMock.Setup(repository => repository.Find(u => !u.EmailConfirmed && u.Id != 1)).Returns(new List<User>());
            //Act
            var result = _usersController.GetDeactivatedUsers();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);

        }
        #endregion

        #region GetActivatedUsers
        [Test]
        public void GetActivatedUsers_ReturnsUnAuthorized_If_NotCoordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false, Id = 1 };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _usersController.GetActivatedUsers();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void GetActivatedUsers_ReturnsOk_If_Coordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _userRepoMock.Setup(repository => repository.Find(u => u.EmailConfirmed && u.Id != 1)).Returns(new List<User>());
            //Act
            var result = _usersController.GetActivatedUsers();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);

        }
        #endregion

        #region MyRegion
        [Test]
        public void PatchUsers_ReturnsUnAuthorized_If_NotCoordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _usersController.PatchUsers(10, false);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void PatchUsers_ReturnsNotFound_If_UserWithGivenIdDoesNotExist()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _userRepoMock.Setup(repository => repository.PatchEmailConfirmed(100, false)).Returns(false);
            //Act
            var result = _usersController.PatchUsers(100, false);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void PatchUsers_ReturnsNoContent_If_EverythingOk()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _userRepoMock.Setup(repository => repository.PatchEmailConfirmed(11, false)).Returns(true);
            //Act
            var result = _usersController.PatchUsers(11, false);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _userRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        #endregion

    }
}

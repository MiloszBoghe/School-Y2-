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
using System.Linq;

namespace Stage_API.Tests.Controllers
{
    public class StagevoorstellenControllerTests
    {
        private Mock<IStagevoorstelRepository> _stagevoorstelRepoMock;
        private Mock<IRequestHelper> _helperMock;
        private StagevoorstellenController _stagevoorstellenController;
        private readonly StageContext _context = TestHelper.Context;

        [SetUp]
        public void SetUp()
        {
            _stagevoorstelRepoMock = new Mock<IStagevoorstelRepository>();
            _helperMock = new Mock<IRequestHelper>();
            _stagevoorstellenController = new StagevoorstellenController(_stagevoorstelRepoMock.Object, _helperMock.Object);
        }

        #region GetStagevoorstellen

        [Test]
        [TestCase("reviewer")]
        [TestCase("bedrijf")]
        public void GetStagevoorstellen_ReturnsUnAuthorized_If_NotStudentOrCoordinator(string role)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false, Role = role };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _stagevoorstellenController.GetStagevoorstellen();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }
        [Test]
        [TestCase("student")]
        [TestCase("coordinator")]
        public void GetStagevoorstellen_ReturnsOk_If_StudentOrCoordinator(string role)
        {
            //Arrange
            var voorstellen = _context.Stagevoorstellen
                .Include(s => s.Bedrijf)
                .Include(s => s.StudentenFavorieten);

            _helperMock.Setup(helper => helper.GetUser(null)).Returns(new UserObject { IsCoordinator = false, Role = role });
            _stagevoorstelRepoMock.Setup(repository => repository.GetAll()).Returns(voorstellen);

            //Act
            var result = _stagevoorstellenController.GetStagevoorstellen();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        #endregion

        #region GetStagevoorstel

        [Test]
        [TestCase("reviewer")]
        [TestCase("student")]
        [TestCase("coordinator")]
        [TestCase("bedrijf")]
        public void GetStagevoorstel_ReturnsNotFound_If_VoorstelDoesNotExist(string role)
        {
            var user = new UserObject { Role = role };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(200));

            //Act
            var result = _stagevoorstellenController.GetStagevoorstel(200);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void GetStagevoorstel_ReturnsUnAuthorized_IfStudentAndNotGoedgekeurd()
        {
            var user = new UserObject { Role = "student", Id = 7 };
            var voorstel = _context.Stagevoorstellen
                .Include(s => s.Bedrijf)
                .Include(s => s.StudentenFavorieten)
                .Include(s => s.ReviewersToegewezen)
                .First(s => s.Id == 7);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(7)).Returns(voorstel);


            //Act
            var result = _stagevoorstellenController.GetStagevoorstel(7);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void GetStagevoorstel_ReturnsUnAuthorized_IfBedrijfAndNotOwnVoorstel()
        {
            var user = new UserObject { Role = "bedrijf", Id = 11 };
            var voorstel = _context.Stagevoorstellen
                .Include(s => s.Bedrijf)
                .Include(s => s.StudentenFavorieten)
                .Include(s => s.ReviewersToegewezen)
                .First(s => s.Id == 8);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(8)).Returns(voorstel);


            //Act
            var result = _stagevoorstellenController.GetStagevoorstel(8);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void GetStagevoorstel_ReturnsUnAuthorized_IfReviewerAndNotToegewezen()
        {
            var user = new UserObject { Role = "reviewer", Id = 3 };
            var voorstel = _context.Stagevoorstellen
                .Include(s => s.Bedrijf)
                .Include(s => s.StudentenFavorieten)
                .Include(s => s.ReviewersToegewezen)
                .First(s => s.Id == 7);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(7)).Returns(voorstel);


            //Act
            var result = _stagevoorstellenController.GetStagevoorstel(7);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }


        [Test]
        [TestCase("reviewer", 3, 4)]
        [TestCase("student", 7, 3)]
        [TestCase("coordinator", 1, 5)]
        [TestCase("bedrijf", 11, 1)]
        public void GetStagevoorstel_ReturnsOk_IfAuthorized(string role, int userId, int stagevoorstelId)
        {
            var user = new UserObject { Role = role, Id = userId };
            var voorstel = _context.Stagevoorstellen
                .Include(s => s.Bedrijf)
                .Include(s => s.StudentenFavorieten)
                .Include(s => s.ReviewersToegewezen)
                .First(s => s.Id == stagevoorstelId);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(stagevoorstelId)).Returns(voorstel);


            //Act
            var result = _stagevoorstellenController.GetStagevoorstel(stagevoorstelId);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        #endregion

        #region GetStagevoorstellenBedrijf
        [Test]
        [TestCase("reviewer")]
        [TestCase("student")]
        [TestCase("bedrijf")]
        public void GetStagevoorstellenBedrijf_ReturnsUnAuthorized_If_NotCoordinatorOrBedrijfWithCorrectId(string role)
        {
            var user = new UserObject { Role = role, Id = 11 };

            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _stagevoorstellenController.GetStagevoorstellenBedrijf(12);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        [TestCase(1, "coordinator")]
        [TestCase(11, "bedrijf")]
        public void GetStagevoorstellenBedrijf_ReturnsOk_If_CoordinatorOrBedrijfWithCorrectId(int userId, string role)
        {
            var voorstellen = _context.Stagevoorstellen.AsNoTracking().Where(s => s.BedrijfId == 11)
                .Include(s => s.Bedrijf)
                .Include(s => s.Reviews)
                .Include(s => s.StudentenFavorieten);
            var user = new UserObject { Role = role, Id = userId };

            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.Find(s => s.Bedrijf.Id == 11, s => s.Bedrijf, s => s.Reviews, s => s.StudentenFavorieten))
                .Returns(voorstellen);

            //Act
            var result = _stagevoorstellenController.GetStagevoorstellenBedrijf(11);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        #endregion

        #region PutStagevoorstel

        [Test]
        public void PutStagevoorstel_ReturnsBadRequest_If_IdsDoNotMatch()
        {
            //Arrange
            const string role = "bedrijf";
            var voorstel = new StagevoorstelModel(_context.Stagevoorstellen.AsNoTracking()
                .Include(s => s.Bedrijf)
                .Include(s => s.StudentenFavorieten)
                .First(s => s.Id == 5), role);
            var user = new UserObject { Id = 11, Role = role };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _stagevoorstellenController.PutStagevoorstel(1, voorstel);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void PutStagevoorstel_ReturnsNotFound_If_VoorstelDoesNotExist()
        {
            //Arrange
            const string role = "bedrijf";
            var voorstel = new StagevoorstelModel { Id = 10000 };
            var user = new UserObject { Id = 11, Role = role };

            _stagevoorstelRepoMock.Setup(repository => repository.GetById(10000));
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _stagevoorstellenController.PutStagevoorstel(10000, voorstel);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        [TestCase("reviewer", 1)]
        [TestCase("bedrijf", 12)]
        [TestCase("student", 5)]
        public void PutStagevoorstel_ReturnsUnAuthorized_If_NotCoordinatorOrBedrijfWithCorrectId(string role, int userId)
        {
            //Arrange
            var voorstel = new StagevoorstelModel(_context.Stagevoorstellen.AsNoTracking()
                .Include(s => s.Bedrijf)
                .Include(s => s.StudentenFavorieten)
                .First(s => s.Id == 1), role);
            var originalVoorstel = _context.Stagevoorstellen.Find(1);
            var user = new UserObject { Id = userId, Role = role };

            _stagevoorstelRepoMock.Setup(repository => repository.GetById(1)).Returns(originalVoorstel);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _stagevoorstellenController.PutStagevoorstel(1, voorstel);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void PutStagevoorstel_ReturnsNotFound_If_UpdateFails()
        {
            //Arrange
            const string role = "bedrijf";
            var voorstel = new StagevoorstelModel(_context.Stagevoorstellen.AsNoTracking()
                .Include(s => s.Bedrijf)
                .Include(s => s.StudentenFavorieten)
                .First(s => s.Id == 1), role);
            var originalVoorstel = _context.Stagevoorstellen.Find(1);
            var user = new UserObject { Id = 11, Role = role };

            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user); _stagevoorstelRepoMock.Setup(repository => repository.GetById(1)).Returns(originalVoorstel);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(5)).Returns(originalVoorstel);

            //Act
            var result = _stagevoorstellenController.PutStagevoorstel(1, voorstel);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(2, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }

        [Test]
        [TestCase("bedrijf", 11)]
        [TestCase("coordinator", 1)]
        public void PutStagevoorstel_ReturnsNoContent_If_CoordinatorOrBedrijfWithCorrectId(string role, int userId)
        {
            //Arrange
            var voorstel = new StagevoorstelModel(_context.Stagevoorstellen.AsNoTracking()
                .Include(s => s.Bedrijf)
                .Include(s => s.StudentenFavorieten)
                .Include(s=>s.ReviewersToegewezen).ThenInclude(rt=>rt.Reviewer)
                .First(s => s.Id == 1), role);
            var originalVoorstel = _context.Stagevoorstellen.Find(1);
            var user = new UserObject { Id = userId, Role = role };

            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user); _stagevoorstelRepoMock.Setup(repository => repository.GetById(1)).Returns(originalVoorstel);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(1)).Returns(originalVoorstel);
            _stagevoorstelRepoMock.Setup(repository => repository.Update(1, It.IsAny<Stagevoorstel>())).Returns(true);

            //Act
            var result = _stagevoorstellenController.PutStagevoorstel(1, voorstel);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(2, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }
        #endregion

        #region PostStagevoorstel

        [Test]
        public void PostStagevoorstel_ReturnsBadRequest_If_BadModel()
        {
            //Arrange
            var voorstel = new StagevoorstelModel();
            var user = new UserObject { Id = 11 };

            //Act
            _stagevoorstellenController.ModelState.AddModelError("Bad", "Bad Model");
            var result = _stagevoorstellenController.PostStagevoorstel(voorstel);

            //Assert
            Assert.AreEqual(0, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        [TestCase("reviewer")]
        [TestCase("student")]
        public void PostStagevoorstel_ReturnsUnAuthorized_If_NotBedrijfOrCoordinator(string role)
        {
            //Arrange
            var voorstel = new StagevoorstelModel();
            var user = new UserObject { Role = role };

            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _stagevoorstellenController.PostStagevoorstel(voorstel);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        [TestCase("coordinator")]
        [TestCase("bedrijf")]
        public void PostStagevoorstel_ReturnsCreatedAtAction_If_EverythingOk(string role)
        {
            //Arrange
            var user = new UserObject { Role = role };

            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _stagevoorstellenController.PostStagevoorstel(new StagevoorstelModel());

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        #endregion

        #region DeleteStageVoorstel(len)

        [Test]
        public void DeleteStagevoorstel_ReturnsUnAuthorized_If_NotCoordinatorOrBedrijfWithCorrectId()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false, Id = 5 };
            var voorstel = _context.Stagevoorstellen.Find(1);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(1)).Returns(voorstel);

            //Act
            var result = _stagevoorstellenController.DeleteStagevoorstel(1);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }
        [Test]
        [TestCase(true, 1)]
        [TestCase(false, 11)]
        public void DeleteStagevoorstel_ReturnsNoContent_If_Authorized(bool isCoordinator, int userId)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = isCoordinator, Id = userId };
            var voorstel = _context.Stagevoorstellen.Find(1);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(1)).Returns(voorstel);

            //Act
            var result = _stagevoorstellenController.DeleteStagevoorstel(1);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(2, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }


        [Test]
        [TestCase("coordinator", true, 1)]
        [TestCase("bedrijf", false, 11)]
        public void DeleteStagevoorstellen_ReturnsNoContent_If_CoordinatorOrBedrijfWithCorrectId(string role, bool isCoordinator, int userId)
        {
            //Arrange
            const string ids = "1,6";
            var user = new UserObject { IsCoordinator = isCoordinator, Id = userId, Role = role };
            var voorstel = _context.Stagevoorstellen.Find(1);
            var voorstel2 = _context.Stagevoorstellen.Find(6);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(1)).Returns(voorstel);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(6)).Returns(voorstel2);

            //Act
            var result = _stagevoorstellenController.DeleteStagevoorstellen(ids);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(3, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        [TestCase("reviewer", 3)]
        [TestCase("student", 6)]
        [TestCase("bedrijf", 11)]
        public void DeleteStagevoorstellen_ReturnsUnAuthorized_If_NotCoordinatorOrBedrijfWithCorrectId(string role, int userId)
        {
            //Arrange
            const string ids = "1,7";
            var user = new UserObject { IsCoordinator = false, Id = userId, Role = role };
            var voorstel = _context.Stagevoorstellen.Find(1);
            var voorstel2 = _context.Stagevoorstellen.Find(7);
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(1)).Returns(voorstel);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(7)).Returns(voorstel2);

            //Act
            var result = _stagevoorstellenController.DeleteStagevoorstellen(ids);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(role == "bedrijf" ? 2 : 0, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }


        [Test]
        public void DeleteStagevoorstellen_ReturnsNotFound_If_IdArrayContainsWrongId()
        {
            //Arrange
            const string ids = "1,30";
            var user = new UserObject { IsCoordinator = true };
            var voorstel = _context.Stagevoorstellen.Find(1);
            var voorstel2 = _context.Stagevoorstellen.Find(30);//null
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(1)).Returns(voorstel);
            _stagevoorstelRepoMock.Setup(repository => repository.GetById(30)).Returns(voorstel2);

            //Act
            var result = _stagevoorstellenController.DeleteStagevoorstellen(ids);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(2, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        [TestCase("")]
        [TestCase("1,")]
        [TestCase(",1")]
        [TestCase("a")]
        [TestCase("1,a")]
        public void DeleteStagevoorstellen_ReturnsBadRequest_If_InvalidQueryInput(string ids)
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _stagevoorstellenController.DeleteStagevoorstellen(ids);

            //Assert
            Assert.AreEqual(0, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        #endregion

        #region PatchStagevoorstel
        [Test]
        public void PatchStagevoorstel_ReturnsNotFound_If_VoorstelDoesNotExist()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.PatchStatus(1000, 2)).Returns(false);

            //Act
            var result = _stagevoorstellenController.PatchStagevoorstel(1000, 2);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void PatchStagevoorstel_ReturnsNoContent_If_EverythingOk()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _stagevoorstelRepoMock.Setup(repository => repository.PatchStatus(1, 2)).Returns(true);

            //Act
            var result = _stagevoorstellenController.PatchStagevoorstel(1, 2);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void PatchStagevoorstel_ReturnsUnAuthorized_If_NotCoordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);

            //Act
            var result = _stagevoorstellenController.PatchStagevoorstel(1, 2);

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _stagevoorstelRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }


        #endregion
    }
}

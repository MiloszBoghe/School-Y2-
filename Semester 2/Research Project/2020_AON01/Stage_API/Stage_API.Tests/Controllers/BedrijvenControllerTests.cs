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
    public class BedrijvenControllerTests
    {
        private Mock<IBedrijfRepository> _bedrijfRepoMock;
        private Mock<IRequestHelper> _helperMock;
        private BedrijvenController _bedrijvenController;

        [SetUp]
        public void SetUp()
        {
            _bedrijfRepoMock = new Mock<IBedrijfRepository>();
            _helperMock = new Mock<IRequestHelper>();
            _bedrijvenController = new BedrijvenController(_bedrijfRepoMock.Object, _helperMock.Object);
        }

        [Test]
        public void GetBedrijven_ReturnsOk_IfCoordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = true };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _bedrijfRepoMock.Setup(repository => repository.GetAll()).Returns(new List<Bedrijf>());

            //Act
            var result = _bedrijvenController.GetBedrijven();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(1, _bedrijfRepoMock.Invocations.Count);
            Assert.IsInstanceOf<OkObjectResult>(result);

        }

        [Test]
        public void GetBedrijven_ReturnsUnauthorized_IfNotCoordinator()
        {
            //Arrange
            var user = new UserObject { IsCoordinator = false };
            _helperMock.Setup(helper => helper.GetUser(null)).Returns(user);
            _bedrijfRepoMock.Setup(repository => repository.GetAll()).Returns(new List<Bedrijf>());

            //Act
            var result = _bedrijvenController.GetBedrijven();

            //Assert
            Assert.AreEqual(1, _helperMock.Invocations.Count);
            Assert.AreEqual(0, _bedrijfRepoMock.Invocations.Count);
            Assert.IsInstanceOf<UnauthorizedResult>(result);

        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Stage_API.Business.Models;
using Stage_API.Controllers;

namespace Stage_API.Tests.Controllers
{
    public class ClaimControllerTests
    {
        private Mock<HttpContext> _HttpContextMock;
        private ClaimsController _claimsController;

        [SetUp]
        public void SetUp()
        {
            _HttpContextMock = new Mock<HttpContext>();
            _claimsController = new ClaimsController();
        }

        // GET api/Claims
        [Test]
        public void GetClaims_Returns_Correct_ClaimModel()
        {
            //Arrange
            var claims = new List<Claim> {
                new Claim("id","1"),
                new Claim("jti","testjti"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress","testemail"),
                new Claim("voornaam","testvoornaam"),
                new Claim("naam","testnaam"),
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role","testrole"),
                new Claim("exp","testexp"),
                new Claim("iss","testiss"),
                new Claim("aud","testaud")
            };
            var claimsIdentity = new List<ClaimsIdentity>() { new ClaimsIdentity(claims) };
            var principal = new ClaimsPrincipal(claimsIdentity);

            _claimsController.ControllerContext.HttpContext = new DefaultHttpContext();
            _claimsController.HttpContext.User = principal;

            //Act
            var result = _claimsController.GetClaims();

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var claimModel = (ClaimModel)((OkObjectResult)result).Value;
            Assert.AreEqual(1, claimModel.Id);
            Assert.AreEqual("testjti", claimModel.Jti);
            Assert.AreEqual("testemail", claimModel.Email);
            Assert.AreEqual("testvoornaam", claimModel.Voornaam);
            Assert.AreEqual("testnaam", claimModel.Naam);
            Assert.AreEqual("testrole", claimModel.Role);
            Assert.AreEqual("testexp", claimModel.Exp);
            Assert.AreEqual("testiss", claimModel.Iss);
            Assert.AreEqual("testaud", claimModel.Aud);

        }

    }
}

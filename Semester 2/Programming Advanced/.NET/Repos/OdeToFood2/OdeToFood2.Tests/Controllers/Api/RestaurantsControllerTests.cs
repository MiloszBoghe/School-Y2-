using Moq;
using NUnit.Framework;
using OdeToFood2.Controllers.Api;

namespace OdeToFood2.Tests.Controllers.Api
{
    public class RestaurantsControllerTests
    {
        private Mock<RestaurantsController> _restaurantsMock;

        [SetUp]
        public void SetUp()
        {
            _restaurantsMock = new Mock<RestaurantsController>();
        }

        [Test]
        public void Get_ReturnsAllRestaurantsFromRepository() { }

        [Test]
        public void Get_ReturnsRestaurantIfItExists() { }

        [Test]
        public void Get_ReturnsNotFoundIfItDoesNotExists() { }

        [Test]
        public void Post_ValidRestaurantIsSavedInRepository() { }

        [Test]
        public void Post_InValidRestaurantCausesBadRequest() { }

        [Test]
        public void Put_ExistingRestaurantIsSavedInRepository() { }

        [Test]
        public void Put_NonExistingRestaurantReturnsNotFound() { }

        [Test]
        public void Put_InValidRestaurantModelStateCausesBadRequest() { }

        [Test]
        public void Put_MismatchBetweenUrlIdAndRestaurantIdCausesBadRequest() { }

        [Test]
        public void Delete_ExistingRestaurantIsDeletedFromRepository() { }

        [Test]
        public void Delete_NonExistingRestaurantReturnsNotFound() { }
    }
}

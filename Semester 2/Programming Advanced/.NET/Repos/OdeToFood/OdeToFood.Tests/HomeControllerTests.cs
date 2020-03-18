using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using OdeToFood.Controllers;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Tests
{
    class HomeControllerTests
    {
        private HomeController _homeController;


        [Test]
        public void Create_WhenModelIsInvalid_ReturnCreateView()
        {
            _homeController.ModelState.AddModelError("error","error");
            var actionResult = _homeController.Create(new EditRestaurantViewModel
            {
                Name = string.Empty
            });
            Assert.That(actionResult, Is.InstanceOf<ViewResult>());
            var viewResult = (ViewResult) actionResult;
            Assert.That(viewResult.ViewName, Is.EqualTo(nameof(HomeController.Create)));

        }

        [Test]
        public void Create_WhenModelIsValid_RedirectToDetailsView()
        {
            var actionResult = _homeController.Create(new EditRestaurantViewModel
            {
                Name = "Ik ben geldig"
            });

            Assert.That(actionResult, Is.InstanceOf<RedirectToActionResult>());
            var redirectActionResult = (RedirectToActionResult)actionResult;
            Assert.That(redirectActionResult.ActionName, Is.EqualTo(nameof(HomeController.Details)));

        }

        [SetUp]
        public void SetUp()
        {
            _homeController = new HomeController(new InMemoryRestaurantData(), new Greeter());
        }

    }
}

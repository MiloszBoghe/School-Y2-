using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using OdeToFood.ViewModels;

namespace OdeToFood.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Validate_NameIsEmpty_IsNotValid()
        {
            var restaurant = new EditRestaurantViewModel
            {
                Name = string.Empty
            };
            var context = new ValidationContext(restaurant);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(restaurant, context, results, true);
            Assert.That(isValid, Is.False);

        }

        [Test]
        public void Validate_NameIsExactly30Characters_IsValid()
        {
            var restaurant = new EditRestaurantViewModel
            {
                Name = "IAmExactlyThirtyCharactersLong"
            };
            var context = new ValidationContext(restaurant);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(restaurant, context, results, true);
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void Validate_NameIsLongerThan30Characters_IsNotValid()
        {
            var restaurant = new EditRestaurantViewModel
            {
                Name = "IAmMoreThanThirtyCharactersLong"
            };
            var context = new ValidationContext(restaurant);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(restaurant, context, results, true);
            Assert.That(isValid, Is.False);
        }


    }
}
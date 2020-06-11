using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PasswordApp.Data;
using PasswordApp.Data.Repositories;
using PasswordApp.Test.Builders;
using PasswordApp.Web.Controllers;
using PasswordApp.Web.Models;
using PasswordApp.Web.Services;
using PasswordApp.Web.Services.Contracts;

namespace PasswordApp.Test
{
    public class EntriesControllerTests
    {
        private static readonly Random RandomGenerator = new Random();
        private Guid _userId;

        private Mock<IEntryService> _entryServiceMock;
        private Mock<IConverter> _converterMock;
        private EntryBuilder _entryBuilder;
        private EntriesController _entriesController;

        [SetUp]
        public void SetUp()
        {
            _entryBuilder = new EntryBuilder();
            _entryServiceMock = new Mock<IEntryService>();
            _converterMock = new Mock<IConverter>();
            _entriesController = new EntriesController(_entryServiceMock.Object, _converterMock.Object)
            {
                ControllerContext = CreateContextWithLoggedInUser()
            };
        }
        [Test]
        public void Index_ShouldRetrieveEntriesOfUserAndConvertThemToListItemModels()
        {
            //KINDA DONE..?: implement the following test scenario
            //DONE  * Create a list that contains a random number of entries (minimal 1 entry and maximal 10 entries). Tip: use the EntryBuilder class in the "Builders" folder.
            //DONE  * Setup a mock of IEntryService that always returns the list of entries you created when GetEntriesOfUserAsync is executed.
            //DONE  * Setup a mock of IConverter.
            //DONE    Tip: the default behavior of the fake will be enough.
            //DONE  * Instantiate an EntriesController that uses the mocks. Use the CreateContextWithLoggedInUser helper method in this class to fake a logged in user.
            //DONE  * Execute the Index action and verify:
            //DONE      - A ViewResult should be returned
            //DONE      - The service should be called one time with the id of the logged in user.
            //DONE BUT ERROR SO COMMENTED     - The converter should be called once for each entry returned by the service to convert the Entry to an EntryListItemViewModel.
            //DONE BUT ERROR SO COMMENTED       Tip: Use 'It.IsIn<Entry>(...)'. Make sure the generic argument (<Entry>) is provided when you use this method.
            //DONE      - The model of the ViewResult should be an IReadOnlyList of EntryListItemViewModel and should have the same amount of items as the amount of entries returned by the service.

            // Production code tips: 
            //  * Tip: Inspecting the CreateContextWithLoggedInUser helper method will give you insight in what claim can be retrieved to get the id of the user.
            //  * Tip: A string can be converted to a Guid with the Guid.Parse method.
            //  * Tip: List<T> implements IReadOnlyList<T>

            //Arrange
            var amountOfEntries = RandomGenerator.Next(1, 11);
            var entryList = new List<Entry>();
            for (var i = 0; i < amountOfEntries; i++)
            {
                entryList.Add(_entryBuilder.Build());
            }

            _entryServiceMock.Setup(service => service.GetEntriesOfUserAsync(_userId)).ReturnsAsync(entryList);
            _converterMock.Setup(converter => converter.ConvertTo<EntryListItemViewModel>(It.IsIn<Entry>(entryList))).CallBase();

            var result = _entriesController.Index().Result as ViewResult;

            _entryServiceMock.Verify(service => service.GetEntriesOfUserAsync(_userId), Times.Once);
            _converterMock.Verify(converter => converter.ConvertTo<EntryListItemViewModel>(It.IsIn<Entry>(entryList)), Times.Exactly(amountOfEntries));
            Assert.NotNull(result);
            Assert.IsInstanceOf<IReadOnlyList<EntryListItemViewModel>>(result.Model);
            Assert.AreEqual(result.ViewData.Values.Count,entryList.Count);


        }

        [Test]
        public void Edit_Get_ShouldRetrieveTheEntryAndConvertItToAnEditViewModel()
        {
            //Arrange
            var entry = _entryBuilder.Build();
            _entryServiceMock.Setup(service => service.GetById(entry.Id)).Returns(entry);
            //Act
            var result = _entriesController.Edit(entry.Id) as ViewResult;

            //Assert
            _entryServiceMock.Verify(service => service.GetById(entry.Id), Times.Once);
            Assert.NotNull(result);
            Assert.IsInstanceOf<EntryEditViewModel>(result.Model);
        }

        private ControllerContext CreateContextWithLoggedInUser()
        {
            _userId = Guid.NewGuid();
            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, _userId.ToString()) }))
                }
            };
            context.HttpContext.User.AddIdentity(new ClaimsIdentity());
            return context;
        }
    }
}
using Moq;
using NUnit.Framework;
using PasswordApp.Data.Repositories;
using PasswordApp.Test.Builders;
using PasswordApp.Web.Services;
using PasswordApp.Web.Services.Contracts;

namespace PasswordApp.Test
{
    public class EntryServiceTests
    {

        private Mock<IEntryRepository> _entryRepositoryMock;
        private Mock<IEncryptionService> _encryptionServiceMock;
        private EntryService _entryService;
        private EntryBuilder _entryBuilder;
        [SetUp]
        public void SetUp()
        {
            _entryBuilder = new EntryBuilder();
            _entryRepositoryMock = new Mock<IEntryRepository>();
            _encryptionServiceMock = new Mock<IEncryptionService>();
            _entryService = new EntryService(_entryRepositoryMock.Object, _encryptionServiceMock.Object);
        }
        [Test]
        public void Update_ShouldEncryptThePasswordAndUpdateUsingTheRepository()
        {
            //Tip: the service will need a repository to retrieve entries and a service to encrypt/decrypt passwords.
            //Tip: you can make use of the EntryBuilder class in the 'Builders' folder.
            //Tip: a string representation of the Id of the Entry should be used as 'salt' to encrypt the password.

            //Arrange
            var entry = _entryBuilder.Build();
            _encryptionServiceMock.Setup(service => service.Encrypt(entry.Password, entry.Id.ToString())).Returns("encrypted");
            _entryRepositoryMock.Setup(repo => repo.Update(entry.Id,"encrypted", entry.Url));

            //Act
            _entryService.Update(entry.Id, entry.Password, entry.Url);

            //Assert
            _encryptionServiceMock.Verify(service => service.Encrypt(entry.Password, entry.Id.ToString()), Times.Once());
            _entryRepositoryMock.Verify(repo => repo.Update(entry.Id, "encrypted", entry.Url), Times.Once);
        }
    }
}
using PasswordApp.Data;
using PasswordApp.Data.Repositories;
using PasswordApp.Web.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordApp.Web.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IEncryptionService _encryptionService;
        public EntryService(IEntryRepository entryRepository, IEncryptionService encryptionService)
        {
            _entryRepository = entryRepository;
            _encryptionService = encryptionService;
        }
        public Entry GetById(Guid id)
        {
            //DONE: retrieve the entry from the repository, decrypt the password and return the entry.
            //You should use the 'Id' of the entry as the salt for decrypting the password.
            var entry = _entryRepository.GetById(id);
            var password = _encryptionService.Decrypt(entry.Password, id.ToString());
            entry.Password = password;
            return entry;
        }

        public void Update(Guid id, string password, string url)
        {
            //DONE: implement the test for this method and make it green
            //You should use the 'Id' of the entry as the salt for encrypting the password.
            var encryptedPassword = _encryptionService.Encrypt(password, id.ToString());
            _entryRepository.Update(id, encryptedPassword, url);
        }

        public async Task<IReadOnlyList<Entry>> GetEntriesOfUserAsync(Guid userId)
        {
            //DONE: retrieve the entries using the 'GetEntriesOfUserAsync' method of the repository and just return the retrieved entries.
            return await _entryRepository.GetEntriesOfUserAsync(userId);
        }
    }
}
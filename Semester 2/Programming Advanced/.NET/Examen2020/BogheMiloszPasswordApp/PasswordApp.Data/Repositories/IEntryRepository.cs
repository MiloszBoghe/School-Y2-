using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordApp.Data.Repositories
{
    public interface IEntryRepository
    {
        Entry GetById(Guid id);
        void Update(Guid id, string encryptedPassword, string url);
        Task<IReadOnlyList<Entry>> GetEntriesOfUserAsync(Guid userId);
    }
}
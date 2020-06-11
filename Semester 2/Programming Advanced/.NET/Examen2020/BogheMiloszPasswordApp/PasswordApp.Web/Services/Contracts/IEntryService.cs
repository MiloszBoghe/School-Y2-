using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordApp.Data;

namespace PasswordApp.Web.Services.Contracts
{
    public interface IEntryService
    {
        Entry GetById(Guid id);
        void Update(Guid id, string password, string url);
        Task<IReadOnlyList<Entry>> GetEntriesOfUserAsync(Guid userId);
    }
}
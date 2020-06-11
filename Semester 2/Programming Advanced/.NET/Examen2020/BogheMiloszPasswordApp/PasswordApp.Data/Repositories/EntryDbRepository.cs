using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PasswordApp.Data.Repositories
{
    public class EntryDbRepository: IEntryRepository
    {
        private readonly PasswordDbContext _context;

        public EntryDbRepository(PasswordDbContext context)
        {
            _context = context;
        }

        public Entry GetById(Guid id)
        {
            return _context.Set<Entry>().Find(id);
        }

        public void Update(Guid id, string encryptedPassword, string url)
        {
            var entryToUpdate = GetById(id);

            entryToUpdate.Password = encryptedPassword;
            entryToUpdate.Url = url;

            _context.SaveChanges();
        }

        public async Task<IReadOnlyList<Entry>> GetEntriesOfUserAsync(Guid userId)
        {
            return await _context.Set<Entry>().Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
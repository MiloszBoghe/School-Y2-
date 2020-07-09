using PasswordApp.Data;
using System;

namespace PasswordApp.Test.Builders
{
    public class EntryBuilder
    {
        private readonly Entry _entry;

        public EntryBuilder()
        {
            _entry = new Entry
            {
                Id = Guid.NewGuid(),
                UserName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid()
            };
        }

        public EntryBuilder WithPassword(string password)
        {
            _entry.Password = password;
            return this;
        }

        public Entry Build()
        {
            return _entry;
        }
    }
}
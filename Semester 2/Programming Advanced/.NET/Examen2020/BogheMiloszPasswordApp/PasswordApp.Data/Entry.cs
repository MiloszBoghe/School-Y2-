using System;

namespace PasswordApp.Data
{
    public class Entry
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PasswordApp.Data
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<Entry> Entries { get; set; }
    }
}
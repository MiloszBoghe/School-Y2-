using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PasswordApp.Data
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<Entry> Entries { get; set; }
    }
}
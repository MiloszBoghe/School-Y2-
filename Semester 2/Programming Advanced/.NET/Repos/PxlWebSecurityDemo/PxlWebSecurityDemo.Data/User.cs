using System;
using Microsoft.AspNetCore.Identity;

namespace PxlWebSecurityDemo.Data
{
    public class User : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
    }
}
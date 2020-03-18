using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace PxlWebSecurityDemo.Data
{
    public class Role : IdentityRole<int>
    {
        public const string Lector = "lector";
        public const string Student = "student";
    }
}

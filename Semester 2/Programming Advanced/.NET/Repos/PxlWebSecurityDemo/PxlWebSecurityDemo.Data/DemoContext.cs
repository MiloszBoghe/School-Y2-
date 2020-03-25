using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PxlWebSecurityDemo.Data
{
    public class DemoContext : IdentityDbContext<User, Role, int>
    {
        public DemoContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}

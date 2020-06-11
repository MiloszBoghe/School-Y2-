using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PasswordApp.Data
{
    public class PasswordDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Entry> Entries { get; set; }
        public PasswordDbContext(DbContextOptions<PasswordDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //relation - 1 User --> n Entries
            modelBuilder.Entity<Entry>()
                .HasOne(e => e.User)
                .WithMany(u=>u.Entries)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

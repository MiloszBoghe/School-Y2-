using System;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SamuraiAppData;Integrated Security=True";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(sb => new {sb.SamuraiId, sb.BattleId});

            modelBuilder.Entity<Battle>().HasData(
                new Battle
                {
                    Id = 1,
                    Name = "The great battle",
                    StartDate = new DateTime(902, 1, 30),
                    EndDate = new DateTime(902, 2, 10)
                },
                new Battle
                {
                    Id = 2,
                    Name = "The long battle",
                    StartDate = new DateTime(903, 10, 10),
                    EndDate = new DateTime(904, 10, 10)
                });
        }
    }
}

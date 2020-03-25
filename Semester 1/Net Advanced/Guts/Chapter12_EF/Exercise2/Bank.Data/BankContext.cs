using System;
using System.Collections.Generic;
using System.Configuration;
using Bank.Data.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data
{
    public class BankContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public BankContext() { } //Constructor used by UI project

        public BankContext(DbContextOptions<BankContext> options) : base(options) { } //Constructor used by Test project

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) //only configure the connection if the parameter-less constructor was used (no options where provided).
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BankConnection"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(a => a.AccountNumber).IsRequired();
            modelBuilder.Entity<City>().HasKey(c => c.ZipCode);

            modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.FirstName).IsRequired();
            modelBuilder.Entity<Customer>()
                .HasOne(a => a.City)
                .WithMany(b => b.Customers)
                .HasForeignKey(c => c.ZipCode)
                .HasPrincipalKey(d => d.ZipCode);

            modelBuilder.Entity<City>().HasData(new City() { Name = "Antwerpen", ZipCode = 2000 });
            modelBuilder.Entity<City>().HasData(new City() { Name = "Leuven", ZipCode = 3000 });
            modelBuilder.Entity<City>().HasData(new City() { Name = "Hasselt", ZipCode = 3500 });
            modelBuilder.Entity<City>().HasData(new City() { Name = "Brugge", ZipCode = 8000 });
            modelBuilder.Entity<City>().HasData(new City() { Name = "Gent", ZipCode = 9000 });
        }


        public void CreateOrUpdateDatabase()
        {
            Database.Migrate();
        }
    }
}

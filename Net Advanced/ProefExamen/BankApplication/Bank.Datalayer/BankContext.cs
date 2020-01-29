using Bank.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace Bank.Datalayer
{
    public class BankContext : DbContext
    {
        // DONE: Vul deze klasse aan
        public BankContext() { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BankDB;Integrated Security=True";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //pk account
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<City>().HasKey(c => c.ZipCode);
            
            //relatie account-customer + pk customer + fk account
            modelBuilder.Entity<Account>()
                        .HasOne(a => a.Customer)
                        .WithMany(c => c.Accounts)
                        .HasForeignKey(a => a.CustomerId)
                        .HasPrincipalKey(c => c.CustomerId);

            //relatie customer-city
            modelBuilder.Entity<Customer>()
                        .HasOne(cu => cu.City)
                        .WithMany(ci => ci.Customers)
                        .HasForeignKey(cu => cu.ZipCode)
                        .HasPrincipalKey(ci => ci.ZipCode);
            
        }
    }
}

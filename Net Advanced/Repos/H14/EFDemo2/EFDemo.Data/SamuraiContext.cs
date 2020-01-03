using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace EFDemo.Data
{
    public class SamuraiContext: DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var consoleLoggerProvider = new ConsoleLoggerProvider(
                filter: (category, level) =>
                
                    category == DbLoggerCategory.Database.Command.Name &&
                    level == LogLevel.Information,
                    includeScopes:true);

            var loggerFactory = new LoggerFactory(providers: new ILoggerProvider[]
            {
                consoleLoggerProvider
            });

            builder.UseLoggerFactory(loggerFactory)
                    .UseSqlServer("server=(localdb)\\MsSQLLocalDb;" +
                                  "Database = SamuraiDemo2; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SamuraiBattle>().HasKey(sb => new {sb.BattleId, sb.SamuraiId});
        }
    }

}

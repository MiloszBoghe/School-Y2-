using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Stage_API.Data;

namespace Stage_API.Tests
{
    [SetUpFixture]
    public class TestHelper
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=StageSysteemDatabase_TEST;Trusted_Connection=True;";

        private static readonly DbContextOptions<StageContext> DbContextOptions =
            new DbContextOptionsBuilder<StageContext>()
                .UseSqlServer(ConnectionString)
                .Options;
        public static StageContext Context = new StageContext(DbContextOptions);

        [OneTimeSetUp]
        public void Setup()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

    }
}

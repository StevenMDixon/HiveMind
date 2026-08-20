using Microsoft.EntityFrameworkCore;

namespace HiveMind.Tests.SchedulerTests;

public class SchedulerTests
{

    private readonly TestDbContext _context;

    public SchedulerTests()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TestDbContext(options);
        TestSeedData.LoadTestData(_context);
    }


    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}

using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server;

public class sqliteDBContext: DbContext
{
    public DbSet<Station> Stations { get; set; }
    public DbSet<Query> Queries { get; set; }
    public DbSet<QueryFilters> QueryFilters { get; set; }
    public DbSet<QueryLineupItem> QueryLineupItems { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<MediaItem> MediaItems { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<Lineup> Lineups { get; set; }
    public DbSet<LineupItem> LineupItems { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<BlockQuery> BlockQueries { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<ProgramStrategy> ProgramStrategies { get; set;}
    public DbSet<ProgramStrategyLineup> ProgramStrategyLineups { get; set;}

    public sqliteDBContext(DbContextOptions<sqliteDBContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Station>().HasData(
            new Station { StationId = 1, StationName = "Test1", StationNumber = 1 },
            new Station { StationId = 2, StationName = "Test2", StationNumber = 2 },
            new Station { StationId = 3, StationName = "Test3", StationNumber = 3 }
        );

        modelBuilder.Entity<Settings>().HasData(
            new Settings { SettingsId = 1, Name = "Import_Location", Value = "/" },
            new Settings { SettingsId = 2, Name = "Export_Location", Value = "/" }
        );
    }

    public static string GetDataBaseConnectionString()
    {
        //check if running in docker
        //bool inDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        //check if config folder is mounted
        var configFolder = Directory.Exists("/config") ? "/config" : "";
        var path = System.IO.Path.Combine(configFolder, "HiveMind.db");

        Console.WriteLine($"Using database folder: {path}");
        Console.WriteLine(System.IO.File.Exists(path));

        return $"Data Source={path}";
    }
}

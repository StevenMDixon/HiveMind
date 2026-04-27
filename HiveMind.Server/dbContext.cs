using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;


namespace HiveMind.Server;

public class sqliteDBContext: DbContext
{
    public DbSet<Channel> Channels { get; set; }
    public DbSet<Query> Queries { get; set; }
    public DbSet<QueryFilters> QueryFilters { get; set; }
    public DbSet<QueryLineupItem> QueryLineupItems { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<MediaItem> MediaItems { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<Lineup> Lineups { get; set; }
    public DbSet<LineupItem> LineupItems { get; set; }
    public DbSet<Tags> Tags { get; set; }

    public sqliteDBContext(DbContextOptions<sqliteDBContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Channel>().HasData(
            new Channel { ChannelId = 1, ChannelName = "Test1"},
            new Channel { ChannelId = 2, ChannelName = "Test2"},
            new Channel { ChannelId = 3, ChannelName = "Test3"}
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

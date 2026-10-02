using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server;

public class SqliteDBContext(DbContextOptions<SqliteDBContext> options) : DbContext(options)
{
    public DbSet<Query> Queries { get; set; }
    public DbSet<QueryFilters> QueryFilters { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<MediaItem> MediaItems { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<Lineup> Lineups { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<ProgramStrategy> ProgramStrategies { get; set;}
    public DbSet<ProgramStrategyLineup> ProgramStrategyLineups { get; set;}
    public DbSet<ScheduleBatch> ScheduleBatches { get; set; }
    public DbSet<ScheduleBatchItem> ScheduleBatchItems { get; set; }
    public DbSet<TransitionTemplate> TransitionTemplates { get; set; }
    public DbSet<TransitionTemplateSlot> TransitionTemplateSlots { get; set; }
    public DbSet<SchedulingResult> SchedulingResults { get; set; }
    public DbSet<Drone> Drones { get; set; }
    public DbSet<ProgramEvent> ProgramEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Station>().HasData(
            new Station { StationId = 1, StationName = "Test1", StationNumber = 1 },
            new Station { StationId = 2, StationName = "Test2", StationNumber = 2 },
            new Station { StationId = 3, StationName = "Test3", StationNumber = 3 }
        );

        modelBuilder.Entity<Settings>().HasData(
            new Settings { SettingsId = 1, Name = "Import_Location", Value = "/" },
            new Settings { SettingsId = 2, Name = "Export_Location", Value = "/" },
            new Settings { SettingsId = 3, Name = "Bump In Tag", Value = "In" },
            new Settings { SettingsId = 4, Name = "Bump Out Tag", Value = "Out" },
            new Settings { SettingsId = 5, Name = "Bump Generic Tag", Value = "Generic" }
        );

        modelBuilder.Entity<TransitionTemplate>().HasData(
            new TransitionTemplate { TransitionTemplateId = 1, Name = "Default MidRoll"},
            new TransitionTemplate { TransitionTemplateId = 2, Name = "Default PostRoll" }
        );

        modelBuilder.Entity<TransitionTemplateSlot>().HasData(
            new TransitionTemplateSlot { TransitionTemplateSlotId = 1, TransitionTemplateId = 1, Slot = TransitionSlot.OutBump, Index = 0},
            new TransitionTemplateSlot { TransitionTemplateSlotId = 2, TransitionTemplateId = 1, Slot = TransitionSlot.Ident, Index = 1},
            new TransitionTemplateSlot { TransitionTemplateSlotId = 3, TransitionTemplateId = 1, Slot = TransitionSlot.Filler, Index = 2},
            new TransitionTemplateSlot { TransitionTemplateSlotId = 4, TransitionTemplateId = 1, Slot = TransitionSlot.Ident, Index = 3},
            new TransitionTemplateSlot { TransitionTemplateSlotId = 5, TransitionTemplateId = 1, Slot = TransitionSlot.InBump, Index = 4},
            new TransitionTemplateSlot { TransitionTemplateSlotId = 6, TransitionTemplateId = 2, Slot = TransitionSlot.Ident, Index = 0},
            new TransitionTemplateSlot { TransitionTemplateSlotId = 7, TransitionTemplateId = 2, Slot = TransitionSlot.Promo, Index = 1},
            new TransitionTemplateSlot { TransitionTemplateSlotId = 8, TransitionTemplateId = 2, Slot = TransitionSlot.Filler, Index = 2},
            new TransitionTemplateSlot { TransitionTemplateSlotId = 9, TransitionTemplateId = 2, Slot = TransitionSlot.Ident, Index = 3}
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

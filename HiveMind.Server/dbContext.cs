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
    public DbSet<ProgramDatePart> ProgramDateParts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Settings>().HasData(
            new Settings { Id = 1, Name = "Import_Location", Value = "/" },
            new Settings { Id = 2, Name = "Export_Location", Value = "/" },
            new Settings { Id = 3, Name = "Bump In Tag", Value = "In" },
            new Settings { Id = 4, Name = "Bump Out Tag", Value = "Out" },
            new Settings { Id = 5, Name = "Bump Generic Tag", Value = "Generic" },
            new Settings { Id = 6, Name = "Schedule Retention Days", Value = "7" }
        );

        modelBuilder.Entity<TransitionTemplate>().HasData(
            new TransitionTemplate { Id = 1, Name = "Default MidRoll"},
            new TransitionTemplate { Id = 2, Name = "Default PostRoll" }
        );

        modelBuilder.Entity<TransitionTemplateSlot>().HasData(
            new TransitionTemplateSlot { Id = 1, TransitionTemplateId = 1, Slot = TransitionSlot.OutBump, Index = 0},
            new TransitionTemplateSlot { Id = 2, TransitionTemplateId = 1, Slot = TransitionSlot.Ident, Index = 1},
            new TransitionTemplateSlot { Id = 3, TransitionTemplateId = 1, Slot = TransitionSlot.Filler, Index = 2},
            new TransitionTemplateSlot { Id = 4, TransitionTemplateId = 1, Slot = TransitionSlot.Ident, Index = 3},
            new TransitionTemplateSlot { Id = 5, TransitionTemplateId = 1, Slot = TransitionSlot.InBump, Index = 4},
            new TransitionTemplateSlot { Id = 6, TransitionTemplateId = 2, Slot = TransitionSlot.Ident, Index = 0},
            new TransitionTemplateSlot { Id = 7, TransitionTemplateId = 2, Slot = TransitionSlot.Promo, Index = 1},
            new TransitionTemplateSlot { Id = 8, TransitionTemplateId = 2, Slot = TransitionSlot.Filler, Index = 2},
            new TransitionTemplateSlot { Id = 9, TransitionTemplateId = 2, Slot = TransitionSlot.Ident, Index = 3}
        );

        modelBuilder.Entity<Tags>().HasData(
            new Tags{ Id = 1, Name = "1980", Category = TagCategory.Airing_Year},
            new Tags{ Id = 2, Name = "1981", Category = TagCategory.Airing_Year},            
            new Tags{ Id = 3, Name = "1982", Category = TagCategory.Airing_Year},
            new Tags{ Id = 4, Name = "1983", Category = TagCategory.Airing_Year},
            new Tags{ Id = 5, Name = "1984", Category = TagCategory.Airing_Year},
            new Tags{ Id = 6, Name = "1985", Category = TagCategory.Airing_Year},
            new Tags{ Id = 7, Name = "1986", Category = TagCategory.Airing_Year},
            new Tags{ Id = 8, Name = "1987", Category = TagCategory.Airing_Year},
            new Tags{ Id = 9, Name = "1988", Category = TagCategory.Airing_Year},
            new Tags{ Id = 10, Name = "1989", Category = TagCategory.Airing_Year},
            new Tags{ Id = 11, Name = "1990", Category = TagCategory.Airing_Year},
            new Tags{ Id = 12, Name = "1991", Category = TagCategory.Airing_Year},
            new Tags{ Id = 13, Name = "1992", Category = TagCategory.Airing_Year},
            new Tags{ Id = 14, Name = "1993", Category = TagCategory.Airing_Year},
            new Tags{ Id = 15, Name = "1994", Category = TagCategory.Airing_Year},
            new Tags{ Id = 16, Name = "1995", Category = TagCategory.Airing_Year},
            new Tags{ Id = 17, Name = "1996", Category = TagCategory.Airing_Year},
            new Tags{ Id = 18, Name = "1997", Category = TagCategory.Airing_Year},
            new Tags{ Id = 19, Name = "1998", Category = TagCategory.Airing_Year},
            new Tags{ Id = 20, Name = "1999", Category = TagCategory.Airing_Year},
            new Tags{ Id = 21, Name = "2000", Category = TagCategory.Airing_Year},
            new Tags{ Id = 22, Name = "2001", Category = TagCategory.Airing_Year},
            new Tags{ Id = 23, Name = "2002", Category = TagCategory.Airing_Year},
            new Tags{ Id = 24, Name = "2003", Category = TagCategory.Airing_Year},
            new Tags{ Id = 25, Name = "2004", Category = TagCategory.Airing_Year},
            new Tags{ Id = 26, Name = "2005", Category = TagCategory.Airing_Year},
            new Tags{ Id = 27, Name = "2006", Category = TagCategory.Airing_Year},
            new Tags{ Id = 28, Name = "2007", Category = TagCategory.Airing_Year},
            new Tags{ Id = 29, Name = "2008", Category = TagCategory.Airing_Year},
            new Tags{ Id = 30, Name = "2009", Category = TagCategory.Airing_Year},

            new Tags{ Id = 31, Name = "Day", Category = TagCategory.Time_Slot},
            new Tags{ Id = 32, Name = "Morning", Category = TagCategory.Time_Slot},
            new Tags{ Id = 33, Name = "Evening", Category = TagCategory.Time_Slot},
            new Tags{ Id = 34, Name = "Night", Category = TagCategory.Time_Slot},
            new Tags{ Id = 35, Name = "Prime Time", Category = TagCategory.Time_Slot},
            new Tags{ Id = 36, Name = "Over Night", Category = TagCategory.Time_Slot},
            
            new Tags{ Id = 37, Name = "Cartoon Network", Category = TagCategory.Network},
            new Tags{ Id = 38, Name = "Nickolodeon", Category = TagCategory.Network},
            new Tags{ Id = 39, Name = "Disney", Category = TagCategory.Network},
            new Tags{ Id = 40, Name = "SYFY", Category = TagCategory.Network},
            new Tags{ Id = 41, Name = "Comedy Central", Category = TagCategory.Network}
        );
    }

    public static string GetDataBaseConnectionString()
    {
        // var databasePath = Environment.GetEnvironmentVariable("DATABASE_PATH");
        
        //check if config folder is mounted
        var configFolder = Directory.Exists("/config") ? "/config" : "";
        var path = System.IO.Path.Combine(configFolder, "HiveMind.db");

        Console.WriteLine($"Using database folder: {path}");
        Console.WriteLine(System.IO.File.Exists(path));

        return $"Data Source={path}";
    }
}

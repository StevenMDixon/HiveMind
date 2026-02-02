using System.Reflection;
using HiveMind.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

namespace HiveMind.Server;

public class sqliteDBContext: DbContext
{
    public DbSet<Channel> Channels { get; set; }

    public sqliteDBContext(DbContextOptions<sqliteDBContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Channel>().HasData(
            new Channel { ChannelID = 1, ChannelName = "Test1"},
            new Channel { ChannelID = 2, ChannelName = "Test2"},
            new Channel { ChannelID = 3, ChannelName = "Test3"}
        );
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Channel>().ToTable("Channels", "dbo");
    //    modelBuilder.Entity<Channel>(entity => entity.HasKey(c => c.ChannelID));

    //    base.OnModelCreating(modelBuilder);
    //}

    public static string GetDataBaseConnectionString()
    {
        //check if config folder is mounted
        var configFolder = Directory.Exists("config/") ? "config/" : "";

        Console.WriteLine($"Using database folder: {configFolder}");

        return $"Data Source={configFolder}HiveMind.db";
    }
}

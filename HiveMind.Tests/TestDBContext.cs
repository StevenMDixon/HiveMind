using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Tests;
public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }

    public DbSet<MediaItem> MediaItems { get; set; } = null!;
    public DbSet<Tags> Tags { get; set; } = null!;
    public DbSet<Show> Shows { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MediaItem>(entity =>
        {
            entity.HasKey(e => e.MediaItemId);
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.FilePath).IsRequired();
        });

        modelBuilder.Entity<Tags>(entity =>
        {
            entity.HasKey(e => e.TagId);
            entity.Property(e => e.TagName).IsRequired();
        });

        // Many-to-many relationship
        modelBuilder.Entity<MediaItem>()
            .HasMany(m => m.Tags)
           .WithMany(t => t.MediaItem);
    }
}
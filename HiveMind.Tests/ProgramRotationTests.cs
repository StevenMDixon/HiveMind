using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Endpoints.Base;
using HiveMind.Server.Entities;
using HiveMind.Server.QueryEngine;
using HiveMind.Tests;
using Microsoft.EntityFrameworkCore;


namespace HiveMind.Server.Tests.ProgramRotation;

public class ProgramRotationTests : IDisposable
{
    private readonly TestDbContext _context;

    public ProgramRotationTests()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TestDbContext(options);
        SeedTestData();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    private void SeedTestData()
    {
        var mediaItems = new List<MediaItem>
        {
            new()
            {
                Id = 1,
                Title = "Movie A",
                FilePath = "/path/movie-a.mp4",
                Duration = 5400000,
                Width = 1920,
                Height = 1080,
                LibraryId = 1,
                ShowId = 1,
                EpisodeNumber = 1,
                SeasonNumber = 1
            },
            new()
            {
                Id = 2,
                Title = "Movie B",
                FilePath = "/path/movie-b.mp4",
                Duration = 7200000,
                Width = 1920,
                Height = 1080,
                LibraryId = 1,
                ShowId = 1,
                EpisodeNumber = 2,
                SeasonNumber = 1
            },
            new()
            {
                Id = 3,
                Title = "Show Episode 1",
                FilePath = "/path/show-ep1.mp4",
                Duration = 2700000,
                Width = 1280,
                Height = 720,
                LibraryId = 2,
                ShowId = 1,
                EpisodeNumber = 3,
                SeasonNumber = 1
            },
            new()
            {
                Id = 4,
                Title = "Show Episode 2",
                FilePath = "/path/show-ep2.mp4",
                Duration = 2700000,
                Width = 1280,
                Height = 720,
                LibraryId = 2,
                ShowId = 1,
                EpisodeNumber = 4,
                SeasonNumber = 1
            },
            new()
            {
                Id = 5,
                Title = "Documentary",
                FilePath = "/path/documentary.mp4",
                Duration = 10800000,
                Width = 3840,
                Height = 2160,
                LibraryId = 1,
                ShowId = 1,
                EpisodeNumber = 5,
                SeasonNumber = 1
            }
        };

        _context.MediaItems.AddRange(mediaItems);
        
        _context.SaveChanges();
    }

    public Entities.ProgramRotation GetProgramRotation()
    {
        return new Entities.ProgramRotation()
        {
            Id = 1,
            StartDate = DateOnly.Parse("11/29/2026"),
            Recurrence = RecurrenceType.Weekly,
            Show = ""
        };
    }

    

}
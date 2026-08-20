using HiveMind.Server.Entities;
using HiveMind.Server.Tests.QueryEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiveMind.Tests;

public static class TestSeedData
{


    public static void LoadTestData(TestDbContext _context)
    {
        _context.Tags.AddRange(Tags);
        _context.Shows.AddRange(Shows);
        _context.MediaItems.AddRange(MediaItems);
        _context.MediaItems.AddRange(Commercials);
        _context.SaveChanges();
    }

    public static List<Tags> Tags = new List<Tags>
    {
        new() { TagId = 1, TagName = "In" },
        new() { TagId = 2, TagName = "Out" }
    };

    public static List<Show> Shows = new List<Show>
    {
        new()
        {
            ShowId = 1,
            ShowTitle = "Show 1"
        },
        new()
        {
            ShowId = 2,
            ShowTitle = "Show 2"
        },
        new()
        {
            ShowId = 3,
            ShowTitle = "Show 3"
        },
    };

    public static List<MediaItem> MediaItems = new List<MediaItem>
        {
            new ()
            {
                MediaItemId = 1,
                Title = "Show 1 Episode 1",
                FilePath = "/path/show-1-ep1.mp4",
                Duration = 1321000,
                Width = 1920,
                Height = 1080,
                LibraryId = 1,
                Tags = new List<Tags> { },
                ShowId = 1
            },
            new ()
            {
                MediaItemId = 2,
                Title = "Show 1 Episode 2",
                FilePath = "/path/show-1-ep2.mp4",
                Duration = 1321000,
                Width = 1920,
                Height = 1080,
                LibraryId = 1,
                Tags = new List<Tags> { }
            },
            new()
            {
                MediaItemId = 3,
                Title = "Show 1 Episode 3",
                FilePath = "/path/show-1-ep3.mp4",
                Duration = 1321000,
                Width = 1280,
                Height = 720,
                LibraryId = 2,
                Tags = new List<Tags> { }
            },
            new()
            {
                MediaItemId = 4,
                Title = "Show 2 Episode 2",
                FilePath = "/path/show-2-ep2.mp4",
                Duration = 1321000,
                Width = 1280,
                Height = 720,
                LibraryId = 2,
                Tags = new List<Tags> { }
            },
            new()
            {
                MediaItemId = 5,
                Title = "Show 2 Episode 3",
                FilePath = "/path/show-2-ep3.mp4",
                Duration = 1321000,
                Width = 3840,
                Height = 2160,
                LibraryId = 1,
                Tags = new List<Tags> { }
            },
            
        };

    public static List<MediaItem> Commercials = new List<MediaItem>
    {
        new()
            {
                MediaItemId = 6,
                Title = "Commercial 1",
                FilePath = "/path/Commercial1.mp4",
                Duration = 1321000,
                Width = 3840,
                Height = 2160,
                LibraryId = 1,
                Tags = new List<Tags> { }
            }
    };
}

using HiveMind.Server.Entities;
using HiveMind.Server.QueryEngine;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Tests.QueryEngine;

public class MediaQueryBuilderTests : IDisposable
{
    private readonly TestDbContext _context;

    public MediaQueryBuilderTests()
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
        // Create tags
        var actionTag = new Tags { TagId = 1, TagName = "Action" };
        var comedyTag = new Tags { TagId = 2, TagName = "Comedy" };
        var dramaTag = new Tags { TagId = 3, TagName = "Drama" };

        _context.Tags.AddRange(actionTag, comedyTag, dramaTag);

        var mediaItems = new List<MediaItem>
        {
            new()
            {
                MediaItemId = 1,
                Title = "Movie A",
                FilePath = "/path/movie-a.mp4",
                Duration = 5400000,
                Width = 1920,
                Height = 1080,
                LibraryId = 1,
                Tags = new List<Tags> { actionTag, dramaTag }
            },
            new()
            {
                MediaItemId = 2,
                Title = "Movie B",
                FilePath = "/path/movie-b.mp4",
                Duration = 7200000,
                Width = 1920,
                Height = 1080,
                LibraryId = 1,
                Tags = new List<Tags> { comedyTag }
            },
            new()
            {
                MediaItemId = 3,
                Title = "Show Episode 1",
                FilePath = "/path/show-ep1.mp4",
                Duration = 2700000,
                Width = 1280,
                Height = 720,
                LibraryId = 2,
                Tags = new List<Tags> { actionTag }
            },
            new()
            {
                MediaItemId = 4,
                Title = "Show Episode 2",
                FilePath = "/path/show-ep2.mp4",
                Duration = 2700000,
                Width = 1280,
                Height = 720,
                LibraryId = 2,
                Tags = new List<Tags> { actionTag, comedyTag }
            },
            new()
            {
                MediaItemId = 5,
                Title = "Documentary",
                FilePath = "/path/documentary.mp4",
                Duration = 10800000,
                Width = 3840,
                Height = 2160,
                LibraryId = 1,
                Tags = new List<Tags> { dramaTag }
            }
        };

        _context.MediaItems.AddRange(mediaItems);
        _context.SaveChanges();
    }

    #region Filter Tests

    [Fact]
    public void Apply_WithNoFilters_ReturnsAllItems()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Apply_WithEqualsFilter_FiltersCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = "Title", Operator = "equals", Value = "Movie A" }
            },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Movie A", result[0].Title);
    }

    [Fact]
    public void Apply_WithContainsFilter_FiltersCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = "Title", Operator = "contains", Value = "Show" }
            },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, item => Assert.Contains("Show", item.Title));
    }

    [Fact]
    public void Apply_WithMultipleFilters_CombinesWithAnd()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = "Title", Operator = "contains", Value = "Show" },
                new() { Field = "Title", Operator = "contains", Value = "Episode 1" }
            },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Show Episode 1", result[0].Title);
    }

    [Fact]
    public void Apply_WithInvalidField_ThrowsInvalidOperationException()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = "InvalidField", Operator = "equals", Value = "test" }
            }
        };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            MediaQueryBuilder.Apply(query, request).ToList());
        Assert.Equal("Invalid field", exception.Message);
    }

    [Fact]
    public void Apply_WithInvalidOperator_ThrowsNotSupportedException()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = "Title", Operator = "invalid", Value = "test" }
            }
        };

        // Act & Assert
        var exception = Assert.Throws<NotSupportedException>(() =>
            MediaQueryBuilder.Apply(query, request).ToList());
        Assert.Equal("Operator not supported", exception.Message);
    }

    [Fact]
    public void Apply_WithNullValue_HandlesGracefully()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = "Title", Operator = "equals", Value = null }
            },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Empty(result);
    }

    #endregion

    #region Sorting Tests

    [Fact]
    public void Apply_WithSortByAscending_SortsCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            SortBy = "Title",
            SortDescending = false,
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(5, result.Count);
        Assert.Equal("Documentary", result[0].Title);
        Assert.Equal("Show Episode 2", result[4].Title);
    }

    [Fact]
    public void Apply_WithSortByDescending_SortsCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            SortBy = "Title",
            SortDescending = true,
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(5, result.Count);
        Assert.Equal("Show Episode 2", result[0].Title);
        Assert.Equal("Documentary", result[4].Title);
    }

    [Fact]
    public void Apply_WithNullSortBy_DoesNotSort()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            SortBy = null,
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Apply_WithEmptySortBy_DoesNotSort()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            SortBy = "",
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(5, result.Count);
    }

    #endregion

    #region Pagination Tests

    [Fact]
    public void Apply_WithPagination_ReturnsCorrectPage()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Page = 1,
            PageSize = 2,
            SortBy = "MediaItemId"
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].MediaItemId);
        Assert.Equal(2, result[1].MediaItemId);
    }

    [Fact]
    public void Apply_WithSecondPage_ReturnsCorrectItems()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Page = 2,
            PageSize = 2,
            SortBy = "MediaItemId"
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(3, result[0].MediaItemId);
        Assert.Equal(4, result[1].MediaItemId);
    }

    [Fact]
    public void Apply_WithLastPartialPage_ReturnsRemainingItems()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Page = 3,
            PageSize = 2,
            SortBy = "MediaItemId"
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(5, result[0].MediaItemId);
    }

    [Fact]
    public void Apply_WithPageBeyondData_ReturnsEmpty()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Page = 10,
            PageSize = 2
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Empty(result);
    }

    [Theory]
    [InlineData(1, 10, 5)]
    [InlineData(1, 3, 3)]
    [InlineData(2, 3, 2)]
    [InlineData(3, 3, 0)]
    public void Apply_WithVariousPageSizes_ReturnsCorrectCount(int page, int pageSize, int expectedCount)
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Page = page,
            PageSize = pageSize
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(expectedCount, result.Count);
    }

    #endregion

    #region Combined Tests

    [Fact]
    public void Apply_WithFilterSortAndPagination_AppliesAllCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = "Title", Operator = "contains", Value = "Movie" }
            },
            SortBy = "Title",
            SortDescending = false,
            Page = 1,
            PageSize = 2
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Movie A", result[0].Title);
        Assert.Equal("Movie B", result[1].Title);
    }

    [Fact]
    public void Apply_WithComplexScenario_WorksCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = "Title", Operator = "contains", Value = "Show" }
            },
            SortBy = "Title",
            SortDescending = true,
            Page = 1,
            PageSize = 1
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Show Episode 2", result[0].Title);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Apply_WithEmptyQuery_ReturnsEmpty()
    {
        // Arrange
        _context.MediaItems.RemoveRange(_context.MediaItems);
        _context.SaveChanges();

        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest();

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Apply_WithDefaultRequest_UsesDefaults()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest(); // Uses defaults: Page=1, PageSize=50

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(5, result.Count); // All items fit in default page size
    }

    #endregion

    #region Tag Filter Tests
    [Fact]
    public void Apply_WithTagEqualsFilter_FiltersCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.Include(m => m.Tags).AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
        {
            new() { Field = "Tag", Operator = "equals", Value = "Action" }
        },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(3, result.Count); // Movie A, Show Episode 1, Show Episode 2
        Assert.All(result, item =>
            Assert.Contains(item.Tags, tag => tag.TagName == "Action"));
    }

    [Fact]
    public void Apply_WithTagContainsFilter_FiltersCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.Include(m => m.Tags).AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
        {
            new() { Field = "Tag", Operator = "contains", Value = "edy" } // Matches "Comedy"
        },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(2, result.Count); // Movie B, Show Episode 2
    }

    [Fact]
    public void Apply_WithMultipleTagFilters_CombinesWithAnd()
    {
        // Arrange
        var query = _context.MediaItems.Include(m => m.Tags).AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
        {
            new() { Field = "Tag", Operator = "equals", Value = "Action" },
            new() { Field = "Tag", Operator = "equals", Value = "Drama" }
        },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Single(result); // Only Movie A has both Action and Drama tags
        Assert.Equal("Movie A", result[0].Title);
    }

    [Fact]
    public void Apply_WithTagAndTitleFilter_CombinesCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.Include(m => m.Tags).AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
        {
            new() { Field = "Tag", Operator = "equals", Value = "Action" },
            new() { Field = "Title", Operator = "contains", Value = "Show" }
        },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(2, result.Count); // Show Episode 1 and Show Episode 2
    }

    #endregion
}

// Test DbContext
public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }

    public DbSet<MediaItem> MediaItems { get; set; } = null!;
    public DbSet<Tags> Tags { get; set; } = null!;

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
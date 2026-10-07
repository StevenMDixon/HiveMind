using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.QueryEngine;
using HiveMind.Tests;
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
        var actionTag = new Tags { Id = 1, Name = "Action" };
        var comedyTag = new Tags { Id = 2, Name = "Comedy" };
        var dramaTag = new Tags { Id = 3, Name = "Drama" };

        _context.Tags.AddRange(actionTag, comedyTag, dramaTag);

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
                Tags = new List<Tags> { actionTag, dramaTag }
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
                Tags = new List<Tags> { comedyTag }
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
                Tags = new List<Tags> { actionTag }
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
                Tags = new List<Tags> { actionTag, comedyTag }
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
                new() { Field = QueryEnums.QueryAllowedFields.Title, Operator = QueryEnums.QueryAllowedOperators.Equals, Value = "Movie A" }
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
                new() { Field = QueryEnums.QueryAllowedFields.Title, Operator = QueryEnums.QueryAllowedOperators.Contains, Value = "Show" }
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
                new() { Field = QueryEnums.QueryAllowedFields.Title, Operator = QueryEnums.QueryAllowedOperators.Contains, Value = "Show" },
                new() { Field = QueryEnums.QueryAllowedFields.Title, Operator = QueryEnums.QueryAllowedOperators.Contains, Value = "Episode 1" }
            },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Show Episode 1", result[0].Title);
    }

    // This test is no longer relevant - invalid fields are now caught at compile time due to enum usage
    // [Fact]
    // public void Apply_WithInvalidField_ThrowsInvalidOperationException()

    // This test is no longer relevant - invalid operators are now caught at compile time due to enum usage  
    // [Fact]
    // public void Apply_WithInvalidOperator_ThrowsNotSupportedException()

    [Fact]
    public void Apply_WithNullValue_HandlesGracefully()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = QueryEnums.QueryAllowedFields.Title, Operator = QueryEnums.QueryAllowedOperators.Equals, Value = null }
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
    public void Apply_WithSortByAscending_ReturnsAllItemsWithoutSorting()
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

        // Assert - Apply no longer sorts, just returns all items
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Apply_WithSortByDescending_ReturnsAllItemsWithoutSorting()
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

        // Assert - Apply no longer sorts, just returns all items
        Assert.Equal(5, result.Count);
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
    public void Apply_WithPagination_ReturnsAllItems()
    {
        // Arrange - Apply no longer paginates
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
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Apply_WithSecondPage_ReturnsAllItems()
    {
        // Arrange - Apply no longer paginates
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
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Apply_WithLastPartialPage_ReturnsAllItems()
    {
        // Arrange - Apply no longer paginates
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
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Apply_WithPageBeyondData_ReturnsAllItems()
    {
        // Arrange - Apply no longer paginates
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Page = 10,
            PageSize = 2
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(5, result.Count);
    }

    [Theory]
    [InlineData(1, 10, 5)]
    [InlineData(1, 3, 5)]
    [InlineData(2, 3, 5)]
    [InlineData(3, 3, 5)]
    public void Apply_WithVariousPageSizes_ReturnsCorrectCount(int page, int pageSize, int expectedCount)
    {
        // Arrange - Apply no longer paginates
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
    public void Apply_WithFilterSortAndPagination_AppliesFilterOnly()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = QueryEnums.QueryAllowedFields.Title, Operator = QueryEnums.QueryAllowedOperators.Contains, Value = "Movie" }
            },
            SortBy = "Title",
            SortDescending = false,
            Page = 1,
            PageSize = 2
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert - Only filtering is applied, no sort/pagination
        Assert.Equal(2, result.Count);
        Assert.Contains(result, item => item.Title == "Movie A");
        Assert.Contains(result, item => item.Title == "Movie B");
    }

    [Fact]
    public void Apply_WithComplexScenario_AppliesFilterOnly()
    {
        // Arrange
        var query = _context.MediaItems.AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
            {
                new() { Field = QueryEnums.QueryAllowedFields.Title, Operator = QueryEnums.QueryAllowedOperators.Contains, Value = "Show" }
            },
            SortBy = "Title",
            SortDescending = true,
            Page = 1,
            PageSize = 1
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert - Only filtering is applied, returns all matching items
        Assert.Equal(2, result.Count);
        Assert.Contains(result, item => item.Title == "Show Episode 1");
        Assert.Contains(result, item => item.Title == "Show Episode 2");
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
            new() { Field = QueryEnums.QueryAllowedFields.Tag, Operator = QueryEnums.QueryAllowedOperators.Equals, Value = "Action" }
        },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(3, result.Count); // Movie A, Show Episode 1, Show Episode 2
        Assert.All(result, item =>
            Assert.Contains(item.Tags ?? [], tag => tag.Name == "Action"));
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
            new() { Field = QueryEnums.QueryAllowedFields.Tag, Operator = QueryEnums.QueryAllowedOperators.Contains, Value = "edy" } // Matches "Comedy"
        },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(2, result.Count); // Movie B, Show Episode 2
    }

    [Fact]
    public void Apply_WithTitleInFilter_FiltersCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.Include(m => m.Tags).AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
        {
            new() { Field = QueryEnums.QueryAllowedFields.Title, Operator = QueryEnums.QueryAllowedOperators.MatchesAny, Value = "A,B" }, // Matches "Movie A" and "Movie B"
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
            new() { Field = QueryEnums.QueryAllowedFields.Tag, Operator = QueryEnums.QueryAllowedOperators.Equals, Value = "Action" },
            new() { Field = QueryEnums.QueryAllowedFields.Tag, Operator = QueryEnums.QueryAllowedOperators.Equals, Value = "Drama" }
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
            new() { Field = QueryEnums.QueryAllowedFields.Tag, Operator = QueryEnums.QueryAllowedOperators.Equals, Value = "Action" },
            new() { Field = QueryEnums.QueryAllowedFields.Title, Operator = QueryEnums.QueryAllowedOperators.Contains, Value = "Show" }
        },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(2, result.Count); // Show Episode 1 and Show Episode 2
    }

    [Fact]
    public void Apply_WithTagMatchesAnyFilter_FiltersCorrectly()
    {
        // Arrange
        var query = _context.MediaItems.Include(m => m.Tags).AsQueryable();
        var request = new QueryRequest
        {
            Filters = new List<FilterRule>
        {
            new() { Field = QueryEnums.QueryAllowedFields.Tag, Operator = QueryEnums.QueryAllowedOperators.MatchesAny, Value = "Action,Comedy" },

        },
            PageSize = 100
        };

        // Act
        var result = MediaQueryBuilder.Apply(query, request).ToList();

        // Assert
        Assert.Equal(4, result.Count); // Show Episode 1 and Show Episode 2
    }

    #endregion

    #region ApplyWithUnion Tests

    [Fact]
    public void ApplyWithUnion_WithEmptyQueries_ReturnsAllItems()
    {
        // Arrange
        var request = new QueryUnionRequest
        {
            Queries = new List<List<FilterRule>>()
        };

        // Act
        var result = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, request).ToList();

        // Assert
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void ApplyWithUnion_WithSingleQuery_ReturnsSameAsApply()
    {
        // Arrange
        var request = new QueryUnionRequest
        {
            Queries = new List<List<FilterRule>>
            {
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Equals, "Movie A")
                }
            }
        };

        // Act
        var result = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, request).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Movie A", result[0].Title);
    }

    [Fact]
    public void ApplyWithUnion_WithMultipleQueries_CombinesResults()
    {
        // Arrange
        var request = new QueryUnionRequest
        {
            Queries = new List<List<FilterRule>>
            {
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Equals, "Movie A")
                },
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Equals, "Movie B")
                }
            }
        };

        // Act
        var result = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, request).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, item => item.Title == "Movie A");
        Assert.Contains(result, item => item.Title == "Movie B");
    }

    [Fact]
    public void ApplyWithUnion_WithOverlappingQueries_DeduplicatesResults()
    {
        // Arrange - both queries match "Show Episode 1"
        var request = new QueryUnionRequest
        {
            Queries = new List<List<FilterRule>>
            {
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Contains, "Show")
                },
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Equals, "Show Episode 1")
                }
            }
        };

        // Act
        var result = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, request).ToList();

        // Assert - Union should deduplicate overlapping results
        Assert.Equal(2, result.Count);
        Assert.Contains(result, item => item.Title == "Show Episode 1");
        Assert.Contains(result, item => item.Title == "Show Episode 2");
    }

    [Fact]
    public void ApplyWithUnion_WithMultipleQueries_ReturnsAllMatches()
    {
        // Arrange
        var request = new QueryUnionRequest
        {
            Queries = new List<List<FilterRule>>
            {
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Contains, "Movie")
                },
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Contains, "Show")
                }
            }
        };

        // Act
        var result = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, request).ToList();

        // Assert - All Movies (2) + All Shows (2) = 4
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void ApplyWithUnion_WithDifferentFieldFilters_CombinesCorrectly()
    {
        // Arrange - one query filters by LibraryId, another by Title
        var request = new QueryUnionRequest
        {
            Queries = new List<List<FilterRule>>
            {
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.LibraryId, QueryEnums.QueryAllowedOperators.Equals, "2")
                },
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Equals, "Documentary")
                }
            }
        };

        // Act
        var result = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, request).ToList();

        // Assert - LibraryId=2 gives Show Episode 1 & 2, plus Documentary
        Assert.Equal(3, result.Count);
        Assert.Contains(result, item => item.Title == "Show Episode 1");
        Assert.Contains(result, item => item.Title == "Show Episode 2");
        Assert.Contains(result, item => item.Title == "Documentary");
    }

    [Fact]
    public void ApplyWithUnion_WithMultipleFiltersPerQuery_AppliesAndWithinEachQuery()
    {
        // Arrange - each filter set uses AND logic internally
        var request = new QueryUnionRequest
        {
            Queries = new List<List<FilterRule>>
            {
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Contains, "Movie"),
                    new FilterRule(QueryEnums.QueryAllowedFields.Duration, QueryEnums.QueryAllowedOperators.GreaterThan, "6000000")
                },
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Equals, "Show Episode 1")
                }
            }
        };

        // Act
        var result = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, request).ToList();

        // Assert - "Movie B" (duration 7200000 > 6000000) plus "Show Episode 1"
        Assert.Equal(2, result.Count);
        Assert.Contains(result, item => item.Title == "Movie B");
        Assert.Contains(result, item => item.Title == "Show Episode 1");
    }

    [Fact]
    public void ApplyWithUnion_WithNoMatchingResults_ReturnsEmpty()
    {
        // Arrange
        var request = new QueryUnionRequest
        {
            Queries = new List<List<FilterRule>>
            {
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Equals, "Nonexistent")
                },
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Title, QueryEnums.QueryAllowedOperators.Equals, "Also Nonexistent")
                }
            }
        };

        // Act
        var result = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, request).ToList();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ApplyWithUnion_WithTagFilters_CombinesTagQueries()
    {
        // Arrange
        var request = new QueryUnionRequest
        {
            Queries = new List<List<FilterRule>>
            {
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Tag, QueryEnums.QueryAllowedOperators.Equals, "Comedy")
                },
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.Tag, QueryEnums.QueryAllowedOperators.Equals, "Drama")
                }
            }
        };

        // Act
        var result = MediaQueryBuilder.ApplyWithUnion(
            _context.MediaItems.Include(m => m.Tags) as DbSet<MediaItem> ?? _context.MediaItems,
            request).ToList();

        // Assert - Comedy: Movie B, Show Episode 2; Drama: Movie A, Documentary
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void ApplyWithUnion_WithAllLibraries_ReturnsAllItems()
    {
        // Arrange - Union of both libraries covers all items
        var request = new QueryUnionRequest
        {
            Queries = new List<List<FilterRule>>
            {
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.LibraryId, QueryEnums.QueryAllowedOperators.Equals, "1")
                },
                new()
                {
                    new FilterRule(QueryEnums.QueryAllowedFields.LibraryId, QueryEnums.QueryAllowedOperators.Equals, "2")
                }
            }
        };

        // Act
        var result = MediaQueryBuilder.ApplyWithUnion(_context.MediaItems, request).ToList();

        // Assert - LibraryId=1 has 3 items, LibraryId=2 has 2 items, union = 5
        Assert.Equal(5, result.Count);
    }

    #endregion
}

// Test DbContext

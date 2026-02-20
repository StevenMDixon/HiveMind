using System.Linq.Expressions;
using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.QueryEngine;

public class QueryRequest
{
    public List<FilterRule> Filters { get; set; } = new();
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

public class FilterRule
{
    public string Field { get; set; } = "";
    public string Operator { get; set; } = "";
    public string? Value { get; set; }
}

public static class MediaQueryBuilder
{
    private static readonly HashSet<string> AllowedFields =
    [
        "Title",
        "Duration",
        "Width",
        "Height",
        "LibraryId"
    ];

    public static IQueryable<MediaItem> Apply(
        IQueryable<MediaItem> query,
        QueryRequest request)
    {
        if (request.Filters.Any())
        {
            var parameter = Expression.Parameter(typeof(MediaItem), "m");
            Expression? combined = null;

            foreach (var filter in request.Filters)
            {
                Expression comparison;

                // Special handling for tag filtering
                if (filter.Field.Equals("Tag", StringComparison.OrdinalIgnoreCase))
                {
                    comparison = BuildTagFilter(parameter, filter);
                }
                else
                {
                    if (!AllowedFields.Contains(filter.Field))
                        throw new InvalidOperationException("Invalid field");

                    var property = Expression.Property(parameter, filter.Field);
                    var constant = Expression.Constant(filter.Value);

                    comparison = filter.Operator switch
                    {
                        "equals" => Expression.Equal(property, constant),
                        "contains" when property.Type == typeof(string) =>
                            Expression.Call(
                                property,
                                typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
                                constant),
                        _ => throw new NotSupportedException("Operator not supported")
                    };
                }

                combined = combined == null
                    ? comparison
                    : Expression.AndAlso(combined, comparison);
            }

            if (combined != null)
            {
                var lambda = Expression.Lambda<Func<MediaItem, bool>>(combined, parameter);
                query = query.Where(lambda);
            }
        }

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            query = request.SortDescending
                ? query.OrderByDescending(e => EF.Property<object>(e, request.SortBy))
                : query.OrderBy(e => EF.Property<object>(e, request.SortBy));
        }

        return query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);
    }

    private static Expression BuildTagFilter(ParameterExpression parameter, FilterRule filter)
    {
        // Get the Tags collection property
        var tagsProperty = Expression.Property(parameter, "Tags");
        
        // Create lambda for tag filtering: tag => tag.TagName.Contains(value)
        var tagParam = Expression.Parameter(typeof(Tags), "tag");
        var tagNameProperty = Expression.Property(tagParam, "TagName");
        var constant = Expression.Constant(filter.Value);

        Expression tagComparison = filter.Operator switch
        {
            "equals" => Expression.Equal(tagNameProperty, constant),
            "contains" => Expression.Call(
                tagNameProperty,
                typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
                constant),
            _ => throw new NotSupportedException("Operator not supported for tags")
        };

        var tagLambda = Expression.Lambda<Func<Tags, bool>>(tagComparison, tagParam);

        // Build: m.Tags.Any(tag => tag.TagName.Contains(value))
        var anyMethod = typeof(Enumerable)
            .GetMethods()
            .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(Tags));

        return Expression.Call(anyMethod, tagsProperty, tagLambda);
    }
}

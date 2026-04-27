using System.Linq.Expressions;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.QueryEngine;

public class QueryRequest
{
    public List<FilterRule> Filters { get; set; } = new();
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

public class FilterRule
{
    public FilterRule()
    {
    }

    public FilterRule(QueryEnums.QueryAllowedFields field, QueryEnums.QueryAllowedOperators @operator, string? value)
    {
        Field = field;
        Operator = @operator;
        Value = value;
    }

    public QueryEnums.QueryAllowedFields Field { get; set; }
    public QueryEnums.QueryAllowedOperators Operator { get; set; }
    public string? Value { get; set; }
}

public static class MediaQueryBuilder
{
    private static object? ConvertValueToType(string? value, Type targetType)
    {
        if (value == null)
            return null;

        // Handle nullable types
        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (underlyingType == typeof(string))
            return value;

        try
        {
            return Convert.ChangeType(value, underlyingType);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"Cannot convert value '{value}' to type {targetType.Name}", ex);
        }
    }

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
                if (filter.Field == QueryEnums.QueryAllowedFields.Tag)
                {
                    comparison = BuildTagFilter(parameter, filter);
                }
                else if (filter.Field == QueryEnums.QueryAllowedFields.Show)
                {
                    comparison = BuildShowFilter(parameter, filter);
                }
                else
                {
                    var property = Expression.Property(parameter, filter.Field.ToString());
                    var convertedValue = ConvertValueToType(filter.Value, property.Type);
                    var constant = Expression.Constant(convertedValue, property.Type);

                    comparison = filter.Operator switch
                    {
                        QueryEnums.QueryAllowedOperators.Equals => Expression.Equal(property, constant),
                        QueryEnums.QueryAllowedOperators.NotEquals => Expression.NotEqual(property, constant),
                        QueryEnums.QueryAllowedOperators.GreaterThan => Expression.GreaterThan(property, constant),
                        QueryEnums.QueryAllowedOperators.LessThan => Expression.LessThan(property, constant),
                        QueryEnums.QueryAllowedOperators.Contains when property.Type == typeof(string) =>
                            Expression.Call(
                                property,
                                typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
                                constant),
                        QueryEnums.QueryAllowedOperators.MatchesAny when property.Type == typeof(string) => BuildInFilter(property, filter),
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

    private static Expression BuildInFilter(MemberExpression property, FilterRule filter)
    {
        //var property = Expression.Property(parameter, filter.Field);
        var values = filter.Value?.Split(',').Select(v => v.Trim()).ToArray() ?? Array.Empty<string>();
        
        if (values.Length == 0)
            return Expression.Constant(false); // No values means nothing matches
        
        Expression? combined = null;
        var stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        
        // Build: property.Contains(value1) OR property.Contains(value2) OR ...
        foreach (var value in values)
        {
            var constant = Expression.Constant(value);
            var containsCall = Expression.Call(property, stringContainsMethod, constant);
            
            combined = combined == null
                ? containsCall
                : Expression.OrElse(combined, containsCall);
        }
        
        return combined ?? Expression.Constant(false);
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
            QueryEnums.QueryAllowedOperators.Equals => Expression.Equal(tagNameProperty, constant),
            QueryEnums.QueryAllowedOperators.NotEquals => Expression.NotEqual(tagNameProperty, constant),
            QueryEnums.QueryAllowedOperators.MatchesAny when tagNameProperty.Type == typeof(string) => BuildInFilter(tagNameProperty, filter),
            QueryEnums.QueryAllowedOperators.Contains => Expression.Call(
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

    private static Expression BuildShowFilter(ParameterExpression parameter, FilterRule filter)
    {
        // Get the Show navigation property (single object, not a collection)
        var showProperty = Expression.Property(parameter, "Show");

        // Get the MediaItemShowTitle property from Show
        var showNameProperty = Expression.Property(showProperty, "MediaItemShowTitle");
        var constant = Expression.Constant(filter.Value);

        Expression showComparison = filter.Operator switch
        {
            QueryEnums.QueryAllowedOperators.Equals => Expression.Equal(showNameProperty, constant),
            QueryEnums.QueryAllowedOperators.NotEquals => Expression.NotEqual(showNameProperty, constant),
            QueryEnums.QueryAllowedOperators.Contains => Expression.Call(
                showNameProperty,
                typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
                constant),
            _ => throw new NotSupportedException("Operator not supported for shows")
        };

        // Add null check for Show property since it's nullable
        var showNotNull = Expression.NotEqual(showProperty, Expression.Constant(null, typeof(Show)));

        // Combine null check with the comparison: Show != null && Show.MediaItemShowTitle [operator] value
        return Expression.AndAlso(showNotNull, showComparison);
    }
}

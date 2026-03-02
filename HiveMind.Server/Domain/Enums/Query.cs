namespace HiveMind.Server.Domain.Enums;

public static class QueryEnums
{
    public enum QueryAllowedFields
    {
        Title,
        Duration,
        Width,
        Height,
        LibraryId,
        Tag
    }

    public static string GetType(QueryAllowedFields field)
    {
        return field switch
        {
            QueryAllowedFields.Title => "string",
            QueryAllowedFields.Duration => "int",
            QueryAllowedFields.Width => "int",
            QueryAllowedFields.Height => "int",
            QueryAllowedFields.LibraryId => "int",
            QueryAllowedFields.Tag => "string",
            _ => throw new ArgumentOutOfRangeException(nameof(field), field, null)
        };
    }

    public enum QueryAllowedOperators
    {
        Equals, //Any
        NotEquals, //Any
        GreaterThan, //int
        LessThan, //int
        Contains, //Any
        MatchesAny //Any
    }

    public static bool IsOperatorValidForType(QueryAllowedOperators op, string type)
    {
        return op switch
        {
            QueryAllowedOperators.Equals => true,
            QueryAllowedOperators.NotEquals => true,
            QueryAllowedOperators.GreaterThan => type == "int",
            QueryAllowedOperators.LessThan => type == "int",
            QueryAllowedOperators.Contains => true,
            QueryAllowedOperators.MatchesAny => true,
            _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
        };
    }

    public record QueryOptions(string name, string type, string options);

    public static List<QueryOptions> GetAllowedOptions()
    {
        var options = new List<QueryOptions>();
        foreach (var field in Enum.GetValues<QueryAllowedFields>())
        {
            var type = GetType(field);
            var allowedOps = Enum.GetValues<QueryAllowedOperators>()
                .Where(op => IsOperatorValidForType(op, type))
                .Select(op => op.ToString())
                .ToArray();
            options.Add(new QueryOptions(field.ToString(), type, string.Join(", ", allowedOps)));
        }
        return options;
    }
}


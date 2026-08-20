using System.ComponentModel.DataAnnotations;
using static HiveMind.Server.Domain.Enums.QueryEnums;

namespace HiveMind.Server.Entities;

public class QueryFilters
{
    [Key]
    public int QueryFilterId { get; set; }
    public QueryAllowedFields Field { get; set; }
    public QueryAllowedOperators Operator { get; set; }
    public string Value { get; set; } = string.Empty;
    public int QueryId { get; set; }
    public Query? Query { get; set; }
}
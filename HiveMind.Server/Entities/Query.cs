using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Entities;

public class Query
{
    public int QueryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public QueryType QueryType { get; set; } = QueryType.None;
    public ICollection<QueryFilters>? Filters { get; set; }
}

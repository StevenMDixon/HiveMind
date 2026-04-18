namespace HiveMind.Server.Entities;

public class Query
{
    public int QueryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<QueryFilters>? Filters { get; set; }
}

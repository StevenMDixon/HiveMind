namespace HiveMind.Server.Entities;

public class QueryFilters
{
    public int QueryFilterId { get; set; }
    public string Field { get; set; } = string.Empty;
    public string Operator { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int CollectionId { get; set; }
    public Collection? Collection { get; set; }
}

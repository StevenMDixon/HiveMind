namespace HiveMind.Server.Entities;

public class Collection
{
    public int CollectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<QueryFilters>? Filters { get; set; }
}

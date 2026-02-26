namespace HiveMind.Server.Entities;

public class Query
{
    public int QueryId { get; set; }
    public int CollectionId { get; set; }
    public Collection? Collection { get; set; }
    public ICollection<QueryFilters>? Filters { get; set; }
}

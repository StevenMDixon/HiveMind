using System.ComponentModel.DataAnnotations;

namespace HiveMind.Server.Entities;

public class QueryFilters
{
    [Key]
    public int QueryFilterId { get; set; }
    public string Field { get; set; } = string.Empty;
    public string Operator { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int QueryId { get; set; }
    public Query? Query { get; set; }
}

//library_id:9 AND(title:199? or title:200?) AND NOT tag:holidays

// where library_id = 9 and (title like '199%' or title like '200%') and not exists (select 1 from media_tags mt where mt.media_id = m.media_id and mt.tag = 'holidays')
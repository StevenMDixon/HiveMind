
namespace HiveMind.Server.Entities;

public class LineupItem
{
    public int LineupItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Index { get; set; }
    public string Type { get; set; } = string.Empty; // Block or Generic
    public ICollection<QueryLineupItem>? Queries { get; set; }
    public int LineupId { get; set; }
    public Lineup? Lineup { get; set; }
}

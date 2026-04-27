using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Entities;

public class QueryLineupItem
{
    public int QueryLineupItemId { get; set; }
    public int QueryId { get; set; } 
    public int LineupItemId { get; set; }
    public int PlayDuration { get; set; } // Duration in milliseconds, we will need to this specificy each item length
    public int PlayCount { get; set; } // how many episodes to put into the block -> 0 = as many as possible. episodes are discarded to fit into PlayDuration
    public int PadTo { get; set; } // Duration in milliseconds pad each episode to. 5, 10, 15, 30
    public QueryType QueryType { get; set; } // e.g., "Opening", "Closing", "Commercials", "MediaItem", 
    public PlayoutType PlayoutType { get; set; } // e.g., "Sequential", "Random", "Shuffle".
    public int Index { get; set; } // Index inside of LineupItem
}

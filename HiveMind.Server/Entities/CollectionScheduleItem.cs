using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Entities;

public class CollectionScheduleItem
{
    public int CollectionScheduleItemId { get; set; }
    public int CollectionId { get; set; } 
    public int ScheduleItemId { get; set; }
    public int Duration { get; set; } // Duration in milliseconds, we will need to this specificy each item length
    public CollectionType CollectionType { get; set; } // e.g., "Opening", "Closing", "Commercials", "MediaItem", 
    public PlayoutType PlayoutType { get; set; } // e.g., "Sequential", "Random", "Shuffle".
    public int Index { get; set; } // Index inside of ScheduleItem
    public bool DisableIntroBump { get; set; } // Remove pre-roll intro media, prevents a intro then bump for blocks
    public bool DisableInterStitials { get; set; } // Remove interstitial media, as some media items may have them built in
}

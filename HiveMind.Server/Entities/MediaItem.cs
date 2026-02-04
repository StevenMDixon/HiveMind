namespace HiveMind.Server.Entities;

public class MediaItem
{
    public int MediaItemId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int Duration { get; set; } // Duration in milliseconds
    public int LibraryId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public ICollection<Tags>? Tags { get; set; }
}

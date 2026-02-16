namespace HiveMind.Server.Entities;

public class Library
{
    public int LibraryId { get; set; }
    public string LibraryName { get; set; } = string.Empty;
    public string LibraryPath { get; set; } = string.Empty;
    public bool IsProcessed { get; set; } = false;
    //public ICollection<MediaItem> MediaItems { get; set; } = new List<MediaItem>();
}

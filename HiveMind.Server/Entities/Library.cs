using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Entities;

public class Library
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string PathsToIgnore { get; set; } = string.Empty;
    public LibraryType Type { get; set; } = LibraryType.Other;
    public bool IsProcessed { get; set; } = false;
    public ICollection<MediaItem> MediaItems { get; set; } = new List<MediaItem>();
}

using HiveMind.Server.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HiveMind.Server.Entities;

public class MediaItem
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int Duration { get; set; } // Duration in milliseconds
    public int LibraryId { get; set; }
    public Library? Library { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
    public string Resolution { get; set; } = string.Empty;
    public bool HasBlackBars { get; set; } = false;
    public int EpisodeNumber { get; set; } = 0;
    public int SeasonNumber { get; set; } = 0;
    public string? Group { get; set; }
    public int? ShowId { get; set; }
    public Show? Show { get; set; }
    public ICollection<Tags>? Tags { get; set; }
}

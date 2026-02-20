using System.ComponentModel.DataAnnotations;

namespace HiveMind.Server.Entities;

public class MediaItem
{
    [Key]
    public int MediaItemId { get; set; }
    public string Title { get; set; } = "";
    public double Duration { get; set; } // Duration in milliseconds
    public int LibraryId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
    public string Resolution { get; set; } = string.Empty;
    public int EpisodeNumber { get; set; } = 0;
    public int SeasonNumber { get; set; } = 0;
    public int? MediaItemShowId { get; set; }
    public MediaItemShow? Show { get; set; }
    public ICollection<Tags>? Tags { get; set; }
}

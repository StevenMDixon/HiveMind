using HiveMind.Server.Entities;         

namespace HiveMind.Server.Domain.Importer;

public class VideoMeta
{
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FullPath { get; set; } = string.Empty;
    public double Duration { get; set; } = 0;
    public int Height { get; set; } = 0;
    public int Width { get; set; } = 0;
    public string Resolution { get; set; } = string.Empty;
    public bool HasBlackBars { get; set; } = false;
    public int? ShowId { get; set; } = null;
    public int SeasonNumber { get; set; } = 0;
    public int EpisodeNumber { get; set; } = 0;
    public ICollection<Tags> Tags { get; set; } = new List<Tags>();
}

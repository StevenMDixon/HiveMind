namespace HiveMind.Server.Entities;

public class Tags
{
    public int TagId { get; set; }
    public string TagName { get; set; } = string.Empty;

    public ICollection<MediaItem>? MediaItems { get; set; }
}

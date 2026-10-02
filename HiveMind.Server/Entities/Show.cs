using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Entities;

public class Show
{
    public int ShowId { get; set; }
    public string ShowTitle { get; set; } = string.Empty;
    public Ratings Rating { get; set; } = Ratings.None;
}

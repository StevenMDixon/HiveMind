using HiveMind.Server.Domain.Enums;

namespace HiveMind.Server.Entities;

public class Show
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Ratings Rating { get; set; } = Ratings.None;
}

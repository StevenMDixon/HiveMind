namespace HiveMind.Server.Entities;

public class Channel
{
    public int ChannelId { get; set; }
    public int ChannelNumber { get; set; }
    public string ChannelName { get; set; } = string.Empty;
    public Lineup? Lineup { get; set; }
}

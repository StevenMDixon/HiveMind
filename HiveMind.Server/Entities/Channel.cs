using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiveMind.Server.Entities;

public class Channel
{
    public int ChannelId { get; set; }
    public string ChannelName { get; set; } = string.Empty;
    public Schedule? Schedule { get; set; }
}

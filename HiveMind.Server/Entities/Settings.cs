namespace HiveMind.Server.Entities;

public class Settings
{
    public int SettingsId { get; set; }
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
}

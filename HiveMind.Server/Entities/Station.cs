namespace HiveMind.Server.Entities;

public class Station
{
    public int StationId { get; set; }
    public int StationNumber { get; set; }
    public string StationName { get; set; } = string.Empty;
    public string StationLogo { get; set; } = string.Empty;
    public ProgramStrategy? Strategy { get; set; }
}

namespace HiveMind.Server.Entities;

public class Station
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public int? StrategyId { get; set; }
    public ProgramStrategy? Strategy { get; set; }
    public int? DroneId { get; set; }
}

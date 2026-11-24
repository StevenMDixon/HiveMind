namespace HiveMind.Server.Domain.Enums;

public enum LineupSelectionType
{
    [Priority(1)]
    Any = 0,
    [Priority(2)]
    Weekday,
    [Priority(3)]
    Weekend, 
    [Priority(4)]
    DayOfWeek, // mon, tues
    [Priority(5)]
    Month, // 10
    [Priority(6)]
    MonthAndDay, // 10/04 
    [Priority(7)]
    Date // 10/04/25
}

[AttributeUsage(AttributeTargets.Field)]
public class Priority : Attribute
{
    public int _Priority { get; }

    public Priority(int priority)
    {
        _Priority = priority;
    }
}
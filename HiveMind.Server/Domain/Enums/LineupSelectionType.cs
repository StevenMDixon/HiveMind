namespace HiveMind.Server.Domain.Enums;

public enum LineupSelectionType
{
    [Priority(int.MaxValue)]
    Any = 0,
    [Priority(3)]
    DayOfWeek,
    [Priority(2)]
    Month,
    [Priority(1)]
    MonthAndDay,
    [Priority(0)]
    Date,

    NthWeekday,
    DateRange, 
    MonthDayRange,
    MonthRange,
    DayOfWeekRange,
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
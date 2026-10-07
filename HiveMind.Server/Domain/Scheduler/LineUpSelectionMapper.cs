using HiveMind.Server.Entities;
using HiveMind.Server.Domain.Enums;
using System.Reflection;


namespace HiveMind.Server.Domain.Scheduler;

public static class LineUpSelectionMapper
{
    private class DateMetaData(DateOnly date)
    {
        public DateOnly Date { get; } = date;
        public DayOfWeek DayOfWeek { get; } = date.DayOfWeek;
        public int DayOfMonth { get; } = date.Day;
        public int Month { get; } = date.Month;
        public int Year { get; } = date.Year;
        public string MonthAndDay => $"{date.Month}/{date.Day}";
    }

    private record PrioritizedLineUp(ProgramStrategyLineup Lineup, int Priority);

    private static int GetSelectionTypePriority(LineupSelectionType selectionType)
    {
        var member = typeof(LineupSelectionType).GetMember(selectionType.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<Priority>()?._Priority ?? 0;
    }

    // used to map 
    public static int? GetMatchingProgramStrategyLineup(ProgramStrategy programStrategy, DateOnly date)
    {
        if(programStrategy.Lineups == null || !programStrategy.Lineups.Any())
        {
            return null;
        }

        // each Lineup in program strategy has a SelectionType and selection option we need to use the passed date as a key to find the correct lineup for this date
        var dateMeta = new DateMetaData(date);

        var matches = new List<PrioritizedLineUp>();

        matches.AddRange(programStrategy.Lineups.Select(lineup =>
        {
            int priority = lineup.SelectionType switch
            {
                LineupSelectionType.Date => lineup.SelectionOption == dateMeta.Date.ToString("yyyy-MM-dd") ? GetSelectionTypePriority(LineupSelectionType.Date) : 0,
                LineupSelectionType.Month => lineup.SelectionOption == dateMeta.Month.ToString() ? GetSelectionTypePriority(LineupSelectionType.Month) : 0,
                LineupSelectionType.DayOfWeek => lineup.SelectionOption == dateMeta.DayOfWeek.ToString() ? GetSelectionTypePriority(LineupSelectionType.DayOfWeek) : 0,
                LineupSelectionType.MonthAndDay => lineup.SelectionOption == dateMeta.MonthAndDay ? GetSelectionTypePriority(LineupSelectionType.MonthAndDay) : 0,
                LineupSelectionType.Any => GetSelectionTypePriority(LineupSelectionType.Any),
                _ => 0
            };
            return new PrioritizedLineUp(lineup, priority);
        }).Where(plu => plu.Priority > 0));

        return matches.OrderByDescending(plu => plu.Priority).FirstOrDefault()?.Lineup.Id;
    }
}

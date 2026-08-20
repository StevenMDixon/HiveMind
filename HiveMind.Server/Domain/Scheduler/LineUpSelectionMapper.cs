using HiveMind.Server.Entities;
using HiveMind.Server.Domain.Enums;


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
    }

    private record PrioritizedLineUp(ProgramStrategyLineup Lineup, int Priority);

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
                LineupSelectionType.Date => lineup.SelectionOption == dateMeta.Date.ToString("yyyy-MM-dd") ? 1 : 0,
                LineupSelectionType.Month => lineup.SelectionOption == dateMeta.Month.ToString() ? 2 : 0,
                LineupSelectionType.DayOfWeek => lineup.SelectionOption == dateMeta.DayOfWeek.ToString() ? 3 : 0,
                LineupSelectionType.Any => 4,
                _ => 0
            };
            return new PrioritizedLineUp(lineup, priority);
        }).Where(plu => plu.Priority > 0));

        return matches.OrderBy(plu => plu.Priority).FirstOrDefault()?.Lineup.ProgramStrategyLineupId;
    }
}

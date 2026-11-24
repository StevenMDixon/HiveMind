using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class ProgramEventService(SqliteDBContext context) : BaseService<ProgramEvent>(context)
{
    public IEnumerable<ProgramEvent> GetUpComingEvents(int programStrategyID, DateTime currentDate)
    {
        var today = DateOnly.FromDateTime(currentDate);
        var results = new List<ProgramEvent>();
        var upcoming = Get().Where(pe => pe.ProgramStrategyId == programStrategyID && pe.EventDate >= today);

        foreach(var programEvent in upcoming)
        {
            var startDate = programEvent.EventDate.AddDays(-programEvent.PromoDays);
            var endDate = programEvent.EventDate.AddDays(-1);

            if(today >= startDate && today <= endDate) results.Add(programEvent);
        }

        return results;
    }

    public ProgramEvent? GetProgramEventByID(int Id)
    {
        return Get().FirstOrDefault(x => x.Id == Id);
    }

    public IEnumerable<ProgramEvent> GetProgramEventByProgramId(int Id)
    {
        return Get().Where(x => x.ProgramStrategyId == Id);
    }
}

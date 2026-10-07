using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class ProgramEventService(SqliteDBContext context) : BaseService<ProgramEvent>(context)
{

    public IEnumerable<ProgramEvent> GetAllProgramEvents()
    {
        return Get().Include(pe => pe.ProgramStrategy).Include(pe => pe.Query);
    }


    public IEnumerable<ProgramEvent> GetUpComingEvents(int programStrategyID, DateTime currentDate)
    {
        var results = new List<ProgramEvent>();
        var upcoming = Get().Where(pe => pe.ProgramStrategyId == programStrategyID && pe.EventDate >= currentDate);

        foreach(var programEvent in upcoming)
        {
            var startDate = programEvent.EventDate.AddDays(-programEvent.PromoDays);
            var endDate = programEvent.EventDate.AddDays(-1);

            if(currentDate >= startDate && currentDate <= endDate) results.Add(programEvent);
        }

        return results;
    }

    public void AddProgramEvent(ProgramEvent programEvent)
    {
        Create(programEvent);
    }

    public void AddProgramEvent(IEnumerable<ProgramEvent> programEvents)
    {
        foreach (var programEvent in programEvents)
        {
            Create(programEvent);
        }
    }

    public ProgramEvent? GetProgramEventByID(int Id)
    {
        return Get().FirstOrDefault(x => x.Id == Id);
    }

    public IEnumerable<ProgramEvent> GetProgramEventByProgramId(int Id)
    {
        return Get().Where(x => x.ProgramStrategyId == Id);
    }

    public void Delete(int Id)
    {
        var programEvent = Get().FirstOrDefault(x => x.Id == Id);
        if (programEvent != null)
        {
            Delete(programEvent);
        }
    }

    public void DeleteMany(IEnumerable<ProgramEvent> programEvents)
    {
        Delete(programEvents.ToList());
    }
}

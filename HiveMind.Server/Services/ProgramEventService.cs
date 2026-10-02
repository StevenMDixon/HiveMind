using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class ProgramEventService: BaseService
{
    public ProgramEventService(sqliteDBContext context) : base(context) { }

    public IEnumerable<ProgramEvent> GetAllProgramEvents()
    {
        return _context.ProgramEvents.Include(pe => pe.ProgramStrategy).Include(pe => pe.Query);
    }


    public IEnumerable<ProgramEvent> GetUpComingEvents(int programStrategyID, DateTime currentDate)
    {
        var results = new List<ProgramEvent>();
        var upcoming = _context.ProgramEvents.Where(pe => pe.ProgramStrategyId == programStrategyID && pe.EventDate >= currentDate);

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
        _context.ProgramEvents.Add(programEvent);
        _context.SaveChanges();
    }

    public void AddProgramEvent(IEnumerable<ProgramEvent> programEvents)
    {
        _context.ProgramEvents.AddRange(programEvents);
        _context.SaveChanges();
    }

    public ProgramEvent? GetProgramEventByID(int id)
    {
        return _context.ProgramEvents.Find(id);
    }

    public IEnumerable<ProgramEvent> GetProgramEventByProgramId(int id)
    {
        return _context.ProgramEvents.Where(x => x.ProgramStrategyId == id);
    }

    public void Update(ProgramEvent programEvent)
    {
        _context.ProgramEvents.Update(programEvent);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var programEvent = _context.ProgramEvents.Find(id);
        if (programEvent != null)
        {
            _context.ProgramEvents.Remove(programEvent);
            _context.SaveChanges();
        }
    }

    public void DeleteMany(IEnumerable<ProgramEvent> programEvent)
    {
        _context.ProgramEvents.RemoveRange(programEvent);
        _context.SaveChanges();
    }
}

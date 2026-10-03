using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class ScheduleService : BaseService
{
    public ScheduleService(SqliteDBContext context) : base(context) { }

    public void Create(SchedulingResult item)
    {
        _context.SchedulingResults.Add(item);
        _context.SaveChanges();
    }

    public void Delete(SchedulingResult item)
    {
        _context.SchedulingResults.Remove(item);
        _context.SaveChanges();
    }

    public List<SchedulingResult> GetSchedulingResults(DateOnly date)
    {
        return _context.SchedulingResults.Where(sr => sr.Date <= date).ToList();
    }
}

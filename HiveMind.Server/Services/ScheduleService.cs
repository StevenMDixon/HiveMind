using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class ScheduleService : BaseService
{
    public ScheduleService(sqliteDBContext context) : base(context) { }

    public void Create(SchedulingResult item)
    {
        _context.SchedulingResults.Add(item);
        _context.SaveChanges();
    }
}

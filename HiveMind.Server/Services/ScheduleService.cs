using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class ScheduleService:BaseService
{
    public ScheduleService(sqliteDBContext context) : base(context) { }

    public IEnumerable<Schedule> GetAllSchedules()
    {
        return _context.Schedules;
    }

    public void AddSchedule(Schedule schedule)
    {
        _context.Schedules.Add(schedule);
        _context.SaveChanges();
    }

    public Schedule? GetScheduleByID(int id)
    {
        return _context.Schedules
            .Include(c => c.ScheduleItems)
            .FirstOrDefault(x => x.ScheduleId == id);
    }

    public void Update(Schedule schedule)
    {
        _context.Schedules.Update(schedule);
        _context.SaveChanges();
    }

    public void DeleteSchedule(int id)
    {
        var schedule = _context.Schedules.Find(id);
        if (schedule != null)
        {
            _context.Schedules.Remove(schedule);
            _context.SaveChanges();
        }
    }
}

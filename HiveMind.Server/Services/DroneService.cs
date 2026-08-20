using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class DroneService: BaseService
{
    public DroneService(sqliteDBContext context) : base(context) { }

    public List<Drone> GetAll()
    {
        return _context.Drones.ToList();
    }

    public Drone? GetByHostName(string hostName)
    {
        var drone = _context.Drones.Where(x => x.HostName == hostName).Include(x => x.Stations).FirstOrDefault();

        return drone;
    }

    public void Create(string name, string hostName)
    {
        _context.Drones.Add(new Drone() { Name = name, HostName = hostName });
        _context.SaveChanges();
    }

    public List<(string, SchedulingResult)> GetDroneSchedule(int droneId)
    {
        var drone = _context.Drones.Where(x => x.DroneId == droneId).Include(x => x.Stations).ThenInclude(x => x.Strategy).FirstOrDefault();
        var currentDateOnly = DateOnly.FromDateTime(DateTime.Today);

        if (drone != null)
        {
            var schedules = new List<(string, SchedulingResult)>();

            foreach(var station in drone.Stations)
            {
                var currentStrategy = station.Strategy;

                if(currentStrategy != null)
                {
                    var schedulingResult = _context.SchedulingResults.Where(x => x.ProgramStrategyId == currentStrategy.ProgramStrategyId && x.Date == currentDateOnly).FirstOrDefault();

                    if(schedulingResult != null)
                    {
                        schedules.Add((station.StationNumber.ToString(), schedulingResult));
                    }
                }
            }
            return schedules;
        }

        return new List<(string, SchedulingResult)>();
    }
}

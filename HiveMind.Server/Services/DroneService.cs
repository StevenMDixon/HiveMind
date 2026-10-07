using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class DroneService(SqliteDBContext context) : BaseService<Drone>(context)
{
    public List<Drone> GetAll()
    {
        return Get().Include(x => x.Stations).IgnoreAutoIncludes().ToList();
    }

    public Drone? GetByHostName(string hostName)
    {
        var drone = Get(x => x.Stations).FirstOrDefault(x => x.HostName == hostName);

        return drone;
    }

    public Drone? GetById(int Id)
    {
        var drone = Get(x => x.Stations).FirstOrDefault(x => x.Id == Id);
        return drone;
    }

    public void Create(string name, string hostName, int stationSlots)
    {
        _context.Drones.Add(new Drone() { Name = name, HostName = hostName, StationSlots = stationSlots });
        _context.SaveChanges();
    }

    public void Delete(int Id)
    {
        var drone = Get().FirstOrDefault(x => x.Id == Id);
        if (drone != null)
        {
            Delete(drone);
        }
    }

    public List<(string, string)> GetDroneSchedule(int droneId, DateOnly date)
    {
        var drone = _context.Drones.Where(x => x.Id == droneId).Include(x => x.Stations).ThenInclude(x => x.Strategy).FirstOrDefault();

        if (drone != null)
        {
            var schedules = new List<(string, string)>();

            foreach(var station in drone.Stations)
            {
                var currentStrategy = station.Strategy;

                if(currentStrategy != null)
                {
                    var schedulingResult = _context.SchedulingResults.Where(x => x.ProgramStrategyId == currentStrategy.Id && x.Date == date).FirstOrDefault();

                    if(schedulingResult != null)
                    {
                        string filePath = schedulingResult.Path;
                        string fileContents = System.IO.File.ReadAllText(filePath);

                        schedules.Add((station.Number.ToString(), fileContents));
                    }
                }
            }
            return schedules;
        }

        return [.. new List<(string, string)>()];
    }
}

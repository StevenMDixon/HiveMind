using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class DroneService(SqliteDBContext context) : BaseService(context)
{
    public List<Drone> GetAll()
    {
        return [.. _context.Drones.Include(x => x.Stations)];
    }

    public Drone? GetByHostName(string hostName)
    {
        var drone = _context.Drones.Where(x => x.HostName == hostName).Include(x => x.Stations).FirstOrDefault();

        return drone;
    }

    public Drone? GetById(int id)
    {
        var drone = _context.Drones.Where(x => x.DroneId == id).Include(x => x.Stations).FirstOrDefault();
        return drone;
    }

    public void Create(string name, string hostName, int stationSlots)
    {
        _context.Drones.Add(new Drone() { Name = name, HostName = hostName, StationSlots = stationSlots });
        _context.SaveChanges();
    }

    public void Update(Drone drone)
    {
        _context.Drones.Update(drone);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var drone = _context.Drones.Find(id);
        if (drone != null)
        {
            _context.Drones.Remove(drone);
            _context.SaveChanges();
        }
    }

    public List<(string, string)> GetDroneSchedule(int droneId, DateOnly date)
    {
        var drone = _context.Drones.Where(x => x.DroneId == droneId).Include(x => x.Stations).ThenInclude(x => x.Strategy).FirstOrDefault();

        if (drone != null)
        {
            var schedules = new List<(string, string)>();

            foreach(var station in drone.Stations)
            {
                var currentStrategy = station.Strategy;

                if(currentStrategy != null)
                {
                    var schedulingResult = _context.SchedulingResults.Where(x => x.ProgramStrategyId == currentStrategy.ProgramStrategyId && x.Date == date).FirstOrDefault();

                    if(schedulingResult != null)
                    {
                        string filePath = schedulingResult.Path;
                        string fileContents = System.IO.File.ReadAllText(filePath);

                        schedules.Add((station.StationNumber.ToString(), fileContents));
                    }
                }
            }
            return schedules;
        }

        return [.. new List<(string, string)>()];
    }
}

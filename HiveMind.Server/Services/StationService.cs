using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class StationService: BaseService
{
    public StationService(SqliteDBContext context) : base(context) { }

    public IEnumerable<Station> GetAllStations()
    {
        return _context.Stations;
    }

    public void AddStation(Station station)
    {
        _context.Stations.Add(station);
        _context.SaveChanges();
    }

    public Station? GetStationByID(int id)
    {
        return _context.Stations.Find(id);
    }

    public void Update(Station station)
    {
        _context.Stations.Update(station);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var station = _context.Stations.Find(id);
        if (station != null)
        {
            _context.Stations.Remove(station);
            _context.SaveChanges();
        }
    }
}
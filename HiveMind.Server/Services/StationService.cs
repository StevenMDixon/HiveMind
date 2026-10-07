using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class StationService(SqliteDBContext context) : BaseService<Station>(context)
{
    public IEnumerable<Station> GetAllStations()
    {
        return _context.Stations;
    }

    public Station AddStation(Station station)
    {
        return Create(station);
    }

    public Station? GetStationByID(int id)
    {
        return Get().FirstOrDefault(s => s.Id == id);
    }

    public Station UpdateStation(Station station)
    {
        return Update(station);
    }

    public void Delete(int id)
    {
        var station = Get().FirstOrDefault(s => s.Id == id);
        if (station != null)
        {
            Delete(station);
        }
    }
}
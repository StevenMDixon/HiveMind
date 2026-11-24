using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class StationService(SqliteDBContext context) : BaseService<Station>(context), IStationService
{
    public IEnumerable<Station> GetByIds(ICollection<int> ids)
    {
        return _context.Stations.Where(x => ids.Contains(x.Id));
    }

    public Station? GetById(int id)
    {
        return Get().FirstOrDefault(s => s.Id == id);
    }
}
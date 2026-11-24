using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public interface IStationService: IBaseService<Station>
{
    public IEnumerable<Station> GetByIds(ICollection<int> ids);

    public Station? GetById(int id);
}
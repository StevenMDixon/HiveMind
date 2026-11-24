using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class LineupService(SqliteDBContext context) : BaseService<Lineup>(context)
{
    public Lineup? GetById(int Id)
    {
        return Get().FirstOrDefault(x => x.Id == Id);
    }
}

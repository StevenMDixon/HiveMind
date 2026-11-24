using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class ShowService(SqliteDBContext context) : BaseService<Show>(context), IShowService
{
    public Show? GetById(int id)
    {
        return Get().FirstOrDefault(s => s.Id == id);
    }

    public Show? GetByName(string name)
    {
        return Get().FirstOrDefault(s => s.Name == name);
    }
}

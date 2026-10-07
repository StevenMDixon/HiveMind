using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class ShowService(SqliteDBContext context) : BaseService<Show>(context)
{
    public IEnumerable<Show> GetAllShows()
    {
        return Get();
    }

    public Show AddShow(Show mediaItemShow)
    {
        return Create(mediaItemShow);
    }

    public Show? GetShowByID(int id)
    {
        return Get().FirstOrDefault(s => s.Id == id);
    }

    public Show? GetByName(string name)
    {
        return Get().FirstOrDefault(s => s.Name == name);
    }

    public Show UpdateShow(Show show)
    {
        return Update(show);
    }

    public void DeleteShow(int id)
    {
        var show = Get().FirstOrDefault(s => s.Id == id);
        if (show != null)
        {
            Delete(show);
        }
    }
}

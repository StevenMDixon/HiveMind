using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class ShowService:BaseService
{
    public ShowService(sqliteDBContext context) : base(context) { }

    public IEnumerable<Show> GetAllShows()
    {
        return _context.Shows;
    }

    public int AddShow(Show mediaItemShow)
    {
        _context.Shows.Add(mediaItemShow);
        _context.SaveChanges();

        return mediaItemShow.ShowId;
    }

    public Show? GetShowByID(int id)
    {
        return _context.Shows.Find(id);
    }

    public Show? GetByName(string name)
    {
        return _context.Shows.FirstOrDefault(s => s.ShowTitle == name);
    }

    public void Update(Show show)
    {
        _context.Shows.Update(show);
        _context.SaveChanges();
    }

    public void DeleteShow(int id)
    {
        var show = _context.Shows.Find(id);
        if (show != null)
        {
            _context.Shows.Remove(show);
            _context.SaveChanges();
        }
    }
}

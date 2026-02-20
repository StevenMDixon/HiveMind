using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class MediaItemShowService:BaseService
{
    public MediaItemShowService(sqliteDBContext context) : base(context) { }

    public IEnumerable<MediaItemShow> GetAllMediaItemShows()
    {
        return _context.MediaItemShows;
    }


    public int AddMediaItemShow(MediaItemShow mediaItemShow)
    {
        _context.MediaItemShows.Add(mediaItemShow);
        _context.SaveChanges();

        return mediaItemShow.MediaItemShowId;
    }

    public MediaItemShow? GetMediaItemShowByID(int id)
    {
        return _context.MediaItemShows.Find(id);
    }

    public MediaItemShow? GetByName(string name)
    {
        return _context.MediaItemShows.FirstOrDefault(s => s.MediaItemShowTitle == name);
    }

    public void Update(MediaItemShow mediaItemShow)
    {
        _context.MediaItemShows.Update(mediaItemShow);
        _context.SaveChanges();
    }

    public void DeleteMediaItemShow(int id)
    {
        var mediaItemShow = _context.MediaItemShows.Find(id);
        if (mediaItemShow != null)
        {
            _context.MediaItemShows.Remove(mediaItemShow);
            _context.SaveChanges();
        }
    }
}

using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class MediaItemService: BaseService
{
    public MediaItemService(sqliteDBContext context) : base(context) { }

    public IEnumerable<MediaItem> GetAllSMediaItems()
    {
        return _context.MediaItems;
    }

    public void AddMediaItem(MediaItem mediaItem)
    {
        _context.MediaItems.Add(mediaItem);
        _context.SaveChanges();
    }

    public void AddMediaItems(IEnumerable<MediaItem> mediaItems)
    {
        _context.MediaItems.AddRange(mediaItems);
        _context.SaveChanges();
    }

    public MediaItem? GetMediaItemByID(int id)
    {
        return _context.MediaItems.Find(id);
    }

    public IEnumerable<MediaItem> GetMediaItemLibraryID(int id)
    {
        return _context.MediaItems.Where(x => x.LibraryId == id);
    }

    public void Update(MediaItem mediaItem)
    {
        _context.MediaItems.Update(mediaItem);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var mediaItem = _context.MediaItems.Find(id);
        if (mediaItem != null)
        {
            _context.MediaItems.Remove(mediaItem);
            _context.SaveChanges();
        }
    }

    public void DeleteMany(IEnumerable<MediaItem> mediaItems)
    {
        _context.MediaItems.RemoveRange(mediaItems);
        _context.SaveChanges();
    }
}

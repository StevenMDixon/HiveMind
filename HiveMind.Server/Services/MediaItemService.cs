using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class MediaItemService(SqliteDBContext context) : BaseService<MediaItem>(context)
{
    public IEnumerable<MediaItem> GetAllMediaItems()
    {
        return Get().Include(media => media.Library).Include(media => media.Show).Include(media => media.Tags);
    }

    public void AddMediaItem(MediaItem mediaItem)
    {
        Create(mediaItem);
    }

    public void AddMediaItems(IEnumerable<MediaItem> mediaItems)
    {
        foreach (var mediaItem in mediaItems)
        {
            Create(mediaItem);
        }
    }

    public MediaItem? GetMediaItemByID(int Id)
    {
        return Get().Include(x => x.Tags).Include(x => x.Library).Include(x => x.Show).FirstOrDefault(x => x.Id == Id);
    }

    public IEnumerable<MediaItem> GetMediaItemLibraryID(int Id)
    {
        return Get().Where(x => x.LibraryId == Id);
    }

    public void Delete(int Id)
    {
        var mediaItem = Get().FirstOrDefault(x => x.Id == Id);
        if (mediaItem != null)
        {
            Delete(mediaItem);
        }
    }

    public void DeleteMany(IEnumerable<MediaItem> mediaItems)
    {
        foreach (var mediaItem in mediaItems)
        {
            Delete(mediaItem);
        }
    }
}

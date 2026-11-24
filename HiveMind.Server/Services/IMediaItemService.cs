using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public interface IMediaItemService: IBaseService<MediaItem>
{
    public IEnumerable<MediaItem> GetAllMediaItems();
    public IEnumerable<MediaItem> GetMediaItemsWithoutThumbs();
    public MediaItem? GetMediaItemByID(int Id);
    public IEnumerable<MediaItem> GetMediaItemLibraryID(int Id);
}
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.MediaItems;

public class GetAllMediaItems
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllMediaItems");
    }

    public record Tag(int TagId, string TagName);
    public record Show(int ShowId, string Name);

    public record Library();

    public record MediaItem(int MediaItemId, string Title, double Duration, int LibraryId, string? MediaType, string FilePath, int Width, int Height, string Resolution, int EpisodeNumber, int SeasonNumber, Show? Show, IEnumerable<Tag> Tags);
    public record GetAllMediaItemsResponse(List<MediaItem> mediaItems);

    public static Results<Ok<GetAllMediaItemsResponse>, NotFound> Handle(MediaItemService mediaItemService)
    {
        var mediaItems = mediaItemService.GetAllMediaItems();

        var mappedMediaItems = mediaItems.Select(x =>
            new MediaItem(
                x.MediaItemId,
                x.Title,
                x.Duration,
                x.LibraryId,
                x.Library?.LibraryType.ToString(),
                x.FilePath,
                x.Width,
                x.Height,
                x.Resolution,
                x.EpisodeNumber,
                x.SeasonNumber,
                x.Show is not null ? new Show(x.Show.ShowId, x.Show.ShowTitle) : null,
                x.Tags is not null ? x.Tags.Select(y => new Tag(y.TagId, y.TagName)) : []
                )
        );

        return TypedResults.Ok(new GetAllMediaItemsResponse(mappedMediaItems.ToList()));
    }
}
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.MediaItems;

public class GetAll
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle).WithName("GetAllMediaItems");
    }

    public record Tag(int Id, string Name);
    public record Show(int Id, string Name, string Rating);

    public record Library();

    public record MediaItem(int Id, string Title, double Duration, int LibraryId, string? MediaType, string FilePath, int Width, int Height, string Resolution, int EpisodeNumber, int SeasonNumber, Show? Show, IEnumerable<Tag> Tags);
    public record GetAllMediaItemsResponse(List<MediaItem> MediaItems);

    public static Results<Ok<GetAllMediaItemsResponse>, NotFound> Handle(MediaItemService mediaItemService)
    {
        var mediaItems = mediaItemService.GetAllMediaItems();

        var mappedMediaItems = mediaItems.Select(x =>
            new MediaItem(
                x.Id,
                x.Title,
                x.Duration,
                x.LibraryId,
                x.Library?.Type.ToString(),
                x.FilePath,
                x.Width,
                x.Height,
                x.Resolution,
                x.EpisodeNumber,
                x.SeasonNumber,
                x.Show is not null ? new Show(x.Show.Id, x.Show.Name, x.Show.Rating.ToString()) : null,
                x.Tags is not null ? x.Tags.Select(y => new Tag(y.Id, y.Name)) : []
                )
        ).ToList();

        return TypedResults.Ok(new GetAllMediaItemsResponse(mappedMediaItems));
    }
}
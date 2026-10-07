using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static HiveMind.Server.Endpoints.MediaItems.GetAll;


namespace HiveMind.Server.Endpoints.MediaItems;

public class Get
{ 
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle)
            .WithName("Get");
    }

    public record MediaItem(int Id, string Title, double Duration, int LibraryId, string? MediaType, string FilePath, int Width, int Height, string Resolution, int EpisodeNumber, int SeasonNumber, Show? Show, IEnumerable<Tag> Tags);

    public static Results<Ok<MediaItem>, NoContent, NotFound<string>> Handle(MediaItemService service, [FromRoute] int id)
    {
        var mediaItem = service.GetMediaItemByID(id);

        if(mediaItem != null)
        {
            return TypedResults.Ok(new MediaItem(
                mediaItem.Id,
                mediaItem.Title,
                mediaItem.Duration,
                mediaItem.LibraryId,
                mediaItem.Library?.Type.ToString(),
                mediaItem.FilePath,
                mediaItem.Width,
                mediaItem.Height,
                mediaItem.Resolution,
                mediaItem.EpisodeNumber,
                mediaItem.SeasonNumber,
                mediaItem.Show is not null ? new Show(mediaItem.Show.Id, mediaItem.Show.Name, mediaItem.Show.Rating.ToString()) : null,
                mediaItem.Tags is not null ? mediaItem.Tags.Select(y => new Tag(y.Id, y.Name)) : []
                ));
        }

        return TypedResults.NotFound($"A MediaItem with the ID: {id} was not found.");
    }
}

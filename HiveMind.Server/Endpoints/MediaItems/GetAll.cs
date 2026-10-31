using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static HiveMind.Server.Endpoints.Scheduler.GetLineupResults;
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
    public record GetAllMediaItemsResponse(List<MediaItem> MediaItems, int CurrentPage, int PageSize, int DataCount);

    public record MediaParameters(
        string? Search,
        int? Page,
        int? Count
    );


    public static Results<Ok<GetAllMediaItemsResponse>, NotFound> Handle(MediaItemService mediaItemService, [AsParameters] MediaParameters filters)
    {
        var mediaItems = mediaItemService.GetAllMediaItems();

        var pageSize = filters.Count ?? 100;
        var currentPage = filters.Page ?? 1;
        var dataCount = mediaItems.Count();

         if (filters.Search != null && filters.Search != string.Empty) mediaItems = mediaItems.Where(x => x.Title.ToLower().Contains(filters.Search.ToLower()));

        var m = (pageSize * currentPage);
        mediaItems = mediaItems.Skip((pageSize  * (currentPage - 1))).Take(pageSize);

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
        );

        return TypedResults.Ok(new GetAllMediaItemsResponse(mappedMediaItems.ToList(), currentPage, pageSize, dataCount));
    }
}
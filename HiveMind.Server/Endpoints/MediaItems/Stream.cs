using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.MediaItems;

public class Stream
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/stream", Handle)
            .WithName("StreamMedia");
    }

    public static Results<IResult, NotFound<string>> Handle(MediaItemService service, SettingsService settings, [FromRoute] int id)
    {
        var mediaItem = service.GetMediaItemByID(id);

        var path = settings.GetByName("Import_Location");

        if (mediaItem != null)
        {
            var filePath = Path.Combine(
            path?.Value ?? "",
            mediaItem.FilePath.TrimStart(
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar
            ));

            var contentType = Path.GetExtension(filePath).ToLowerInvariant() switch
            {
                ".mkv" => "video/x-matroska",
                ".mp4" => "video/mp4",
                ".avi" => "video/x-msvideo",
                _ => "application/octet-stream"
            };

            return TypedResults.PhysicalFile(
                filePath,
                contentType: contentType,
                enableRangeProcessing: true
            );
        }
        else return TypedResults.NotFound($"A media item with the ID: {id} was not found.");
    }
}

using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;


namespace HiveMind.Server.Endpoints.MediaItems;

public class GetUntaggedByType
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/type/{type}", Handle)
            .WithName("GetUntaggedByType");
    }

    public static Results<Ok<int>, NoContent, NotFound<string>> Handle(MediaItemService service, [FromRoute] string type)
    {
        var mediaItems = service.GetAllMediaItems().Where(x => x.Library!.Type.ToString() == type);

        var media = mediaItems
            .OrderBy(_ => Random.Shared.Next())
            .FirstOrDefault(x => !MediaItemIsFullyTagged(x));

        return TypedResults.Ok(media?.Id ?? 0);
    }

    private static bool MediaItemIsFullyTagged(MediaItem mediaItem)
    {
        var requiredTypes = Enum.GetValues<TagCategory>().Where(x => isRequired(x)).ToList();

        var mediaTagsTypes = mediaItem.Tags?.Select(x => x.Category).Distinct();

        if (mediaTagsTypes == null || mediaTagsTypes.Count() < requiredTypes.Count()) return false;

        return requiredTypes.All(x => mediaTagsTypes.Contains(x));
    }

    private static Boolean isRequired(TagCategory tagCategory)
    {
        var member = typeof(TagCategory).GetMember(tagCategory.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<Required>()?._Required ?? false;
    }
}

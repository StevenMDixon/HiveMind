using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

using System.Reflection;

namespace HiveMind.Server.Endpoints.TaggingQueue;

public class QueuePercentages
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle)
            .WithName("GetPercentages");
    }

    public record Queue(string Name, int Met, int Total);
    public record GetQueueResponse(List<Queue> Queues);

    private class Meta()
    {
        public int met { get; set; } = 0;
        public int total { get; set; } = 0;
    }

    public static Results<Ok<GetQueueResponse>, NotFound<string>> Handle(MediaItemService mediaService)
    {
        var results = new Dictionary<LibraryType, Meta>();

        var media = mediaService.GetAllMediaItems();

        foreach(var item in media)
        {
            var type = item.Library!.Type;
            var isFullyTagged = MediaItemIsFullyTagged(item);
            if (results.ContainsKey(type))
            {
                var meta = results[type];
                meta.met += isFullyTagged ? 1 : 0;
                meta.total += 1;
            }
            else
            {
                var meta = new Meta()
                {
                    met = isFullyTagged ? 1 : 0,
                    total = 1
                };
                results[type] = meta;
            }
        }

        return TypedResults.Ok(new GetQueueResponse(results.Keys.Select(x => new Queue(x.ToString(), results[x].met, results[x].total)).ToList()));
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

using HiveMind.Server.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;

namespace HiveMind.Server.Endpoints.TaggingQueue;

public class GetTagCategories
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/tag-categories", Handle)
            .WithName("GetTagCategoriesWithRequired");
    }

    public record Tag(TagCategory Category, bool Required);

    public static Results<Ok<List<Tag>>, NotFound<string>> Handle()
    {
        var categories = Enum.GetValues<TagCategory>().ToList();

       return TypedResults.Ok(categories.Select(x => new Tag(x, isRequired(x))).ToList());
    }

    private static Boolean isRequired(TagCategory tagCategory)
    {
        var member = typeof(TagCategory).GetMember(tagCategory.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<Required>()?._Required ?? false;
    }
}
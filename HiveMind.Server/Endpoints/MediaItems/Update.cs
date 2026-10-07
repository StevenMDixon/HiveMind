using FluentValidation;
using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.MediaItems;

public class Update
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<MediaItemUpdateRequest>()
            .WithName("UpdateMediaItem")
            .ProducesValidationProblem();
    }

    public class Validator : AbstractValidator<MediaItemUpdateRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Title).MaximumLength(200).When(x => x.Title != null);
            RuleFor(x => x.Title).NotEmpty().When(x => x.Title != null);
        }
    }

    public record MediaItemUpdateRequest(string? Title, IEnumerable<UpdatedTag>? Tags);
    
    public record UpdatedTag(int Id, string Name);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(MediaItemService mediaItemService, TagsService tagService, [FromRoute] int id, [FromBody] MediaItemUpdateRequest request)
    {
        var mediaItem = mediaItemService.GetMediaItemByID(id);

        if (mediaItem is not null)
        {
            mediaItem.Title = request.Title ?? mediaItem.Title;

            var itemsToUpdate = new List<(Entities.Tags, UpdatedTag)>();

            if (request.Tags != null)
            {
                foreach (var updatedTag in request.Tags)
                {
                    var matchedItem = mediaItem.Tags?.Where(x => x.Id == updatedTag.Id).FirstOrDefault();

                    if (matchedItem != null)
                    {
                        itemsToUpdate.Add((matchedItem, updatedTag));
                        continue;
                    }

                    var existingTag = tagService.GetByName(updatedTag.Name);

                    var fixedTag = existingTag != null ? new UpdatedTag(existingTag.Id, existingTag.Name) : updatedTag;

                    itemsToUpdate.Add((existingTag ?? new Entities.Tags(), fixedTag));
                }

                mediaItem.Tags = itemsToUpdate.Select(x => UpdateItem(x.Item1, x.Item2)).ToList();
            }

            mediaItemService.Update(mediaItem);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A Media Item with the ID: {id} was not found.");
    }

    private static Entities.Tags UpdateItem(Entities.Tags tag, UpdatedTag updatedTag)
    {
        if (updatedTag != null)
        {
            tag.Id = updatedTag.Id;
            tag.Name = updatedTag.Name;
        }

        return tag;
    }
}

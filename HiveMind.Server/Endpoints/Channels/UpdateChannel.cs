using FluentValidation;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Channels;

public class UpdateChannel
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", Handle)
            .WithRequestValidation<Validator>()
            .WithName("UpdateChannel")
            .ProducesValidationProblem();
    }

    public class Validator: AbstractValidator<ChannelRequest>
    {
        public Validator()
        {
            RuleFor(x => x.ChannelName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ChannelNumber).GreaterThan(0);
        }
    }

    public record ChannelRequest(string ChannelName, int ChannelNumber);

    public static Results<Ok, NotFound<string>, ValidationProblem> Handle(ChannelService channelService, [FromRoute] int id, [FromBody] ChannelRequest request)
    {
        var channel = channelService.GetChannelByID(id);

        if(channel is not null)
        {
            channel.ChannelName = request.ChannelName;
            channel.ChannelNumber = request.ChannelNumber;
            channelService.Update(channel);
            return TypedResults.Ok();
        }

        return TypedResults.NotFound($"A channel with the ID: {id} was not found.");
    }
}

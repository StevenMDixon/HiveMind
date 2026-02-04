using System.Reflection.Metadata;
using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Channels;

public class CreateChannel
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle).WithName("CreateChannel");
    }

    public record ChannelRequest(string ChannelName);

    public static Results<Ok, NoContent> Handle(ChannelService channelService, [FromBody] ChannelRequest request)
    {

        var newChannel = new Entities.Channel
        {
            ChannelName = request.ChannelName
        };

        channelService.AddChannel(newChannel);

        return TypedResults.NoContent();
    }
}

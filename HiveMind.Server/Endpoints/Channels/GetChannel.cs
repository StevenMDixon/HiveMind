using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Channels;

public class GetChannel
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", Handle);
    }

    public record Channel(int ChannelID, string ChannelName, int ChannelNumber);
    public record GetChannelsResponse(Channel Channel);

    public static Results<Ok<GetChannelsResponse>, NotFound<string>> Handle(ChannelService channelService, [FromRoute] int id)
    {
        var channel = channelService.GetChannelByID(id);

        if (channel == null) return TypedResults.NotFound($"A channel with the ID: {id} was not found.");

        return TypedResults.Ok(new GetChannelsResponse(new Channel(channel.ChannelId, channel.ChannelName, channel.ChannelNumber)));
    }
}

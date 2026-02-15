using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HiveMind.Server.Endpoints.Channels;

public static class GetAllChannels
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", Handle);
    }
    
    public record Channel(int ChannelID, string ChannelName, int ChannelNumber);
    public record GetAllChannelsResponse(List<Channel> Channels);

    public static Results<Ok<GetAllChannelsResponse>, NotFound> Handle(ChannelService channelService)
    {
        var channels = channelService.GetAllChannels();

        return TypedResults.Ok(new GetAllChannelsResponse(channels.Select(x => new Channel(x.ChannelId, x.ChannelName, x.ChannelNumber)).ToList()));
    }
}

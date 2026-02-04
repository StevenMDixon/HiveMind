namespace HiveMind.Server.Endpoints.Channels;

public static class Endpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("channels");
        GetAllChannels.Map(endpoints);
        CreateChannel.Map(endpoints);
        UpdateChannel.Map(endpoints);
    }
}

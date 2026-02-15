namespace HiveMind.Server.Endpoints.Channels;

public static class EndPoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("channels");
        GetAllChannels.Map(endpoints);
        CreateChannel.Map(endpoints);
        UpdateChannel.Map(endpoints);
        DeleteChannel.Map(endpoints);
    }
}

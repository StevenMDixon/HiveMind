using HiveMind.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiveMind.Server.Endpoints.Channels
{
    public class DeleteChannel
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id:int}", Handle)
                .WithName("DeleteChannel");
        }

        public static Results<Ok, NotFound<string>> Handle(ChannelService channelService, [FromRoute] int id)
        {
            var channel = channelService.GetChannelByID(id);

            if (channel is not null)
            {
                channelService.Delete(id);
                return TypedResults.Ok();
            }

            return TypedResults.NotFound($"A channel with the ID: {id} was not found.");
        }
    }
}

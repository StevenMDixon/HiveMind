namespace HiveMind.Server.Endpoints.Schedules
{
    public class EndPoints
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("Schedules");
            GetAllSchedules.Map(endpoints);
            GetScheduleById.Map(endpoints);
            CreateSchedule.Map(endpoints);
            UpdateSchedule.Map(endpoints);
            DeleteSchedule.Map(endpoints);
        }
    }
}

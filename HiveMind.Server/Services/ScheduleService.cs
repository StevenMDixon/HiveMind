using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class ScheduleService(SqliteDBContext context) : BaseService<SchedulingResult>(context)
{
    public void DeleteSchedulingResult(int schedulingResultId)
    {
        var item = Get().FirstOrDefault(sr => sr.Id == schedulingResultId);
        if (item != null)
        {
            Delete(item);
        }
    }

    public List<SchedulingResult> GetSchedulingResults(DateOnly date)
    {
        return Get().Where(sr => sr.Date <= date).ToList();
    }
}

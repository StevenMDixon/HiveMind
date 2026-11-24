using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public interface ISchedulingService: IBaseService<SchedulingResult>
{
    public void DeleteSchedulingResult(int schedulingResultId);
    public List<SchedulingResult> GetSchedulingResults(DateOnly date);
}
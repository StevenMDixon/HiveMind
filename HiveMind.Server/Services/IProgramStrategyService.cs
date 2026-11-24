using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public interface IProgramStrategyService: IBaseService<ProgramStrategy>
{
    public IEnumerable<ProgramStrategy> GetAllProgramStrategies();
    public IEnumerable<ProgramStrategy> GetAvailableStrategiesToProcess();
    public ProgramStrategy? GetById(int Id);
}
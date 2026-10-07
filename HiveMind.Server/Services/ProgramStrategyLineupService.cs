using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class ProgramStrategyLineupService(SqliteDBContext context) : BaseService<ProgramStrategyLineup>(context)
{
    public ProgramStrategyLineup? GetProgramStrategyLineupById(int Id)
    {
        return Get().Where(x => x.Id == Id).Include(x => x.Lineup).FirstOrDefault();
    }
}

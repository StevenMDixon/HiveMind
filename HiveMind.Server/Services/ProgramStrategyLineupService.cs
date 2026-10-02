using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class ProgramStrategyLineupService(SqliteDBContext context) : BaseService(context)
{
    public ProgramStrategyLineup? GetProgramStrategyLineupById(int id)
    {
        return _context.ProgramStrategyLineups.Where(x => x.ProgramStrategyLineupId == id).Include(x => x.Lineup).FirstOrDefault();
    }
}

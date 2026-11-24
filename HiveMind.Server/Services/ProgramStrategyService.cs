using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services
{
    public class ProgramStrategyService(SqliteDBContext context) : BaseService<ProgramStrategy>(context), IProgramStrategyService
    {

        public IEnumerable<ProgramStrategy> GetAllProgramStrategies()
        {
            return Get().Include(x => x.Lineups);
        }

        public IEnumerable<ProgramStrategy> GetAvailableStrategiesToProcess()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            return Get().Include(x => x.Lineups).Where(ps => ps.Active == true && (ps.LastScheduleDate <= currentDate || ps.LastScheduleDate == null));
        }

        public ProgramStrategy? GetById(int Id)
        {
            return Get().Include(x => x.Lineups).Include(x => x.Events).FirstOrDefault(x => x.Id == Id);
        }
    }
}

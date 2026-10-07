using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services
{
    public class ProgramStrategyService(SqliteDBContext context) : BaseService<ProgramStrategy>(context)
    {

        public IEnumerable<ProgramStrategy> GetAllProgramStrategies()
        {
            return Get(b => b.Lineups);
        }

        public IEnumerable<ProgramStrategy> GetAvailableStrategiesToProcess()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            return Get(b => b.Lineups).Where(ps => ps.Active == true && (ps.LastScheduleDate <= currentDate || ps.LastScheduleDate == null));
        }

        public ProgramStrategy AddProgramStrategy(ProgramStrategy programStrategy)
        {
            return Create(programStrategy);
        }

        public ProgramStrategy? GetProgramStrategyById(int Id)
        {
            return Get(b => b.Lineups).FirstOrDefault(x => x.Id == Id);
        }

        public void Delete(int Id)
        {
            var programStrategy = Get().FirstOrDefault(x => x.Id == Id);
            if (programStrategy != null)
            {
                Delete(programStrategy);
            }
        }
    }
}

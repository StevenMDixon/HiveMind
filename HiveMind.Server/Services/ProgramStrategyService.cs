using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services
{
    public class ProgramStrategyService(SqliteDBContext context) : BaseService(context)
    {

        public IEnumerable<ProgramStrategy> GetAllProgramStrategies()
        {
            return _context.ProgramStrategies.Include(c => c.Lineups);
        }

        public IEnumerable<ProgramStrategy> GetAvailableStrategiesToProcess()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            return [.. _context.ProgramStrategies.Include(x => x.Lineups).Where(ps => ps.Active == true && (ps.LastScheduleDate <= currentDate || ps.LastScheduleDate == null))];
        }

        public ProgramStrategy AddProgramStrategy(ProgramStrategy programStrategy)
        {
            _context.ProgramStrategies.Add(programStrategy);
            _context.SaveChanges();
            return programStrategy;
        }

        public ProgramStrategy? GetProgramStrategyById(int id)
        {
            return _context.ProgramStrategies.Include(x => x.Lineups).FirstOrDefault(x => x.ProgramStrategyId == id);
        }

        public void Update(ProgramStrategy programStrategy)
        {
            _context.ProgramStrategies.Update(programStrategy);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var programStrategy = _context.ProgramStrategies.Find(id);
            if (programStrategy != null)
            {
                _context.ProgramStrategies.Remove(programStrategy);
                _context.SaveChanges();
            }
        }
    }
}

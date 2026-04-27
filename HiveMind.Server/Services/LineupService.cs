using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class LineupService:BaseService
{
    public LineupService(sqliteDBContext context) : base(context) { }

    public IEnumerable<Lineup> GetAllLineups()
    {
        return _context.Lineups;
    }

    public void AddLineup(Lineup lineup)
    {
        _context.Lineups.Add(lineup);
        _context.SaveChanges();
    }

    public Lineup? GetLineupByID(int id)
    {
        return _context.Lineups
            .Include(c => c.LineupItems!)
            .ThenInclude(s => s.Queries)
            .FirstOrDefault(x => x.LineupId == id);
    }

    public void Update(Lineup lineup)
    {
        _context.Lineups.Update(lineup);
        _context.SaveChanges();
    }

    public void DeleteLineup(int id)
    {
        var lineup = _context.Lineups.Find(id);
        if (lineup != null)
        {
            _context.Lineups.Remove(lineup);
            _context.SaveChanges();
        }
    }
}

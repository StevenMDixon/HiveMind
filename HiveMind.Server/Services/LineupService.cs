using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class LineupService(SqliteDBContext context) : BaseService<Lineup>(context)
{
    public IEnumerable<Lineup> GetAllLineups()
    {
        return Get();
    }

    public void AddLineup(Lineup lineup)
    {
        Create(lineup);
    }

    public Lineup? GetLineupByID(int Id)
    {
        return Get().FirstOrDefault(x => x.Id == Id);
    }

    public void Update(Lineup lineup)
    {
        _context.Lineups.Update(lineup);
        _context.SaveChanges();
    }

    public void DeleteLineup(int Id)
    {
        var lineup = Get().FirstOrDefault(x => x.Id == Id);
        if (lineup != null)
        {
            Delete(lineup);
        }
    }
}

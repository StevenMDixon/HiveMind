using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services
{
    public class LineupItemService:BaseService
    {
        public LineupItemService(sqliteDBContext context) : base(context) { }
        
        public IEnumerable<LineupItem> GetLineupItems()
        {
            return _context.LineupItems;
        }
        
        public LineupItem AddLineupItem(LineupItem lineupItem)
        {
            _context.LineupItems.Add(lineupItem);
            _context.SaveChanges();
            return lineupItem;
        }
        
        public LineupItem? GetLineupItemByID(int id)
        {
            return _context.LineupItems.Include(x => x.Queries).FirstOrDefault(x => x.LineupItemId == id);
        }

        public IEnumerable<LineupItem> GetLineupItemsByLineupID(int lineupId)
        {
            return _context.LineupItems.Where(item => item.LineupId == lineupId);
        }

        public void Update(LineupItem lineupItem)
        {
            _context.LineupItems.Update(lineupItem);
            _context.SaveChanges();
        }
        
        public void DeleteLineupItem(int id)
        {
            var lineupItem = _context.LineupItems.Find(id);
            if (lineupItem != null)
            {
                _context.LineupItems.Remove(lineupItem);
                _context.SaveChanges();
            }
        }
    }
}

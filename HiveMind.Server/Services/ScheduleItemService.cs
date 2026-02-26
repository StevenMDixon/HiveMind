using HiveMind.Server.Entities;

namespace HiveMind.Server.Services
{
    public class ScheduleItemService:BaseService
    {
        public ScheduleItemService(sqliteDBContext context) : base(context) { }
        
        public IEnumerable<ScheduleItem> GetAllScheduleItems()
        {
            return _context.ScheduleItems;
        }
        
        public void AddScheduleItem(ScheduleItem scheduleItem)
        {
            _context.ScheduleItems.Add(scheduleItem);
            _context.SaveChanges();
        }
        
        public ScheduleItem? GetScheduleItemByID(int id)
        {
            return _context.ScheduleItems.Find(id);
        }

        public IEnumerable<ScheduleItem> GetScheduleItemsByScheduleID(int scheduleId)
        {
            return _context.ScheduleItems.Where(item => item.ScheduleId == scheduleId);
        }

        public void Update(ScheduleItem scheduleItem)
        {
            _context.ScheduleItems.Update(scheduleItem);
            _context.SaveChanges();
        }
        
        public void DeleteScheduleItem(int id)
        {
            var scheduleItem = _context.ScheduleItems.Find(id);
            if (scheduleItem != null)
            {
                _context.ScheduleItems.Remove(scheduleItem);
                _context.SaveChanges();
            }
        }
    }
}

using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class TransitionTemplateService: BaseService
{
    public TransitionTemplateService(SqliteDBContext context) : base(context)
    {
    }

    public IEnumerable<TransitionTemplate> GetAllTransitionTemplates()
    {
        return _context.TransitionTemplates;
    }   

    public TransitionTemplate? GetTransitionTemplateByID(int id)
    {
        return _context.TransitionTemplates.Where(tt => tt.TransitionTemplateId == id).Include(tt => tt.Slots).FirstOrDefault();
    }

    public void CreateTransitionTemplate(TransitionTemplate template)
    {
        _context.TransitionTemplates.Add(template);
        _context.SaveChanges();
    }

    public void UpdateTransitionTemplate(TransitionTemplate template)
    {
        _context.Update(template);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var template = _context.TransitionTemplates.Find(id);
        if (template != null) {
            _context.TransitionTemplates.Remove(template);
            _context.SaveChanges();
        }
    }
}

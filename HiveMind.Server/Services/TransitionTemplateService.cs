using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class TransitionTemplateService(SqliteDBContext context) : BaseService<TransitionTemplate>(context)
{
    public IEnumerable<TransitionTemplate> GetAllTransitionTemplates()
    {
        return Get().ToList();
    }   

    public TransitionTemplate? GetTransitionTemplateByID(int id)
    {
        return Get().Where(tt => tt.Id == id).Include(tt => tt.Slots).FirstOrDefault();
    }

    public TransitionTemplate CreateTransitionTemplate(TransitionTemplate template)
    {
        return Create(template);
    }

    public TransitionTemplate UpdateTransitionTemplate(TransitionTemplate template)
    {
        return Update(template);
    }

    public void Delete(int id)
    {
        var template = Get().FirstOrDefault(tt => tt.Id == id);
        if (template != null) {
            Delete(template );
        }
    }
}

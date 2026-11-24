using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class TransitionTemplateService(SqliteDBContext context) : BaseService<TransitionTemplate>(context), ITransitionTemplateService
{ 
    public TransitionTemplate? GetById(int id)
    {
        return Get().Where(tt => tt.Id == id).Include(tt => tt.Slots).FirstOrDefault();
    }
}

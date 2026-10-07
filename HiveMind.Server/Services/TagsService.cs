using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class TagsService(SqliteDBContext context) : BaseService<Tags>(context)  
{
    public IEnumerable<Tags> GetAllTags()
    {
        return _context.Tags;      
    }

    public Tags? GetByName(string name)
    {
        return Get().Where(x => x.Name == name).FirstOrDefault();
    }

    public Tags? GetById(int Id)
    {
        return Get().Where(x => x.Id == Id).FirstOrDefault();
    }
}

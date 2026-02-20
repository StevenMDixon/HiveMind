using Microsoft.EntityFrameworkCore;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class TagsService: BaseService
{
    public TagsService(sqliteDBContext context) : base(context) { }

    public IEnumerable<Tags> GetAllTags()
    {
        return _context.Tags;      
    }
}

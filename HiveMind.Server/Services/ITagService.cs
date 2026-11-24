using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public interface ITagService: IBaseService<Tags>
{
    public Tags? GetByName(string Name);
    public Tags? GetById(int Id);
}
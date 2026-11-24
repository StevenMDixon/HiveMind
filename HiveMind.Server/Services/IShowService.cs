using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public interface IShowService: IBaseService<Show>
{
    public Show? GetByName(string Name);
    public Show? GetById(int Id);
}
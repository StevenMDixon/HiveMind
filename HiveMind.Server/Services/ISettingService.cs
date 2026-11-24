using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public interface ISettingService: IBaseService<Settings>
{
    public Settings? GetByName(string Name);
    public Settings? GetById(int Id);
}
using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class SettingsService(SqliteDBContext context) : BaseService<Settings>(context), ISettingService
{
    public Settings? GetByName(string Name)
    {
        return Get().FirstOrDefault(s => s.Name == Name);
    }

    public Settings? GetById(int Id)
    {
        return Get().FirstOrDefault(s => s.Id == Id);
    }
}

using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class SettingsService(SqliteDBContext context) : BaseService<Settings>(context)
{
    public IEnumerable<Settings> GetAllSettings()
    {
        return Get();
    }

    public Settings UpdateSetting(Settings setting)
    {
        return Update(setting);
    }

    public Settings? GetByName(string Name)
    {
        return Get().FirstOrDefault(s => s.Name == Name);
    }

    public Settings? GetById(int Id)
    {
        return Get().FirstOrDefault(s => s.Id == Id);
    }

    public void UpdateSettings(Settings[] settings)
    {
        _context.Settings.UpdateRange(settings);
        _context.SaveChanges();
    }
}

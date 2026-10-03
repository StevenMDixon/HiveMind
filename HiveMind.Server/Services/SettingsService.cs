using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class SettingsService(SqliteDBContext context) : BaseService(context)
{
    public IEnumerable<Settings> GetAllSettings()
    {
        return _context.Settings;
    }

    public void UpdateSetting(Settings setting)
    {
        _context.Settings.Update(setting);
        _context.SaveChanges();
    }

    public Settings? GetByName(string Name)
    {
        return _context.Settings.FirstOrDefault(s => s.Name == Name);
    }

    public Settings? GetById(int Id)
    {
        return _context.Settings.Find(Id);
    }

    public void UpdateSettings(Settings[] settings)
    {
        _context.Settings.UpdateRange(settings);
        _context.SaveChanges();
    }
}

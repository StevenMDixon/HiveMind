using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class SettingsService: BaseService
{
    public SettingsService(sqliteDBContext context) : base(context) { }

    public IEnumerable<Settings> GetAllSettings()
    {
        return _context.Settings;
    }

    public void UpdateSetting(Settings setting)
    {
        _context.Settings.Update(setting);
        _context.SaveChanges();
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

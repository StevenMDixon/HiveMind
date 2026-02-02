using HiveMind.Server.Models;

namespace HiveMind.Server.Services;

public class ChannelService
{
    private readonly sqliteDBContext _context;

    public ChannelService(sqliteDBContext context)
    {
        _context = context;
    }

    public IEnumerable<Channel> GetAllChannels()
    {
        return _context.Channels.ToList();
    }
}

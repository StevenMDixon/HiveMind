using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Services;

public class ChannelService: BaseService
{
    public ChannelService(sqliteDBContext context) : base(context) { }

    public IEnumerable<Channel> GetAllChannels()
    {
        return [.. _context.Channels.Include(c => c.Schedule)];
    }

    public void AddChannel(Channel channel)
    {
        _context.Channels.Add(channel);
        _context.SaveChanges();
    }

    public Channel? GetChannelByID(int id)
    {
        return _context.Channels.Find(id);
    }

    public void Update(Channel channel)
    {
        _context.Channels.Update(channel);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var channel = _context.Channels.Find(id);
        if (channel != null)
        {
            _context.Channels.Remove(channel);
            _context.SaveChanges();
        }
    }
}
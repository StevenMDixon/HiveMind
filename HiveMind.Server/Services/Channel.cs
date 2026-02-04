using HiveMind.Server.Entities;
using Microsoft.EntityFrameworkCore;

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
}

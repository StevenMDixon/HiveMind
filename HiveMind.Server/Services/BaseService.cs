namespace HiveMind.Server.Services;

public abstract class BaseService(SqliteDBContext context) : IService
{
    protected readonly SqliteDBContext _context = context;
}

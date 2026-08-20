namespace HiveMind.Server.Services;

public abstract class BaseService: IService
{
    protected readonly sqliteDBContext _context;

    public BaseService(sqliteDBContext context)
    {
        _context = context;
    }
}

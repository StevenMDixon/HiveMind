using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public class ProgramRotationService(SqliteDBContext context) : BaseService<ProgramRotation>(context), IProgramRotationService
{
    
}

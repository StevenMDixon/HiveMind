using HiveMind.Server.Entities;

namespace HiveMind.Server.Services;

public interface ITransitionTemplateService: IBaseService<TransitionTemplate>
{
    public TransitionTemplate? GetById(int Id);
}
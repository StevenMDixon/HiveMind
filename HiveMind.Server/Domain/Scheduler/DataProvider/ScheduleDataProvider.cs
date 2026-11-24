using HiveMind.Server.Services;

namespace HiveMind.Server.Domain.Scheduler.DataProvider;

public class ScheduleDataProvider(
    IQueryService queryService,
    IMediaItemService mediaItemService,
    IProgramStrategyService programStrategyService,
    ISchedulingService schedulingService,
    ILibraryService libraryService,
    ITransitionTemplateService transitionTemplateService,
    IProgramRotationService programRotationService
    )
{
    public IQueryService Queries { get; } = queryService;
    public IMediaItemService MediaItems { get; } = mediaItemService;
    public IProgramStrategyService Programs { get; } = programStrategyService;
    public ISchedulingService Schedules {get;} = schedulingService;
    public ILibraryService Libraries {get;} = libraryService;
    public ITransitionTemplateService TransitionTemplates {get;} = transitionTemplateService;
    public IProgramRotationService programRotationService {get;} = programRotationService;
 }
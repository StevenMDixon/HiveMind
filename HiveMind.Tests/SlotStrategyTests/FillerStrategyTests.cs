using HiveMind.Server.Domain.Scheduler;
using HiveMind.Server.Domain.Scheduler.DataProvider;
using HiveMind.Server.Domain.Scheduler.SlotStrategy;
using HiveMind.Server.Domain.Scheduler.Sources;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using NSubstitute;

namespace HiveMind.Server.Tests.SlotStrategyTests;

public class FillerStrategyTests
{
    public GenerationContext getContext() {
        var genContext = new GenerationContext();
        
        var mockedQueryService = Substitute.For<IQueryService>();
        mockedQueryService.GetMediaItemsByQueryId(Arg.Any<int>()).Returns(mediaItemsData());
        mockedQueryService.GetMediaItemsByQuery(Arg.Any<Query>()).Returns(mediaItemsData());

        var mockedMediaItemService = Substitute.For<IMediaItemService>();
        var mockedProgramService = Substitute.For<IProgramStrategyService>();
        var mockedSchedulingService = Substitute.For<ISchedulingService>();
        var mockedLibraryService = Substitute.For<ILibraryService>();
        var mockedTransitionTemplateService = Substitute.For<ITransitionTemplateService>();
        var mockedProgramRotationService = Substitute.For<IProgramRotationService>();

        var scheduleDataProvider = new ScheduleDataProvider(
            mockedQueryService,
            mockedMediaItemService,
            mockedProgramService,
            mockedSchedulingService,
            mockedLibraryService,
            mockedTransitionTemplateService,
            mockedProgramRotationService
        );

        genContext.ScheduleDataProvider = scheduleDataProvider;

        return genContext;
    }
    
    // public IDataSource source => new Mock<IDataSource>().Object;

    public List<MediaItem> mediaItemsData()
    {
        return new List<MediaItem>
        {
            new MediaItem() {Title="Test", Duration = 5 * 60 * 1000},
            new MediaItem() {Title="Test", Duration = 6 * 60 * 1000},
            new MediaItem() {Title="Test", Duration = 1 * 60 * 1000},
            new MediaItem() {Title="Test", Duration = 4 * 60 * 1000}
        };
    }

    [Fact]
    public void FillerStrategy_ShouldFillSlotsCorrectly()
    {
        var context = getContext();

        var strat = new FillerStrategy();

        var source = new CustomDataSource() {Queries = ["Title, Equals, Test"]};

        var results = strat.Generate(context, source, new List<string>(), 10 * 60 * 1000);

        Assert.NotNull(results);
        Assert.All(results, item => Assert.IsType<GenerationResultItem>(item));
    }
}


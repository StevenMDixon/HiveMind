using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Domain.Scheduler.SelectionStrategy;
using HiveMind.Server.Domain.Scheduler.splitter;
using HiveMind.Server.Endpoints.Enums;
using HiveMind.Server.Entities;

namespace HiveMind.Server.Domain.Scheduler.Nodes;

public class ProgramGenerator
{
    public int Duration { get; set; }

    public string ShowName { get; set; } = "";

    public string[] Queries { get; set; } = [];

    public SplitterStrategy SplitStrategy { get; set; }

    public int SplitInterval { get; set; } = 1;

    public RetreiverType RetrievalSetting { get; set; }

    public SelectionPolicy ContentSelection { get; set; } = SelectionPolicy.Single;

    public bool UseBlockName { get; set; } = false;

    public IEnumerable<GenerationResultItem> Generate(GenerationContext context)
    {
        var programResults = new List<GenerationResultItem>();

        var selector = SelectionStrategyResolver.Resolve(ContentSelection);

        var selectedMedia = selector.Generate(context, Queries.ToList(), Duration, RetrievalSetting, ShowName);

        if (selectedMedia.Any())
        {
            programResults.AddRange(selectedMedia.SelectMany(x => Split(new GenerationResultItem { MediaItem = x, StartTime = 0, EndTime = x.Duration })));
        }

        return programResults;
    }

    private List<GenerationResultItem> Split(GenerationResultItem node)
    {
        var results = new List<GenerationResultItem>();

        var splitter = SplitStrategyResolver.Resolve(SplitStrategy);

        results.AddRange(splitter.Generate(node, SplitInterval));

        if (!results.Any()) results.Add(node);

        return results;
    }
}

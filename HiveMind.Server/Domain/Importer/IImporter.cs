using HiveMind.Server.Services;

namespace HiveMind.Server.Domain.Importer
{
    public interface IImporter
    {
        public List<VideoMeta> Generate(List<string> files, string mountedPath, string libraryPath, string defaultTags, ShowService showService, TagsService tagService, QueryService queryService);
    }
}

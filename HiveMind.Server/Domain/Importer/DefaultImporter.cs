using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using System.Diagnostics;

namespace HiveMind.Server.Domain.Importer;

public class DefaultImporter : IImporter
{
    public List<VideoMeta> Generate(List<string> files, string mountedPath, string libraryPath, ShowService showService, TagsService tagService, QueryService queryService)
    {
        var results = new List<VideoMeta>();

        var tagDict = tagService.GetAllTags().ToDictionary(t => t.TagName, t => t);

        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var normalizedFile = file.Replace(mountedPath, String.Empty);
            var fileNameWithExt = Path.GetFileName(file);

            var tags = file.Replace(mountedPath + libraryPath, String.Empty).Split('/').ToList().Where(x => x != "" && x != fileNameWithExt).ToList();

            var mappedTags = new List<Tags>();

            foreach (var tag in tags)
            {
                // Check if already in dictionary
                if (tagDict.ContainsKey(tag))
                {
                    mappedTags.Add(tagDict[tag]);
                }
                else
                {
                    var newTag = new Tags { TagName = tag };
                    tagDict.Add(tag, newTag);
                    mappedTags.Add(newTag);
                }
            }

            results.Add(new VideoMeta
            {
                Name = fileName,
                Path = normalizedFile,
                FullPath = file,
                Tags = mappedTags
            });

        }

        return results;
    }
}

using HiveMind.Server.Domain.Enums;
using HiveMind.Server.Entities;
using HiveMind.Server.Services;
using System.Diagnostics;

namespace HiveMind.Server.Domain.Importer;

public class DefaultImporter : IImporter
{
    public List<VideoMeta> Generate(List<string> files, string mountedPath, string libraryPath, string defaultTags, ShowService showService, TagsService tagService, QueryService queryService)
    {
        var results = new List<VideoMeta>();

        var tagDict = tagService.GetAllTags().ToDictionary(t => t.Name, t => t);

        var prePath = Path.Join(mountedPath.TrimEnd('/'), libraryPath.TrimStart('/'));

        foreach (var file in files)
        {
            var fileNameWithExt = Path.GetFileName(file);

            var localFilePath = mountedPath == "/" ? file: file.Replace(mountedPath, "");

            var extractedTags = MediaTagExtractor.ExtractTags(fileNameWithExt, prePath, file, defaultTags);

            var mappedTags = new List<Tags>();

            foreach (var tag in extractedTags)
            {
                // Check if already in dictionary
                if (tagDict.ContainsKey(tag))
                {
                    mappedTags.Add(tagDict[tag]);
                }
                else
                {
                    var newTag = new Tags { Name = tag };
                    tagDict.Add(tag, newTag);
                    mappedTags.Add(newTag);
                }
            }

            results.Add(new VideoMeta
            {
                Name = Path.GetFileNameWithoutExtension(file),
                Path = localFilePath,
                FullPath = file,
                Tags = mappedTags
            });

        }

        return results;
    }
}

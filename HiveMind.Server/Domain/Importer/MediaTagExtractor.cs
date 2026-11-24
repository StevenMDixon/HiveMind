using System.Text.RegularExpressions;
namespace HiveMind.Server.Domain.Importer;

public static partial class MediaTagExtractor
{
    public static HashSet<String> ExtractTags(string fileName, string importPath, string fullPath, string defaultTags)
    {
        var results = new HashSet<string>();
        
        // add default tags
        if(defaultTags != string.Empty) results.UnionWith(defaultTags.Split(",").Select(x => x.Trim()));

        // get tags from path
        var resultingPath = fullPath.Replace(importPath, "").Replace(fileName, "");

        results.UnionWith(resultingPath.Split("/").Where(x => x != string.Empty));

        // get tags from file name [year, network] or (year) or just year
        var years = YearRegex().Matches(fileName);

        if(years.Any()) results.UnionWith(years.Select(x => x.Value));

        return results;
    }

    [GeneratedRegex(@"(?:20|19)[0-9]{2}")]
    private static partial Regex YearRegex();
    
}
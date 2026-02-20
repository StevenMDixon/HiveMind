using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Entities;

[Index(nameof(TagName), IsUnique = true)]
public class Tags
{
    [Key]
    public int TagId { get; set; }
    public string TagName { get; set; } = string.Empty;
    public ICollection<MediaItem> MediaItem { get; set; } = null!;
}

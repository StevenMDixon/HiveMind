using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Tags
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<MediaItem> MediaItem { get; set; } = null!;
}

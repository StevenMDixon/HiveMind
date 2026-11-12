using System.ComponentModel.DataAnnotations;
using HiveMind.Server.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HiveMind.Server.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Tags
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TagCategory Category { get; set; } = TagCategory.Other;
    public ICollection<MediaItem> MediaItem { get; set; } = null!;
}

namespace HiveMind.Server.Domain.Enums;

public enum TagCategory
{
    Other,
    [Required(true)]
    Network,
    [Required(true)]
    Time_Slot,
    Genre,
    Audience,
    Era,
    [Required(true)]
    Programming_Block,
    Content_Rating,
    Holiday,
    Language,
    Theme,
    [Required(true)]
    Airing_Year
}

[AttributeUsage(AttributeTargets.Field)]
public class Required : Attribute
{
    public bool _Required { get; } = true;

    public Required(bool required)
    {
        _Required = required;
    }
}
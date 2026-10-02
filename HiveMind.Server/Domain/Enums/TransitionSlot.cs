using System.Text.Json.Serialization;

namespace HiveMind.Server.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransitionSlot
{

    InBump,
    OutBump,
    Ident,
    Commercial =3,
    Filler,
    Promo,
    BlockIntro = 6,
    BlockOutro,
    ShowIntro,
    Bump,
    UpNext,
    Media,
    CutFiller,
    ShowOutro
}


[AttributeUsage(AttributeTargets.Field)]
public class Weight(string description, int maxUsers) : Attribute
{
    public string Description { get; } = description;
    public int MaxUsers { get; } = maxUsers;
}
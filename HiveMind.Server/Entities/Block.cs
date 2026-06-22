namespace HiveMind.Server.Entities;

public class Block
{
    public int BlockId { get; set; }
    public string BlockName { get; set; } = null!;
    public string Logo { get; set; } = string.Empty;
    public ICollection<QueryLineupItem>? Queries { get; set; }
    

    /*
    *  Blocks are a template almost exactly like a lineup Item 
    *  Blocks are made up of an Intro, Outro, Shows, Bumpers, Interstitials, and Promos
    *  Blocks have branding
    */
}

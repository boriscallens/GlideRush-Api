using System.Diagnostics.CodeAnalysis;

namespace GlideRush.Leaderboard.Domain;

public class BoardEntry
{
    public Guid Id { get; set; }
    public Guid PlayfabId { get; set; }
    public uint Time { get; set; }


    public DateTime CreatedUtc { get; set; }
    
    public Board Board { get; set; }
}
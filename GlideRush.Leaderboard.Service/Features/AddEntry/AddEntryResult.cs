using GlideRush.Leaderboard.Service.Dto;

namespace GlideRush.Leaderboard.Service.AddEntry;

public class AddEntryResult
{
    public Guid LeaderBoardId { get; set; }
    public BoardEntryDto Entry { get; set; }
}
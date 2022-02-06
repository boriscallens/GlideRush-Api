using GlideRush.Leaderboard.Service.Dto;

namespace GlideRush.Leaderboard.Service.AddEntry;

public class AddHandlerResult
{
    public Guid LeaderBoardId { get; set; }
    public BoardEntryDto Entry { get; set; }
}
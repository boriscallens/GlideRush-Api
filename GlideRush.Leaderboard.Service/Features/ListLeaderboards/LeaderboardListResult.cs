using GlideRush.Leaderboard.Service.Dto;

namespace GlideRush.Leaderboard.Service.ListLeaderboards;

public class LeaderboardListResult
{
    public List<BoardDto> Boards { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int PageCount { get; set; }
}
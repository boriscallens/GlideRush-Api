using MediatR;

namespace GlideRush.Leaderboard.Service.ListLeaderboards;

public class ListLeaderboardQuery: IRequest<LeaderboardListResult>
{
    public string? SearchTerm { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
}
using MediatR;

namespace GlideRush.Leaderboard.Service.CreateLeaderboard;

public class CreateLeaderboardCommand: IRequest<CreateLeaderboardResult>
{
    public string Name { get; set; }
}
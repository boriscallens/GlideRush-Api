using MediatR;

namespace GlideRush.Leaderboard.Service.CreateLeaderboard;

public class CreateLeaderboardHandler : IRequestHandler<CreateLeaderboardCommand, CreateLeaderboardResult>
{
    public async Task<CreateLeaderboardResult> Handle(CreateLeaderboardCommand request, CancellationToken cancellationToken)
    {
        var leaderboard = new Domain.Board();

        return await Task.FromResult(new CreateLeaderboardResult
        {
            LeaderBoardId = leaderboard.Id
        });
    }
}
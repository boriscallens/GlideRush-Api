using GlideRush.Leaderboard.Persistence;

using MediatR;

namespace GlideRush.Leaderboard.Service.CreateLeaderboard;

public class CreateLeaderboardHandler : IRequestHandler<CreateLeaderboardCommand, CreateLeaderboardResult>
{
    private readonly LeaderboardContext _context;

    public CreateLeaderboardHandler(LeaderboardContext context)
    {
        _context = context;
    }

    public async Task<CreateLeaderboardResult> Handle(CreateLeaderboardCommand request, CancellationToken cancellationToken)
    {
        var leaderboard = new Domain.Board
        {
            Name = request.Name
        };
        await _context.Boards.AddAsync(leaderboard, cancellationToken);

        return new CreateLeaderboardResult
        {
            LeaderBoardId = leaderboard.Id
        };
    }
}
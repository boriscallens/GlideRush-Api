using GlideRush.Leaderboard.Persistence;
using GlideRush.Leaderboard.Service.CreateLeaderboard;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace GlideRush.Leaderboard.Service.Test;

public class CreateBoardShould: IClassFixture<UnitTestFixture>
{
    private readonly LeaderboardContext _context;

    public CreateBoardShould(UnitTestFixture fixture)
    {
        _context = fixture.GetRequiredService<LeaderboardContext>();
    }

    [Fact]
    public async Task PersistNewBoard()
    {
        var cmd = new CreateLeaderboardCommand { Name = "Coolest track in the game" };
        var handler = new CreateLeaderboardHandler(_context);

        var result = await handler.Handle(cmd, CancellationToken.None);

        Assert.NotEqual(default, result.LeaderBoardId);
    }
}
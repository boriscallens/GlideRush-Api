using AutoMapper;

using GlideRush.Leaderboard.Domain;
using GlideRush.Leaderboard.Persistence;
using GlideRush.Leaderboard.Service.AddEntry;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace GlideRush.Leaderboard.Service.Test;

public class AddEntryShould : IClassFixture<UnitTestFixture>
{
    private readonly LeaderboardContext _context;
    private readonly IMapper _mapper;

    public AddEntryShould(UnitTestFixture fixture)
    {
        _mapper = fixture.GetRequiredService<IMapper>();
        _context = fixture.GetRequiredService<LeaderboardContext>();
    }

    [Fact]
    public async Task AddEntryToABoard()
    {
        Board board = new() { Id = Guid.NewGuid() };
        await _context.Boards.AddAsync(board, CancellationToken.None);

        var cmd = new AddEntryCommand { BoardId = board.Id, Time = 69, PlayfabId = Guid.NewGuid() };
        var handler = new AddEntryHandler(_context, _mapper);
        var result = await handler.Handle(cmd, CancellationToken.None);

        Assert.Equal(board.Id, result.LeaderBoardId);
        Assert.NotEqual(default, result.Entry.Id);
        Assert.NotEqual(default, result.Entry.CreatedUtc);
    }
}
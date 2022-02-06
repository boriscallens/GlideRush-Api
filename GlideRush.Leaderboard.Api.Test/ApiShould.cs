using GlideRush.Leaderboard.Api.Controllers;
using GlideRush.Leaderboard.Service.AddEntry;
using GlideRush.Leaderboard.Service.CreateLeaderboard;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Xunit;

namespace GlideRush.Leaderboard.Api.Test
{
    public class ApiShould : IClassFixture<IntegrationTestFixture>
    {
        private readonly LeaderboardController _leaderBoardController;
        private readonly BoardEntryController _boardEntryController;

        public ApiShould(IntegrationTestFixture fixture)
        {
            var mediator = fixture.GetRequiredService<IMediator>();
            var logger = fixture.GetRequiredService<ILogger<LeaderboardController>>();
            ILogger<BoardEntryController> logger2 = fixture.GetRequiredService<ILogger<BoardEntryController>>();
            _leaderBoardController = new LeaderboardController(logger, mediator);
            _boardEntryController = new BoardEntryController(logger2, mediator);
        }

        [Theory]
        [InlineData(10, 1_000_000)]
        public async void AllowManyEntriesForMultipleBoards(int numberOfBoards, int numberOfPlayers)
        {
            var boardTasks = Enumerable.Range(0, numberOfBoards)
                .Select(_ => new CreateLeaderboardCommand { Name = Guid.NewGuid().ToString() })
                .Select(cmd => _leaderBoardController.Create(cmd))
                .ToArray();

            await Task.WhenAll(boardTasks);
            var boardIds = boardTasks.Select(task => task.Result.LeaderBoardId).ToArray();
            var playerIds = Enumerable.Range(0, numberOfPlayers).Select(idx => Guid.NewGuid());

            var entryCommands = boardIds.SelectMany(boardId => playerIds, (boardId, playerId) =>
                        new AddEntryCommand
                        {
                            BoardId = boardId,
                            PlayfabId = playerId,
                            Time = 10000
                        });

            var entryTasks = entryCommands
                .Select(cmd => _boardEntryController.Add(cmd))
                .ToArray();

            await Task.WhenAll(entryTasks);
            var entriesPerBoard = entryTasks
                .Select(task => task.Result)
                .ToLookup(task => task.LeaderBoardId);

            Assert.Equal(numberOfBoards, entriesPerBoard.Count);
            Assert.All(boardIds, (boardId) => Assert.Equal(numberOfPlayers, entriesPerBoard[boardId].Count()));
        }
    }
}

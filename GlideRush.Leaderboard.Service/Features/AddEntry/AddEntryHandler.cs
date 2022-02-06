using AutoMapper;
using GlideRush.Leaderboard.Domain;
using GlideRush.Leaderboard.Domain.Exceptions;
using GlideRush.Leaderboard.Persistence;
using GlideRush.Leaderboard.Service.Dto;
using MediatR;

namespace GlideRush.Leaderboard.Service.AddEntry;

public class AddEntryHandler : IRequestHandler<AddHandlerCommand, AddHandlerResult>
{
    private readonly LeaderboardContext _context;
    private readonly IMapper _mapper;

    public AddEntryHandler(LeaderboardContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AddHandlerResult> Handle(AddHandlerCommand request, CancellationToken cancellationToken)
    {
        var board = await _context.Boards.FindAsync(new object[] { request.BoardId }, cancellationToken);

        if (board == null)
        {
            throw new NotFoundException(request.BoardId, nameof(Leaderboard));
        }

        var entry = new BoardEntry
        {
            Board = board,
            CreatedUtc = DateTime.UtcNow,
            PlayfabId = request.PlayfabId,
            Time = request.Time
        };

        return await Task.FromResult(new AddHandlerResult
        {
            Entry = _mapper.Map<BoardEntry, BoardEntryDto>(entry)
        });
    }
}
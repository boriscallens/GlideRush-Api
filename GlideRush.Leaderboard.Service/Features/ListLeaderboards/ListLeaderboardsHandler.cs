
using AutoMapper;

using GlideRush.Leaderboard.Domain;
using GlideRush.Leaderboard.Persistence;
using GlideRush.Leaderboard.Service.Dto;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace GlideRush.Leaderboard.Service.ListLeaderboards;

public class ListLeaderboardsHandler : IRequestHandler<ListLeaderboardQuery, LeaderboardListResult>
{
    private readonly LeaderboardContext _context;
    private readonly IMapper _mapper;

    public ListLeaderboardsHandler(LeaderboardContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LeaderboardListResult> Handle(ListLeaderboardQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Board> boardQuery = _context.Boards;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            boardQuery = boardQuery.Where(board => board.Name.Contains(request.SearchTerm));
        }

        var count = await boardQuery.CountAsync(cancellationToken);
        var pageSize = request.PageSize ?? 50;
        var pageCount = (int)Math.Ceiling((decimal)count / pageSize);
        var pageNumber = Math.Min(request.PageNumber ?? 1, pageCount);
        var skipCount = Math.Max(pageSize * (pageNumber - 1), 0);

        var boards = await boardQuery
            .OrderBy(board => board.Name)
            .Skip(skipCount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        var boardDtos = _mapper.Map<List<BoardDto>>(boards);

        return new LeaderboardListResult
        {
            Boards = boardDtos,
            PageSize = pageSize,
            PageNumber = pageNumber,
            PageCount = pageCount
        };
    }
}
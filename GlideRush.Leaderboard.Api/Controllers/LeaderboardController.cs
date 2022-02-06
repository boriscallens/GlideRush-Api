using GlideRush.Leaderboard.Service.CreateLeaderboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlideRush.Leaderboard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly ILogger<LeaderboardController> _logger;
    private readonly IMediator _mediator;

    public LeaderboardController(ILogger<LeaderboardController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost(Name = "Create")]
    public async Task<CreateLeaderboardResult> Create([FromBody]CreateLeaderboardCommand command)
    {
        return await _mediator.Send(command);
    }
}
using GlideRush.Leaderboard.Service.AddEntry;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GlideRush.Leaderboard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardEntryController : ControllerBase
{
    private readonly ILogger<BoardEntryController> _logger;
    private readonly IMediator _mediator;

    public BoardEntryController(ILogger<BoardEntryController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost(Name = "Add")]
    public async Task<AddEntryResult> Add([FromBody] AddEntryCommand command)
    {
        return await _mediator.Send(command);
    }
}
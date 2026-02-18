using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResenhaFc.API.Features.Players.Create;

namespace ResenhaFc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlayersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayerRequest request)
    {
        var result = await _mediator.Send(request.ToCommand());

        return CreatedAtAction(
            nameof(Create),
            new { id = result.Id },
            CreatePlayerResponse.From(result)
        );
    }
}

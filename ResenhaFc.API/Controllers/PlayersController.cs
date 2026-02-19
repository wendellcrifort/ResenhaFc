using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResenhaFc.API.Features.Players.Create;
using ResenhaFc.Application.Features.Players.Create;
using ResenhaFc.Application.Features.Players.GetByEmail;
using ResenhaFc.Application.Features.Players.GetByGroupId;
using ResenhaFc.Application.Features.Players.GetById;

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
            nameof(GetById),
            new { id = result.Id },
            CreatePlayerResponse.From(result)
        );
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetPlayerByIdQuery(id));

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("by-email")]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        var result = await _mediator.Send(new GetPlayerByEmailQuery(email));

        if (result is null)
            return NotFound();

        return Ok(result);
    }
    
    [HttpGet("by-group/{groupId:int}")]
    public async Task<IActionResult> GetByGroup(int groupId)
    {
        var result = await _mediator.Send(new GetPlayersByGroupIdQuery(groupId));
        return Ok(result);
    }
}

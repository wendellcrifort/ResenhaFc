using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResenhaFc.API.Features.Groups.Create;
using ResenhaFc.Application.Features.Groups.Create;
using ResenhaFc.Application.Features.Groups.GetByPlayerId;

namespace ResenhaFc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequest request)
    {
        var result = await _mediator.Send(request.ToCommand());

        return CreatedAtAction(
            nameof(Create),
            new { id = result.Id },
            CreateGroupResponse.From(result)
        );
    }
    
    [HttpGet]
    public async Task<IActionResult> GetByPlayerId([FromQuery] int playerId)
    {
        if (playerId <= 0)
            return BadRequest("playerId is required.");

        var result = await _mediator.Send(new GetGroupsByPlayerIdQuery(playerId));
        return Ok(result);
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using ResenhaFc.Application.Common.Interfaces;

namespace ResenhaFc.Application.Features.Players.GetByGroupId;

public class GetPlayersByGroupIdHandler
    : IRequestHandler<GetPlayersByGroupIdQuery, List<GetPlayersByGroupIdResult>>
{
    private readonly IApplicationDbContext _context;

    public GetPlayersByGroupIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetPlayersByGroupIdResult>> Handle(
        GetPlayersByGroupIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.GroupPlayers
            .AsNoTracking()
            .Where(gp => gp.GroupId == request.GroupId)
            .Select(gp => new GetPlayersByGroupIdResult
            {
                Id = gp.Player.Id,
                Name = gp.Player.Name,
                Email = gp.Player.Email,
                Role = gp.Role,
                ScoreAvg = gp.ScoreAvg
            })
            .ToListAsync(cancellationToken);
    }
}

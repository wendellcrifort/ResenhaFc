using MediatR;
using Microsoft.EntityFrameworkCore;
using ResenhaFc.Application.Common.Interfaces;
using ResenhaFc.Application.Common.Models;

namespace ResenhaFc.Application.Features.Players.GetById;

public class GetPlayerByIdHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDto?>
{
    private readonly IApplicationDbContext _context;

    public GetPlayerByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PlayerDto?> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Players
            .AsNoTracking()
            .Where(p => p.Id == request.Id)
            .Select(p => new PlayerDto
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                DominantFoot = p.DominantFoot,
                Type = p.Type
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}

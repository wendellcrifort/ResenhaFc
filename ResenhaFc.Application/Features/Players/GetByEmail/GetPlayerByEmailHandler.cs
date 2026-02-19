using MediatR;
using Microsoft.EntityFrameworkCore;
using ResenhaFc.Application.Common.Interfaces;
using ResenhaFc.Application.Common.Models;

namespace ResenhaFc.Application.Features.Players.GetByEmail;

public class GetPlayerByEmailHandler : IRequestHandler<GetPlayerByEmailQuery, PlayerDto?>
{
    private readonly IApplicationDbContext _context;

    public GetPlayerByEmailHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PlayerDto?> Handle(GetPlayerByEmailQuery request, CancellationToken cancellationToken)
    {
        var email = (request.Email ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(email))
            return null;

        return await _context.Players
            .AsNoTracking()
            .Where(p => p.Email == email)
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

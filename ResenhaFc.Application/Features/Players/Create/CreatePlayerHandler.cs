using MediatR;
using Microsoft.EntityFrameworkCore;
using ResenhaFc.Application.Common.Interfaces;
using ResenhaFc.Domain.Entities;

namespace ResenhaFc.Application.Features.Players.Create;

public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, CreatePlayerResult>
{
    private readonly IApplicationDbContext _context;

    public CreatePlayerHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreatePlayerResult> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email is required.");

        if (string.IsNullOrWhiteSpace(request.Phone))
            throw new ArgumentException("Phone is required.");

        var emailExists = await _context.Players
            .AnyAsync(p => p.Email == request.Email, cancellationToken);

        if (emailExists)
            throw new InvalidOperationException("Email already exists.");

        var player = new Player(
            request.Name.Trim(),
            request.Email.Trim(),
            request.Phone.Trim(),
            request.DominantFoot,
            request.Type
        );

        _context.Players.Add(player);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreatePlayerResult
        {
            Id = player.Id,
            Name = player.Name,
            Email = player.Email,
            Phone = player.Phone,
            DominantFoot = player.DominantFoot,
            Type = player.Type
        };
    }
}

using Microsoft.EntityFrameworkCore;
using ResenhaFc.Domain.Entities;

namespace ResenhaFc.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Player> Players { get; }
    DbSet<Group> Groups { get; }
    DbSet<GroupPlayer> GroupPlayers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

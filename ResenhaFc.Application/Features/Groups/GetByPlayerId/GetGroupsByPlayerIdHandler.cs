using MediatR;
using Microsoft.EntityFrameworkCore;
using ResenhaFc.Application.Common.Interfaces;
using ResenhaFc.Application.Common.Models;
using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Application.Features.Groups.GetByPlayerId;

public class GetGroupsByPlayerIdHandler : IRequestHandler<GetGroupsByPlayerIdQuery, List<PlayerGroupDto>>
{
    private readonly IApplicationDbContext _context;

    public GetGroupsByPlayerIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PlayerGroupDto>> Handle(GetGroupsByPlayerIdQuery request, CancellationToken cancellationToken)
    {
        if (request.PlayerId <= 0)
            throw new ArgumentException("PlayerId is required.");

        var playerId = request.PlayerId;
        
        var items = await _context.GroupPlayers
            .AsNoTracking()
            .Where(gp => gp.PlayerId == playerId &&
                         (gp.Status == GroupMemberStatus.Active || gp.Status == GroupMemberStatus.Pending))
            .Select(gp => new PlayerGroupDto
            {
                GroupId = gp.Group.Id,
                Name = gp.Group.Name,

                IsAdmin = gp.MemberType == GroupMemberType.Admin,
                MembershipStatus = gp.Status,

                IsRecurring = gp.Group.IsRecurring,
                WeekDay = gp.Group.WeekDay,
                StartTime = gp.Group.StartTime.ToString("HH:mm"),
                EndTime = gp.Group.EndTime.ToString("HH:mm"),
                GameDate = gp.Group.GameDate.HasValue ? gp.Group.GameDate.Value.ToString("yyyy-MM-dd") : null,

                PlayersLimitPerGame = gp.Group.PlayersLimitPerGame,

                CourtName = gp.Group.CourtName,
                FullAddress = gp.Group.FullAddress,

                MonthlyFee = gp.Group.MonthlyFee,
                SingleGameFee = gp.Group.SingleGameFee,

                VestColor = gp.Group.VestColor
            })
            .ToListAsync(cancellationToken);
      
        var result = items
            .OrderBy(x => x.IsAdmin ? 0 : (x.MembershipStatus == GroupMemberStatus.Active ? 1 : 2))
            .ThenBy(x => x.Name)
            .ToList();

        return result;
    }
}

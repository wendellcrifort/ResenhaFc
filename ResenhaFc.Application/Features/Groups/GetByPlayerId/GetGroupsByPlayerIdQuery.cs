using MediatR;
using ResenhaFc.Application.Common.Models;

namespace ResenhaFc.Application.Features.Groups.GetByPlayerId;

public record GetGroupsByPlayerIdQuery(int PlayerId) : IRequest<List<PlayerGroupDto>>;

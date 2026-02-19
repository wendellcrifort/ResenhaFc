using MediatR;

namespace ResenhaFc.Application.Features.Players.GetByGroupId;

public record GetPlayersByGroupIdQuery(int GroupId) : IRequest<List<GetPlayersByGroupIdResult>>;

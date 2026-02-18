using MediatR;
using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Application.Features.Players.Create;

public record CreatePlayerCommand(
    string Name,
    string Email,
    string Phone,
    DominantFoot DominantFoot,
    PlayerType Type
) : IRequest<CreatePlayerResult>;

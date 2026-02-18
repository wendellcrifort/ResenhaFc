using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Application.Features.Players.Create;

public class CreatePlayerResult
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;

    public DominantFoot DominantFoot { get; init; }
    public PlayerType Type { get; init; }
}

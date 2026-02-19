using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Application.Features.Players.GetByGroupId;

public class GetPlayersByGroupIdResult
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

    public PlayerRole Role { get; init; }
    public decimal ScoreAvg { get; init; }
}

using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Application.Common.Models;

public class PlayerGroupDto
{
    public int GroupId { get; init; }
    public string Name { get; init; } = string.Empty;

    public bool IsAdmin { get; init; }
    public GroupMemberStatus MembershipStatus { get; init; }

    public bool IsRecurring { get; init; }
    public WeekDay? WeekDay { get; init; }
    public string StartTime { get; init; } = string.Empty; // "HH:mm"
    public string EndTime { get; init; } = string.Empty;   // "HH:mm"
    public string? GameDate { get; init; }                 // "yyyy-MM-dd"

    public int PlayersLimitPerGame { get; init; }

    public string CourtName { get; init; } = string.Empty;
    public string FullAddress { get; init; } = string.Empty;

    public decimal MonthlyFee { get; init; }
    public decimal SingleGameFee { get; init; }

    public string VestColor { get; init; } = string.Empty;
}

using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Application.Features.Groups.Create;

public class CreateGroupResult
{
    public int Id { get; init; }
    public int AdminId { get; init; }

    public string Name { get; init; } = string.Empty;

    public bool IsRecurring { get; init; }
    public WeekDay? WeekDay { get; init; }
    public string StartTime { get; init; } = string.Empty;
    public string EndTime { get; init; } = string.Empty;
    public string? GameDate { get; init; }

    public int PlayersLimitPerGame { get; init; }

    public string CourtName { get; init; } = string.Empty;
    public string FullAddress { get; init; } = string.Empty;

    public decimal MonthlyFee { get; init; }
    public decimal SingleGameFee { get; init; }

    public string PixKey { get; init; } = string.Empty;
    public string BankAccountHolderName { get; init; } = string.Empty;
    public string BankName { get; init; } = string.Empty;

    public string VestColor { get; init; } = string.Empty;
}

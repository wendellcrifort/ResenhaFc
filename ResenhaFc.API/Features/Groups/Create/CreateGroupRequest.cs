using ResenhaFc.Application.Features.Groups.Create;
using ResenhaFc.Domain.Enums;

namespace ResenhaFc.API.Features.Groups.Create;

public class CreateGroupRequest
{
    public int AdminId { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsRecurring { get; set; }
    public WeekDay? WeekDay { get; set; }

    // "HH:mm"
    public string StartTime { get; set; } = string.Empty;

    // "HH:mm"
    public string EndTime { get; set; } = string.Empty;

    // "yyyy-MM-dd" (required if IsRecurring = false)
    public string? GameDate { get; set; }

    public int PlayersLimitPerGame { get; set; }

    public string CourtName { get; set; } = string.Empty;
    public string FullAddress { get; set; } = string.Empty;

    public decimal MonthlyFee { get; set; }
    public decimal SingleGameFee { get; set; }

    public string PixKey { get; set; } = string.Empty;

    public string BankAccountHolderName { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;

    public string VestColor { get; set; } = string.Empty;

    public CreateGroupCommand ToCommand()
    {
        return new CreateGroupCommand(
            AdminId,
            Name,
            IsRecurring,
            WeekDay,
            StartTime,
            EndTime,
            GameDate,
            PlayersLimitPerGame,
            CourtName,
            FullAddress,
            MonthlyFee,
            SingleGameFee,
            PixKey,
            BankAccountHolderName,
            BankName,
            VestColor
        );
    }
}

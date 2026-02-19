using ResenhaFc.Application.Features.Groups.Create;
using ResenhaFc.Domain.Enums;

namespace ResenhaFc.API.Features.Groups.Create;

public class CreateGroupResponse
{
    public int Id { get; set; }
    public int AdminId { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsRecurring { get; set; }
    public WeekDay? WeekDay { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
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

    public static CreateGroupResponse From(CreateGroupResult result)
    {
        return new CreateGroupResponse
        {
            Id = result.Id,
            AdminId = result.AdminId,
            Name = result.Name,

            IsRecurring = result.IsRecurring,
            WeekDay = result.WeekDay,
            StartTime = result.StartTime,
            EndTime = result.EndTime,
            GameDate = result.GameDate,

            PlayersLimitPerGame = result.PlayersLimitPerGame,

            CourtName = result.CourtName,
            FullAddress = result.FullAddress,

            MonthlyFee = result.MonthlyFee,
            SingleGameFee = result.SingleGameFee,

            PixKey = result.PixKey,
            BankAccountHolderName = result.BankAccountHolderName,
            BankName = result.BankName,

            VestColor = result.VestColor
        };
    }
}

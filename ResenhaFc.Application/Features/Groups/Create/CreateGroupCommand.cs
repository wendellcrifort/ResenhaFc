using MediatR;
using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Application.Features.Groups.Create;

public record CreateGroupCommand(
    int AdminId,
    string Name,

    bool IsRecurring,
    WeekDay? WeekDay,
    string StartTime,   // "HH:mm"
    string EndTime,     // "HH:mm"
    string? GameDate,   // "yyyy-MM-dd" (required if IsRecurring = false)

    int PlayersLimitPerGame,
    string CourtName,
    string FullAddress,

    decimal MonthlyFee,
    decimal SingleGameFee,

    string PixKey,
    string BankAccountHolderName,
    string BankName,

    string VestColor
) : IRequest<CreateGroupResult>;

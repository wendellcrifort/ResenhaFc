using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Domain.Entities;

public class Group
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;
    
    public int AdminId { get; private set; }

    public bool IsRecurring { get; private set; }
    public WeekDay? WeekDay { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public DateOnly? GameDate { get; private set; }

    public int PlayersLimitPerGame { get; private set; }
    
    public string CourtName { get; private set; } = string.Empty;
    public string FullAddress { get; private set; } = string.Empty;
    
    public decimal MonthlyFee { get; private set; }
    public decimal SingleGameFee { get; private set; }

    public string PixKey { get; private set; } = string.Empty;
    public string BankAccountHolderName { get; private set; } = string.Empty;
    public string BankName { get; private set; } = string.Empty;

    public string VestColor { get; private set; } = string.Empty;

    public ICollection<GroupPlayer> Players { get; private set; } = new List<GroupPlayer>();

    private Group() { } 

    public Group(
        string name,
        int adminId,
        bool isRecurring,
        WeekDay? weekDay,
        TimeOnly startTime,
        TimeOnly endTime,
        DateOnly? gameDate,
        int playersLimitPerGame,
        string courtName,
        string fullAddress,
        decimal monthlyFee,
        decimal singleGameFee,
        string pixKey,
        string bankAccountHolderName,
        string bankName,
        string vestColor)
    {
        Name = name;
        AdminId = adminId;

        IsRecurring = isRecurring;
        WeekDay = weekDay;
        StartTime = startTime;
        EndTime = endTime;
        GameDate = gameDate;

        PlayersLimitPerGame = playersLimitPerGame;

        CourtName = courtName;
        FullAddress = fullAddress;

        MonthlyFee = monthlyFee;
        SingleGameFee = singleGameFee;

        PixKey = pixKey;
        BankAccountHolderName = bankAccountHolderName;
        BankName = bankName;

        VestColor = vestColor;
    }
}

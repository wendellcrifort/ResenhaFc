using MediatR;
using Microsoft.EntityFrameworkCore;
using ResenhaFc.Application.Common.Interfaces;
using ResenhaFc.Domain.Entities;
using ResenhaFc.Domain.Enums;
using System.Globalization;

namespace ResenhaFc.Application.Features.Groups.Create;

public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, CreateGroupResult>
{
    private readonly IApplicationDbContext _context;

    // TODO: move to config later
    private const int LIMITED_GROUP_LIMIT = 3;

    public CreateGroupHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateGroupResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var normalized = ValidateAndNormalizeRequest(request);

        var admin = await LoadAndValidateAdminAsync(normalized.AdminId, cancellationToken);

        var group = BuildGroup(normalized);

        await CreateGroupAndAdminMembershipAsync(group, admin, cancellationToken);

        return ToResult(group);
    }
    
    private static NormalizedCreateGroup ValidateAndNormalizeRequest(CreateGroupCommand request)
    {
        if (request.AdminId <= 0)
            throw new ArgumentException("AdminId is required.");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(request.StartTime))
            throw new ArgumentException("StartTime is required.");

        if (string.IsNullOrWhiteSpace(request.EndTime))
            throw new ArgumentException("EndTime is required.");

        if (request.PlayersLimitPerGame <= 0)
            throw new ArgumentException("PlayersLimitPerGame must be greater than zero.");

        if (string.IsNullOrWhiteSpace(request.CourtName))
            throw new ArgumentException("CourtName is required.");

        if (string.IsNullOrWhiteSpace(request.FullAddress))
            throw new ArgumentException("FullAddress is required.");

        if (request.MonthlyFee < 0)
            throw new ArgumentException("MonthlyFee must be >= 0.");

        if (request.SingleGameFee < 0)
            throw new ArgumentException("SingleGameFee must be >= 0.");

        if (string.IsNullOrWhiteSpace(request.PixKey))
            throw new ArgumentException("PixKey is required.");

        if (string.IsNullOrWhiteSpace(request.BankAccountHolderName))
            throw new ArgumentException("BankAccountHolderName is required.");

        if (string.IsNullOrWhiteSpace(request.BankName))
            throw new ArgumentException("BankName is required.");

        if (string.IsNullOrWhiteSpace(request.VestColor))
            throw new ArgumentException("VestColor is required.");

        var name = request.Name.Trim();
        var courtName = request.CourtName.Trim();
        var fullAddress = request.FullAddress.Trim();
        var pixKey = request.PixKey.Trim();
        var bankAccountHolderName = request.BankAccountHolderName.Trim();
        var bankName = request.BankName.Trim();
        var vestColor = request.VestColor.Trim();

        var startTime = ParseTime(request.StartTime, "StartTime");
        var endTime = ParseTime(request.EndTime, "EndTime");

        if (endTime <= startTime)
            throw new ArgumentException("EndTime must be greater than StartTime.");

        WeekDay? weekDay = request.WeekDay;
        DateOnly? gameDate = null;

        if (request.IsRecurring)
        {
            if (weekDay is null)
                throw new ArgumentException("WeekDay is required when IsRecurring is true.");

            if (!string.IsNullOrWhiteSpace(request.GameDate))
                throw new ArgumentException("GameDate must be empty when IsRecurring is true.");
        }
        else
        {
            if (string.IsNullOrWhiteSpace(request.GameDate))
                throw new ArgumentException("GameDate is required when IsRecurring is false.");

            gameDate = ParseDate(request.GameDate, "GameDate");
            weekDay = null;
        }

        return new NormalizedCreateGroup(
            AdminId: request.AdminId,
            Name: name,
            IsRecurring: request.IsRecurring,
            WeekDay: weekDay,
            StartTime: startTime,
            EndTime: endTime,
            GameDate: gameDate,
            PlayersLimitPerGame: request.PlayersLimitPerGame,
            CourtName: courtName,
            FullAddress: fullAddress,
            MonthlyFee: request.MonthlyFee,
            SingleGameFee: request.SingleGameFee,
            PixKey: pixKey,
            BankAccountHolderName: bankAccountHolderName,
            BankName: bankName,
            VestColor: vestColor
        );
    }

    private static TimeOnly ParseTime(string value, string fieldName)
    {
        if (!TimeOnly.TryParseExact(value.Trim(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
            throw new ArgumentException($"{fieldName} must be in HH:mm format.");

        return parsed;
    }

    private static DateOnly ParseDate(string value, string fieldName)
    {
        if (!DateOnly.TryParseExact(value.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
            throw new ArgumentException($"{fieldName} must be in yyyy-MM-dd format.");

        return parsed;
    }
    
    private async Task<Player> LoadAndValidateAdminAsync(int adminId, CancellationToken cancellationToken)
    {
        var admin = await _context.Players
            .FirstOrDefaultAsync(p => p.Id == adminId, cancellationToken);

        if (admin is null)
            throw new InvalidOperationException("Admin player not found.");

        if (admin.Type != PlayerType.Admin)
            throw new InvalidOperationException("Only Admin players can create groups.");

        if (!admin.CanCreateGroup(LIMITED_GROUP_LIMIT))
            throw new InvalidOperationException("You cannot create more groups with your current plan.");

        return admin;
    }
    
    private static Group BuildGroup(NormalizedCreateGroup data)
    {
        return new Group(
            name: data.Name,
            adminId: data.AdminId,
            isRecurring: data.IsRecurring,
            weekDay: data.WeekDay,
            startTime: data.StartTime,
            endTime: data.EndTime,
            gameDate: data.GameDate,
            playersLimitPerGame: data.PlayersLimitPerGame,
            courtName: data.CourtName,
            fullAddress: data.FullAddress,
            monthlyFee: data.MonthlyFee,
            singleGameFee: data.SingleGameFee,
            pixKey: data.PixKey,
            bankAccountHolderName: data.BankAccountHolderName,
            bankName: data.BankName,
            vestColor: data.VestColor
        );
    }

    private async Task CreateGroupAndAdminMembershipAsync(Group group, Player admin, CancellationToken cancellationToken)
    {
        _context.Groups.Add(group);
        
        await _context.SaveChangesAsync(cancellationToken);

        var adminMembership = GroupPlayer.CreateAdminMembership(group.Id, admin.Id);
        _context.GroupPlayers.Add(adminMembership);

        admin.IncrementCreatedGroups();

        await _context.SaveChangesAsync(cancellationToken);
    }

    private static CreateGroupResult ToResult(Group group)
    {
        return new CreateGroupResult
        {
            Id = group.Id,
            AdminId = group.AdminId,
            Name = group.Name,

            IsRecurring = group.IsRecurring,
            WeekDay = group.WeekDay,
            StartTime = group.StartTime.ToString("HH:mm"),
            EndTime = group.EndTime.ToString("HH:mm"),
            GameDate = group.GameDate?.ToString("yyyy-MM-dd"),

            PlayersLimitPerGame = group.PlayersLimitPerGame,

            CourtName = group.CourtName,
            FullAddress = group.FullAddress,

            MonthlyFee = group.MonthlyFee,
            SingleGameFee = group.SingleGameFee,

            PixKey = group.PixKey,
            BankAccountHolderName = group.BankAccountHolderName,
            BankName = group.BankName,

            VestColor = group.VestColor
        };
    }
    
    private sealed record NormalizedCreateGroup(
        int AdminId,
        string Name,
        bool IsRecurring,
        WeekDay? WeekDay,
        TimeOnly StartTime,
        TimeOnly EndTime,
        DateOnly? GameDate,
        int PlayersLimitPerGame,
        string CourtName,
        string FullAddress,
        decimal MonthlyFee,
        decimal SingleGameFee,
        string PixKey,
        string BankAccountHolderName,
        string BankName,
        string VestColor
    );
}

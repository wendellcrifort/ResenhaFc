using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResenhaFc.Application.Common.Interfaces;
using ResenhaFc.Domain.Entities;

namespace ResenhaFc.Infrastructure.Data;

public class ResenhaDbContext : DbContext, IApplicationDbContext
{
    public ResenhaDbContext(DbContextOptions<ResenhaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players => Set<Player>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<GroupPlayer> GroupPlayers => Set<GroupPlayer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var nullableDateOnlyConverter = new ValueConverter<DateOnly?, DateTime?>(
            d => d.HasValue ? d.Value.ToDateTime(TimeOnly.MinValue) : null,
            d => d.HasValue ? DateOnly.FromDateTime(d.Value) : null);

        var timeOnlyConverter = new ValueConverter<TimeOnly, TimeSpan>(
            t => t.ToTimeSpan(),
            t => TimeOnly.FromTimeSpan(t));

        // Player
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Phone).IsRequired().HasMaxLength(30);

            entity.Property(x => x.DominantFoot).IsRequired();
            entity.Property(x => x.Type).IsRequired();

            entity.Property(x => x.PremiumType).IsRequired();
            entity.Property(x => x.CreatedGroupsCount).IsRequired();

            entity.HasIndex(x => x.Email).IsUnique();
        });

        // Group
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name).IsRequired().HasMaxLength(150);
            entity.Property(x => x.AdminId).IsRequired();

            entity.Property(x => x.IsRecurring).IsRequired();
            entity.Property(x => x.WeekDay).IsRequired(false);

            entity.Property(x => x.StartTime)
                .HasConversion(timeOnlyConverter)
                .IsRequired();

            entity.Property(x => x.EndTime)
                .HasConversion(timeOnlyConverter)
                .IsRequired();

            entity.Property(x => x.GameDate)
                .HasConversion(nullableDateOnlyConverter)
                .IsRequired(false);

            entity.Property(x => x.PlayersLimitPerGame).IsRequired();

            entity.Property(x => x.CourtName).IsRequired().HasMaxLength(150);
            entity.Property(x => x.FullAddress).IsRequired().HasMaxLength(400);

            entity.Property(x => x.MonthlyFee).IsRequired().HasPrecision(10, 2);
            entity.Property(x => x.SingleGameFee).IsRequired().HasPrecision(10, 2);

            entity.Property(x => x.PixKey).IsRequired().HasMaxLength(200);
            entity.Property(x => x.BankAccountHolderName).IsRequired().HasMaxLength(150);
            entity.Property(x => x.BankName).IsRequired().HasMaxLength(150);

            entity.Property(x => x.VestColor).IsRequired().HasMaxLength(50);
        });

        // GroupPlayer
        modelBuilder.Entity<GroupPlayer>(entity =>
        {
            entity.HasKey(x => new { x.GroupId, x.PlayerId });

            entity.Property(x => x.MemberType).IsRequired();
            entity.Property(x => x.Status).IsRequired();

            entity.Property(x => x.Role).IsRequired();

            entity.Property(x => x.ScoreAvg)
                .IsRequired()
                .HasPrecision(10, 2);

            entity.HasOne(x => x.Group)
                .WithMany(g => g.Players)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Player)
                .WithMany(p => p.Groups)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

using Microsoft.EntityFrameworkCore;
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

        // --------------------
        // Player
        // --------------------
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(x => x.DominantFoot)
                .IsRequired();

            entity.Property(x => x.Type)
                .IsRequired();

            // Unique Email across the whole app
            entity.HasIndex(x => x.Email).IsUnique();
        });

        // --------------------
        // Group
        // --------------------
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.AdminId)
                .IsRequired();
        });

        // --------------------
        // GroupPlayer (many-to-many with payload)
        // Prevent same user twice in the same group:
        // Composite key (GroupId, PlayerId)
        // --------------------
        modelBuilder.Entity<GroupPlayer>(entity =>
        {
            entity.HasKey(x => new { x.GroupId, x.PlayerId });

            entity.Property(x => x.Role)
                .IsRequired();

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

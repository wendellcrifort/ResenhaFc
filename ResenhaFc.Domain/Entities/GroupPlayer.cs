using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Domain.Entities;

public class GroupPlayer
{    
    public int GroupId { get; private set; }
    public Group Group { get; private set; } = null!;

    public int PlayerId { get; private set; }
    public Player Player { get; private set; } = null!;

    public GroupMemberType MemberType { get; private set; }
    public GroupMemberStatus Status { get; private set; }

    public PlayerRole Role { get; private set; }
    public decimal ScoreAvg { get; private set; }

    private GroupPlayer() { } 
    public GroupPlayer(int groupId, int playerId, PlayerRole role, decimal scoreAvg)
    {
        GroupId = groupId;
        PlayerId = playerId;

        MemberType = GroupMemberType.Member;
        Status = GroupMemberStatus.Pending;

        Role = role;
        ScoreAvg = scoreAvg;
    }
    
    public static GroupPlayer CreateAdminMembership(int groupId, int playerId)
    {
        return new GroupPlayer
        {
            GroupId = groupId,
            PlayerId = playerId,
            MemberType = GroupMemberType.Admin,
            Status = GroupMemberStatus.Active,
            Role = PlayerRole.Both,
            ScoreAvg = 0m
        };
    }

    public void Accept()
    {
        if (Status != GroupMemberStatus.Pending)
            return;

        Status = GroupMemberStatus.Active;
    }

    public void Decline()
    {
        if (Status != GroupMemberStatus.Pending)
            return;

        Status = GroupMemberStatus.Declined;
    }

    public void ChangeRole(PlayerRole role) => Role = role;

    public void SetScoreAvg(decimal scoreAvg) => ScoreAvg = scoreAvg;
}

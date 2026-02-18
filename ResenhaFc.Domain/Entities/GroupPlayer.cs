using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Domain.Entities;

public class GroupPlayer
{    
    public int GroupId { get; private set; }
    public Group Group { get; private set; } = null!;
    public int PlayerId { get; private set; }
    public Player Player { get; private set; } = null!;    
    public PlayerRole Role { get; private set; }
    public decimal ScoreAvg { get; private set; }

    private GroupPlayer() { } // EF

    public GroupPlayer(int groupId, int playerId, PlayerRole role, decimal scoreAvg)
    {
        GroupId = groupId;
        PlayerId = playerId;
        Role = role;
        ScoreAvg = scoreAvg;
    }

    public void ChangeRole(PlayerRole role) => Role = role;

    public void SetScoreAvg(decimal scoreAvg) => ScoreAvg = scoreAvg;
}

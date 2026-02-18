using ResenhaFc.Domain.Enums;

namespace ResenhaFc.Domain.Entities;

public class Player
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;

    public DominantFoot DominantFoot { get; private set; }
    public PlayerType Type { get; private set; }

    public PremiumType PremiumType { get; private set; }
    public int CreatedGroupsCount { get; private set; }

    public ICollection<GroupPlayer> Groups { get; private set; } = new List<GroupPlayer>();

    private Player() { } // EF
    
    public Player(string name, string email, string phone, DominantFoot dominantFoot, PlayerType type)
    {
        Name = name;
        Email = email;
        Phone = phone;
        DominantFoot = dominantFoot;
        Type = type;

        PremiumType = PremiumType.None;
        CreatedGroupsCount = 0;
    }

    public void SetPremiumType(PremiumType premiumType)
    {
        PremiumType = premiumType;
    }

    public void IncrementCreatedGroups()
    {
        CreatedGroupsCount++;
    }

    public bool CanCreateGroup(int limitedPlanLimit)
    {
        return PremiumType switch
        {
            PremiumType.None => false,
            PremiumType.Unlimited => true,
            PremiumType.Limited => CreatedGroupsCount < limitedPlanLimit,
            _ => false
        };
    }
}

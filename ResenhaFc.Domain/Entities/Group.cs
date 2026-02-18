namespace ResenhaFc.Domain.Entities;

public class Group
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;
    
    public int AdminId { get; private set; }

    public ICollection<GroupPlayer> Players { get; private set; } = new List<GroupPlayer>();

    private Group() { } // EF

    public Group(string name, int adminId)
    {
        Name = name;
        AdminId = adminId;
    }
}

using ResenhaFc.Application.Features.Players.Create;
using ResenhaFc.Domain.Enums;

namespace ResenhaFc.API.Features.Players.Create;

public class CreatePlayerResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public DominantFoot DominantFoot { get; set; }
    public PlayerType Type { get; set; }

    public static CreatePlayerResponse From(CreatePlayerResult result)
    {
        return new CreatePlayerResponse
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email,
            Phone = result.Phone,
            DominantFoot = result.DominantFoot,
            Type = result.Type
        };
    }
}

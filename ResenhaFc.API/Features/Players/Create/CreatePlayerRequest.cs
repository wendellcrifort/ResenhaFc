using ResenhaFc.Application.Features.Players.Create;
using ResenhaFc.Domain.Enums;

namespace ResenhaFc.API.Features.Players.Create;

public class CreatePlayerRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public DominantFoot DominantFoot { get; set; }
    public PlayerType Type { get; set; }

    public CreatePlayerCommand ToCommand()
    {
        return new CreatePlayerCommand(
            Name,
            Email,
            Phone,
            DominantFoot,
            Type
        );
    }
}

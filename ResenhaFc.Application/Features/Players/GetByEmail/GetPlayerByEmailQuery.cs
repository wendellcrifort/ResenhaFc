using MediatR;
using ResenhaFc.Application.Common.Models;

namespace ResenhaFc.Application.Features.Players.GetByEmail;

public record GetPlayerByEmailQuery(string Email) : IRequest<PlayerDto?>;

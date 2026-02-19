using MediatR;
using ResenhaFc.Application.Common.Models;

namespace ResenhaFc.Application.Features.Players.GetById;

public record GetPlayerByIdQuery(int Id) : IRequest<PlayerDto?>;

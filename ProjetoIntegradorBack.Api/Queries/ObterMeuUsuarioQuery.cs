using Prime.Architecture.Mediator.Standard.Requests;
using ProjetoIntegradorBack.Domain.DTOs;

namespace ProjetoIntegradorBack.Api.Queries;

public sealed record GetMyUserQuery : IRequest<UserSummaryDto>;

using ProjetoIntegradorBack.Domain.DTOs; using Prime.Architecture.Mediator.Standard.Requests;
namespace ProjetoIntegradorBack.Api.Queries; public record HorariosDisponiveisQuery(Guid EstabelecimentoId, Guid FuncionarioId, Guid ServicoId, DateOnly Data) : IRequest<List<HorarioDisponivelDto>>;

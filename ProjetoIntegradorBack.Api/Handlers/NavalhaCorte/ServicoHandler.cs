using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class ListarServicosHandler(IServicoRepository repository, IMapper mapper) : IRequestHandler<ListarServicosQuery, List<ServicoDto>>
{
    public async Task<List<ServicoDto>> Handle(ListarServicosQuery request, CancellationToken ct)
    {
        var result = await repository.ListarPorEstabelecimentoAsync(request.EstabelecimentoId, ct);
        return result.IsSuccess ? result.Success!.Select(x => mapper.Map<Servico, ServicoDto>(x)).ToList() : [];
    }
}

public sealed class CriarServicoHandler(IServicoRepository repository, IMapper mapper) : IRequestHandler<CriarServicoCommand, ServicoDto>
{
    public async Task<ServicoDto> Handle(CriarServicoCommand request, CancellationToken ct)
    {
        var result = await repository.AdicionarAsync(new Servico { EstabelecimentoId = request.EstabelecimentoId, Nome = request.Nome, Descricao = request.Descricao, DuracaoMinutos = request.DuracaoMinutos, Preco = request.Preco }, ct);
        if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return mapper.Map<Servico, ServicoDto>(result.Success!);
    }
}

public sealed class AtualizarServicoHandler(IServicoRepository repository, IMapper mapper) : IRequestHandler<AtualizarServicoCommand, ServicoDto>
{
    public async Task<ServicoDto> Handle(AtualizarServicoCommand request, CancellationToken ct)
    {
        var current = await repository.ObterPorIdAsync(request.Id, ct);
        if (current.IsFailure) throw new KeyNotFoundException("Serviço não encontrado.");
        var entity = current.Success!;
        entity.Nome = request.Nome; entity.Descricao = request.Descricao; entity.DuracaoMinutos = request.DuracaoMinutos; entity.Preco = request.Preco;
        var result = await repository.AtualizarAsync(entity, ct);
        if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return mapper.Map<Servico, ServicoDto>(result.Success!);
    }
}

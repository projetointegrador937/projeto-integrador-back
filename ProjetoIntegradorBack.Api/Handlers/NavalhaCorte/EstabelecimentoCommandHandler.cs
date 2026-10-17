using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Domain.ValueObjects;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class CriarEstabelecimentoHandler(IEstabelecimentoRepository repository, IMapper mapper) : IRequestHandler<CriarEstabelecimentoCommand, EstabelecimentoDto>
{
    public async Task<EstabelecimentoDto> Handle(CriarEstabelecimentoCommand r, CancellationToken ct)
    { var x=await repository.AdicionarAsync(new Estabelecimento{RazaoSocial=r.RazaoSocial,NomeFantasia=r.NomeFantasia,Cnpj=r.Cnpj,InscricaoEstadual=r.InscricaoEstadual,Telefone=r.Telefone,Email=r.Email,Descricao=r.Descricao,HorarioFuncionamento=r.HorarioFuncionamento,Endereco=mapper.Map<EnderecoDto,Endereco>(r.Endereco)},ct); if(x.IsFailure)throw new InvalidOperationException(x.Failure!.ToString()); return mapper.Map<Estabelecimento,EstabelecimentoDto>(x.Success!); }
}

public sealed class AtualizarEstabelecimentoHandler(IEstabelecimentoRepository repository, IMapper mapper) : IRequestHandler<AtualizarEstabelecimentoCommand, EstabelecimentoDto>
{
    public async Task<EstabelecimentoDto> Handle(AtualizarEstabelecimentoCommand r, CancellationToken ct) { var old=await repository.ObterPorIdAsync(r.Id,ct); if(old.IsFailure)throw new KeyNotFoundException("Estabelecimento não encontrado."); var e=old.Success!; e.NomeFantasia=r.NomeFantasia;e.Telefone=r.Telefone;e.Email=r.Email;e.Descricao=r.Descricao;e.HorarioFuncionamento=r.HorarioFuncionamento;e.Endereco=mapper.Map<EnderecoDto,Endereco>(r.Endereco); var x=await repository.AtualizarAsync(e,ct);if(x.IsFailure)throw new InvalidOperationException(x.Failure!.ToString());return mapper.Map<Estabelecimento,EstabelecimentoDto>(x.Success!); }
}

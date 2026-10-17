using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.ValueObjects;
using Prime.Architecture.Mapper.Configuration;
namespace ProjetoIntegradorBack.Api.Mappings;
public class NavalhaCorteMappingProfile : MappingProfile
{
    public NavalhaCorteMappingProfile()
    {
        CreateMap<Endereco, EnderecoDto>(); CreateMap<EnderecoDto, Endereco>();
        CreateMap<Estabelecimento, EstabelecimentoDto>(); CreateMap<Estabelecimento, EstabelecimentoDetalheDto>();
        CreateMap<Funcionario, FuncionarioDto>(); CreateMap<Servico, ServicoDto>();
        CreateMap<Agendamento, AgendamentoDto>(); CreateMap<Contato, ContatoDto>();
        CreateMap<ClientUser, UsuarioDto>().ForMember(d => d.Nome, o => o.MapFrom(s => s.Name)).ForMember(d => d.Tipo, o => o.MapFrom(s => s.UserType));
    }
}

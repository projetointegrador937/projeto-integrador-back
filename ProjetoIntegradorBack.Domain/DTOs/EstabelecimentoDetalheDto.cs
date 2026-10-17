namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class EstabelecimentoDetalheDto : EstabelecimentoDto
    {
        public List<ServicoDto> Servicos { get; set; } = [];
        public List<FuncionarioDto> Funcionarios { get; set; } = [];
    }
}
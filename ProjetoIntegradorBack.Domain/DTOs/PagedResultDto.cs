namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class PagedResultDto<T>
    {
        public IReadOnlyList<T> Dados { get; set; } = [];
        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }
        public int Total { get; set; }
    }
}
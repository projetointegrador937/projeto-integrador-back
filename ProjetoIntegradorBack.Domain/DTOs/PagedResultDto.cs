namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class PagedResultDto<T>
    {
        public IReadOnlyList<T> Data { get; set; } = [];
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
namespace ProjetoIntegradorBack.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiraEm { get; set; }
        public bool Revogado { get; set; } = false;
        public string? DispositivoInfo { get; set; }
        public DateTime CriadoEm { get; set; }
        public ClientUser Usuario { get; set; } = null!;
    }
}
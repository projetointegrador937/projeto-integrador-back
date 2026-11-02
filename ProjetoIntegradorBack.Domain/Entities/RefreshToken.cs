namespace ProjetoIntegradorBack.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool Revoked { get; set; } = false;
        public string? DeviceInfo { get; set; }
        public DateTime CreatedAt { get; set; }
        public ClientUser User { get; set; } = null!;
    }
}
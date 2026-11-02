namespace ProjetoIntegradorBack.Domain.DTOs;

public sealed class UserSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Contact { get; set; }
    public AddressDto? Address { get; set; }
}

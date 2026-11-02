using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<Result<RefreshToken>> GetPorTokenAsync(string token, CancellationToken ct = default);
        Task<Result<RefreshToken>> AddAsync(RefreshToken refreshToken, CancellationToken ct = default);
        Task<Result<string>> RevogarAsync(string token, CancellationToken ct = default);
        Task<Result<string>> RevogarTodosDoUserAsync(Guid userId, CancellationToken ct = default);
    }
}
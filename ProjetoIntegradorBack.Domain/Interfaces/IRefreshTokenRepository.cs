using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<Result<RefreshToken>> ObterPorTokenAsync(string token, CancellationToken ct = default);
        Task<Result<RefreshToken>> AdicionarAsync(RefreshToken refreshToken, CancellationToken ct = default);
        Task<Result<string>> RevogarAsync(string token, CancellationToken ct = default);
        Task<Result<string>> RevogarTodosDoUsuarioAsync(Guid usuarioId, CancellationToken ct = default);
    }
}
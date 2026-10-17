using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<ClientUser>> GetUserByIdAsync(Guid id);
        Task<Result<ClientUser>> GetUserByEmailAsync(string email);
        Task<Result<IEnumerable<ClientUser>>> GetAllUsersAsync();
        Task<Result<string>> AddUserAsync(ClientUser user);
        Task<Result<string>> UpdateUserAsync(ClientUser user);
        Task<Result<string>> DeleteUserAsync(Guid id);
    }
}

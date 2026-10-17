using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Infrastructure.Context;
using ProjetoIntegradorBack.Shared.Pattern;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProjetoIntegradorBack.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjetoIntegradorBackDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ProjetoIntegradorBackDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Result<string>> AddUserAsync(ClientUser user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("User added successfully.");
            }
            catch (Exception ex)
            {
                return Result<string>.AsFailure(new Failure(ex.HResult,$"Error adding user: {ex.Message}"));
            }
        }
        public async Task<Result<string>> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<ClientUser>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _context.Users
                            .AsNoTracking()
                            .ToListAsync();
                return Result<IEnumerable<ClientUser>>.AsSuccess(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users");

                return Result<IEnumerable<ClientUser>>.AsFailure(
                    new Failure(ex.HResult, "An unexpected error occurred.")
                );
            }
        }

        public Task<Result<ClientUser>> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ClientUser>> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> UpdateUserAsync(ClientUser user)
        {
            throw new NotImplementedException();
        }
    }
}

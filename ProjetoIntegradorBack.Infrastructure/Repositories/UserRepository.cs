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
                _logger.LogError(ex, "Falha ao adicionar usuário. UserId: {UserId}, Email: {Email}", user.Id, user.Email);
                                return Result<string>.AsFailure(new Failure(500, "Não foi possível adicionar o usuário."));
            }
        }
        public async Task<Result<string>> DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return Result<string>.AsFailure(new Failure(404, "Usuário não encontrado."));
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("Usuário removido com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao remover usuário. UserId: {UserId}", id);
                return Result<string>.AsFailure(new Failure(500, "Não foi possível remover o usuário."));
            }
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
                _logger.LogError(ex, "Falha ao listar usuários. Operação: {Operation}", nameof(GetAllUsersAsync));

                return Result<IEnumerable<ClientUser>>.AsFailure(
                    new Failure(ex.HResult, "An unexpected error occurred.")
                );
            }
        }

        public async Task<Result<ClientUser>> GetUserByEmailAsync(string email)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();
            var atIndex = normalizedEmail.IndexOf('@');
            if (atIndex <= 0 || atIndex == normalizedEmail.Length - 1)
                return Result<ClientUser>.AsFailure(new Failure(400, "E-mail inválido."));

            var userName = normalizedEmail[..atIndex];
            var domain = normalizedEmail[(atIndex + 1)..];
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.UserName == userName && x.Email.Domain == domain);

            return user == null
                ? Result<ClientUser>.AsFailure(new Failure(404, "Usuário não encontrado."))
                : Result<ClientUser>.AsSuccess(user);
        }

        public async Task<Result<ClientUser>> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(x => x.Address)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            return user == null ? Result<ClientUser>.AsFailure(new Failure(404, "Usuário não encontrado.")) : Result<ClientUser>.AsSuccess(user);
        }

        public async Task<Result<string>> UpdateUserAsync(ClientUser user)
        {
            try
            {
                var current = await _context.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Id == user.Id);
                if (current == null) return Result<string>.AsFailure(new Failure(404, "Usuário não encontrado."));
                current.Name = user.Name;
                current.Phone = user.Phone;
                current.ProfilePictureUrl = user.ProfilePictureUrl;
                if (user.Address != null)
                {
                    if (current.Address == null) current.Address = user.Address;
                    else
                    {
                        current.Address.Street = user.Address.Street; current.Address.Number = user.Address.Number;
                        current.Address.AdditionalInfo = user.Address.AdditionalInfo; current.Address.Neighborhood = user.Address.Neighborhood;
                        current.Address.PostalCode = user.Address.PostalCode; current.Address.City = user.Address.City;
                        current.Address.State = user.Address.State; current.Address.Latitude = user.Address.Latitude; current.Address.Longitude = user.Address.Longitude;
                    }
                }
                await _context.SaveChangesAsync();
                return Result<string>.AsSuccess("Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao atualizar usuário. UserId: {UserId}", user.Id);
                return Result<string>.AsFailure(new Failure(500, "Não foi possível atualizar o usuário."));
            }
        }
    }
}

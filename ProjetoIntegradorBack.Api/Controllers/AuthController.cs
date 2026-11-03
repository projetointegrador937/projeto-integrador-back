using BCryptHash = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Services;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mapper.Interfaces;

namespace ProjetoIntegradorBack.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public sealed class AuthController(
    IUserRepository users,
    IRefreshTokenRepository refreshTokens,
    IJwtTokenService jwt,
    IMapper mapper,
    ILogger<AuthController> logger) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

        try
        {
            var result = await users.GetUserByEmailAsync(command.Email);
            if (result.IsFailure || result.Success is null || !BCryptHash.Verify(command.Password, result.Success.Password))
                return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

            var user = result.Success;
            var refresh = jwt.GenerateRefreshToken();
            var refreshResult = await refreshTokens.AddAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = refresh,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                DeviceInfo = Request.Headers.UserAgent.ToString()
            }, ct);

            if (refreshResult.IsFailure)
            {
                logger.LogError("Falha ao criar refresh token. UserId: {UserId}, Failure: {Failure}", user.Id, refreshResult.Failure);
                return Problem("Não foi possível concluir o login.", statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok(new AuthResponseDto
            {
                AccessToken = jwt.GenerateAccessToken(user),
                RefreshToken = refresh,
                User = mapper.Map<ClientUser, UserDto>(user)
            });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Erro inesperado no login. Método: {Method}, Rota: {Path}, Email: {Email}, UserAgent: {UserAgent}", Request.Method, Request.Path, command.Email, Request.Headers.UserAgent.ToString());
            return Problem("Não foi possível concluir o login.", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command, CancellationToken ct)
    {
        var storedResult = await refreshTokens.GetPorTokenAsync(command.RefreshToken, ct);
        if (storedResult.IsFailure || storedResult.Success is null)
            return Unauthorized(new { mensagem = "Refresh token inválido." });

        var stored = storedResult.Success;
        if (stored.Revoked || stored.ExpiresAt <= DateTime.UtcNow)
            return Unauthorized(new { mensagem = "Refresh token expirado ou revogado." });

        var user = stored.User;
        var newRefreshToken = jwt.GenerateRefreshToken();

        await refreshTokens.RevogarAsync(stored.Token, ct);
        await refreshTokens.AddAsync(new RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            DeviceInfo = Request.Headers.UserAgent.ToString()
        }, ct);

        return Ok(new AuthResponseDto
        {
            AccessToken = jwt.GenerateAccessToken(user),
            RefreshToken = newRefreshToken,
            User = mapper.Map<ClientUser, UserDto>(user)
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] string refreshToken, CancellationToken ct)
        => Ok(await refreshTokens.RevogarAsync(refreshToken, ct));
}

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
public sealed class AuthController(IUserRepository users, IRefreshTokenRepository refreshTokens, IJwtTokenService jwt, IMapper mapper) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
    {
        var result = await users.GetUserByEmailAsync(command.Email);
        if (result.IsFailure || result.Success is null || !BCryptHash.Verify(command.Password, result.Success.Password))
            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

        var user = result.Success;
        var refresh = jwt.GenerateRefreshToken();
        await refreshTokens.AddAsync(new RefreshToken { UserId = user.Id, Token = refresh, ExpiresAt = DateTime.UtcNow.AddDays(7), DeviceInfo = Request.Headers.UserAgent.ToString() }, ct);
        return Ok(new AuthResponseDto { AccessToken = jwt.GenerateAccessToken(user), RefreshToken = refresh, User = mapper.Map<ClientUser, UserDto>(user) });
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
    public async Task<IActionResult> Logout([FromBody] string refreshToken, CancellationToken ct) => Ok(await refreshTokens.RevogarAsync(refreshToken, ct));
}

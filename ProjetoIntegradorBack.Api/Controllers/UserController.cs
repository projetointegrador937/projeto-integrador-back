using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Queries;
using Microsoft.AspNetCore.Authorization;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Api.Handlers;
using Prime.Architecture.Mediator.Standard;

using Microsoft.AspNetCore.Mvc;

namespace ProjetoIntegradorBack.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly Mediator _medaytor;
        public UserController(Mediator medaytor) => _medaytor = medaytor;

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetData(CancellationToken ct)
            => Ok(await _medaytor.Send(new GetMyUserQuery(), ct));

        [Authorize(Policy = "Cliente")]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateUserCommand command, UpdateUserAddressHandler handler, CancellationToken ct)
            => Ok(await handler.Handle(command, ct));

[AllowAnonymous]
[HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken ct)
        {
            var result = await _medaytor.Send(command, ct);

            if (result.IsSuccess)
                return Ok(result.Success);

            return BadRequest(result.Failure);
        }

        [AllowAnonymous]
        [HttpPost("create-barbershop-owner")]
        public async Task<IActionResult> CreateBarberShopOwner([FromBody] CreateBarbershopOwnerCommand command, CancellationToken ct)
            => Ok(await _medaytor.Send(command, ct));

        [Authorize(Policy = "Barbearia")]
        [HttpPost("create-barber")]
        public async Task<IActionResult> CreateBarber([FromBody] CreateBarberUserCommand command, CancellationToken ct)
            => Ok(await _medaytor.Send(command, ct));
    }
}

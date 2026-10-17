using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Handlers;
using ProjetoIntegradorBack.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoIntegradorBack.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserHandler _userHandler;
        private readonly IUserRepository _userRepository;
        public UserController(UserHandler userHandler, IUserRepository userRepository)
        {
            _userHandler = userHandler;
            _userRepository = userRepository;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _userHandler.HandleCreateUser(command);

            if (result.IsSuccess)
                return Ok(result.Success);

            return BadRequest(result.Failure);
        }

        [HttpPost("create-barbershop-owner")]
        public async Task<IActionResult> CreateBarberShopOwner([FromBody] CreateUserCommand command)
        {
            var result = await _userHandler.HandleCreateBarberShopOwner(command);

            if (result.IsSuccess)
                return Ok(result.Success);

            return BadRequest(result.Failure);
        }  
    }
}

using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Handlers;
using ProjetoIntegradorBack.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoIntegradorBack.Api.Controllers
{
    [ApiController]
    [Route("api/barbershop")]
    public class BarberShopController : ControllerBase
    {
        private readonly BarberShopHandler _barberShopHandler;
        private readonly IBarberShopRepository _barberShopRepository;
        public BarberShopController(IBarberShopRepository barberShopRepository, BarberShopHandler barberShopHandler)
        {
            _barberShopRepository = barberShopRepository;
            _barberShopHandler = barberShopHandler;
        }
        //[Authorize(Roles = "BarberShopOwner")]
        [HttpPost("create-barbershop")]
        public async Task<IActionResult> CreateBarberShop([FromBody] CreateBarberShopCommand command)
        {
            var result = await _barberShopHandler.HandleBarberShop(command);

            if (result.IsSuccess)
                return Ok(result.Success);

            return BadRequest(result.Failure);
        }

        [HttpGet("get-barbershops")]
        public async Task<IActionResult> GetBarberShops()
        {
            var result = await _barberShopRepository.GetAllBarberShopsAsync();

            if (result.IsSuccess)
                return Ok(result.Success);

            return BadRequest(result.Failure);
        }
        //[Authorize(Roles = "BarberShopOwner")]
        [HttpPost("create-barber")]
        public async Task<IActionResult> CreateBarber([FromBody] CreateBarberCommand command)
        {
            var result = await _barberShopHandler.HandleCreateBarber(command);

            if (result.IsSuccess)
                return Ok(result.Success);

            return BadRequest(result.Failure);
        }

        [HttpPost("create-barber-schedule")]
        public async Task<IActionResult> CreateBarberSchedule([FromBody] CreateBarberScheduleCommand command)
        {
            var result = await _barberShopHandler.HandleCreateBarberSchedule(command);

            if (result.IsSuccess)
                return Ok();

            return BadRequest(result.Failure);
        }

        [HttpPost("create-service")]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceCommand command)
        {
            var result = await _barberShopHandler.HandleCreateService(command);

            if (result.IsSuccess)
                return Ok(result.Success);

            return BadRequest(result.Failure);
        }

        [HttpGet("get-services")]
        public async Task<IActionResult> GetServices()
        {
            var result = await _barberShopRepository.GetAllServicesAsync();

            if (result.IsSuccess)
                return Ok(result.Success);

            return BadRequest(result.Failure);
        }

        [HttpGet("get-barbershop/{id}")]
        public async Task<IActionResult> GetBarberShop(Guid id)
        {
            var result = await _barberShopRepository.GetBarberShopByIdAsync(id);

            if (result.IsSuccess)
                return Ok(result.Success);

            return BadRequest(result.Failure);
        }

        [HttpGet("get-service")]
        public async Task<IActionResult> GetService([FromQuery] Guid barberShopId, [FromQuery] Guid id)
        {
            var result = await _barberShopRepository.GetServiceByIdAsync(barberShopId,id);

            if (result.IsSuccess)
                return Ok(result.Success);

            return BadRequest(result.Failure);
        }     

    }
}

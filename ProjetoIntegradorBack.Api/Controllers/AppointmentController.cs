using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Handlers;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoIntegradorBack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _repository;
        private readonly IBarberShopService _barberShopService;
        private readonly AppointmentHandler _appointmentHandler;
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentController(
            IAppointmentRepository repository, 
            IBarberShopService barberShopService, 
            AppointmentHandler appointmentHandler, 
            IAppointmentRepository appointmentRepository)
        {
            _repository = repository;
            _barberShopService = barberShopService;
            _appointmentHandler = appointmentHandler;
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet("get-barbers-schedule")]
        public async Task<IActionResult> GetBarberSchedule(
            [FromQuery] Guid barberShopId, 
            [FromQuery] Guid barberId, 
            [FromQuery] Guid serviceId, 
            [FromQuery] DateOnly date)
        {
            try
            {
                var result = await _barberShopService.GetBarberAvailableTimesAsync(barberShopId, barberId, serviceId,date);

                if (result.IsSuccess)
                    return Ok(result.Success);

                Console.WriteLine($"Failed to retrieve barber's schedule: {result.Failure}");
                return BadRequest(result.Failure);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the barber's schedule: {ex.Message}");
            }
        }

        [HttpPost("create-appointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            try
            {
               var result = await _appointmentHandler.HandleCreateAppointmentAsync(command);

                if (result.IsSuccess)
                    return Ok(result.Success);

                Console.WriteLine($"Failed to create appointment: {result.Failure}");
                return BadRequest(result.Failure);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the appointment: {ex.Message}");
            }
        }

        [HttpGet("get-appointments-barber")]
        public async Task<IActionResult> GetAppointmentsByBarberIdAsync(
            [FromQuery] Guid barberShopId,
            [FromQuery] Guid barberId,
            [FromQuery] DateOnly date)
        {
            try
            {
                var result = await _appointmentRepository.GetAppointmentsByBarberIdAsync(barberShopId,barberId, date);

                if (result.IsSuccess)
                    return Ok(result.Success);

                Console.WriteLine($"Failed to retrieve appointments: {result.Failure}");
                return BadRequest(result.Failure);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving appointments: {ex.Message}");
            }
        }
    }
}

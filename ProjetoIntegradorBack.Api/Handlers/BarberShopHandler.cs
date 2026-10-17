using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Shared.Pattern;
using FluentValidation;
using System.Data;

namespace ProjetoIntegradorBack.Api.Handlers
{
    public class BarberShopHandler
    {
        private readonly IBarberShopRepository _barberShopRepository;
        private readonly IValidator<CreateBarberShopCommand> _validator;
       
        public BarberShopHandler(IBarberShopRepository barberShopRepository, IValidator<CreateBarberShopCommand> validator)
        {
            _barberShopRepository = barberShopRepository;
            _validator = validator;
           
        }

        public async Task<Result<string>> HandleBarberShop(CreateBarberShopCommand command)
        {
            var validationResult = _validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return Result<string>.AsFailure(new Failure(
                    400, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)))
                );
            }

            var barberShop = BarberShop.CreateBarberShop(
                command.Name,
                command.Email,
                command.Description,
                command.Phone,
                command.Logo
            );

            var businessHours = BusinessHours.CreateBusinessHours(
                barberShop.Success.Id,
                command.DayOfWeek,
                command.Start,
                command.End,
                command.BreakStart,
                command.BreakEnd
            );

            var userDb = await _barberShopRepository.AddBusinessHoursAsync(barberShop.Success, businessHours.Success);
            return Result<string>.AsSuccess($"Barber shop created successfully");
        }

        public async Task<Result<string>> HandleCreateBarber(CreateBarberCommand command)
        {
            //var validationResult = _validator.Validate(command);
            //if (!validationResult.IsValid)
            //{
            //    return Result<string>.AsFailure(new Failure(
            //        400, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)))
            //    );
            //}

            var user = ClientUser.CreateBarber(
                command.Email,
                command.Password,
                command.ProfilePictureUrl,
                command.Name
            );
            var barber = Barber.Create(
                user.Success.Id,
                command.Name,
                command.BarberShopId,
                command.Commission
            );
            await _barberShopRepository.AddBarberAsync(user.Success, barber.Success);
            return Result<string>.AsSuccess($"User created successfully");
        }

        public async Task<Result<string>> HandleCreateBarberSchedule(CreateBarberScheduleCommand command)
        {
            var barberSchedule = BarberSchedule.Create(
                command.BarberShopId,
                command.BarberId,
                command.DayOfWeek,
                command.Start,
                command.End,
                command.BreakStart,
                command.BreakEnd
            );
            var barber = await _barberShopRepository.GetBarberByIdAsync(command.BarberId);
            if (barber.IsFailure)
            {
                return Result<string>.AsFailure(new Failure(404, "Barber not found"));
            }
            var result = await _barberShopRepository.AddBarberScheduleAsync(barber.Success, barberSchedule.Success);
            return result;
        }

        public async Task<Result<string>> HandleCreateService(CreateServiceCommand command)
        {
            var service = BarberShopService.Create(
                //command.BarberShopId,
                command.Name,                
                command.Price,
                command.Duration               
            );
            var result = await _barberShopRepository.AddServiceAsync(service.Success);
            return result;
        }
    }

}

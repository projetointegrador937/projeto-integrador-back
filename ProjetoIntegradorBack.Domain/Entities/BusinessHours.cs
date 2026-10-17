using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class BusinessHours
    {
        public Guid Id { get; init; }
        public Guid BarberShopId { get; init; }
        public DayOfWeek DayOfWeek { get; init; }
        public TimeOnly Start { get; init; }
        public TimeOnly End { get; init; }
        public TimeOnly? BreakStart { get; init; }
        public TimeOnly? BreakEnd { get; init; }
        public static Result<BusinessHours> CreateBusinessHours(
            Guid barberShopId, 
            DayOfWeek dayOfWeek, 
            TimeOnly start, 
            TimeOnly end, 
            TimeOnly? breakStart, 
            TimeOnly? breakEnd)
        {
            return Result<BusinessHours>.AsSuccess(new BusinessHours
            {
                Id = Guid.NewGuid(),
                BarberShopId = barberShopId,
                DayOfWeek = dayOfWeek,
                Start = start,
                End = end,
                BreakStart = breakStart,
                BreakEnd = breakEnd
            });
        }
    }
}

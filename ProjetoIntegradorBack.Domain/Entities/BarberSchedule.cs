using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.Entities
{
    public class BarberSchedule
    {
        public Guid Id { get; init; }
        public Guid BarberId { get; init; }
        public Guid BarberShopId { get; init; }
        public DayOfWeek DayOfWeek { get; init; }
        public TimeOnly Start { get; init; }
        public TimeOnly End { get; init; }
        public TimeOnly? BreakStart { get; init; }
        public TimeOnly? BreakEnd { get; init; }

        public static Result<BarberSchedule> Create(
            Guid barberId,
            Guid barberShopId,
            DayOfWeek dayOfWeek,
            TimeOnly start,
            TimeOnly end,
            TimeOnly? breakStart,
            TimeOnly? breakEnd)
        {
            return Result<BarberSchedule>.AsSuccess(new BarberSchedule
            {
                Id = Guid.NewGuid(),
                BarberId = barberId,
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

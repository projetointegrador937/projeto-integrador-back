using System.Text.Json;
using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

/// <summary>
/// Gera os slots de horário disponíveis para um estabelecimento em uma data,
/// considerando horário de funcionamento e agendamentos já existentes.
/// </summary>
public sealed class GetAvailabilityHandler(
    IEstablishmentRepository establishmentRepo,
    IServiceRepository serviceRepo,
    IAppointmentRepository appointmentRepo
) : IRequestHandler<GetAvailabilityQuery, List<AvailableTimeDto>>
{
    private const int DefaultSlotMinutes = 30;

    public async Task<List<AvailableTimeDto>> Handle(GetAvailabilityQuery request, CancellationToken ct)
    {
        // 1. Busca estabelecimento para obter horário de funcionamento
        var estResult = await establishmentRepo.GetPorIdAsync(request.EstablishmentId, ct);
        if (estResult.IsFailure)
            return [];

        var establishment = estResult.Success!;

        // 2. Determina duração do serviço (default 30 min)
        int serviceDuration = DefaultSlotMinutes;
        if (request.ServiceId.HasValue)
        {
            var svcResult = await serviceRepo.GetPorIdAsync(request.ServiceId.Value, ct);
            if (svcResult.IsSuccess)
                serviceDuration = svcResult.Success!.DurationMinutes;
        }

        // 3. Extrai janela de funcionamento do dia
        var (open, close) = ParseBusinessHoursForDay(establishment.BusinessHours, request.Date.DayOfWeek);
        if (open is null || close is null)
            return []; // estabelecimento fechado neste dia

        // 4. Carrega agendamentos do dia para o funcionário (ou para o estabelecimento inteiro)
        var dayStart = request.Date.ToDateTime(open.Value);
        var dayEnd   = request.Date.ToDateTime(close.Value);

        IReadOnlyList<Appointment> existing;
        if (request.EmployeeId.HasValue)
        {
            var apptResult = await appointmentRepo.GetPorEmployeeNoDateAsync(request.EmployeeId.Value, request.Date, ct);
            existing = apptResult.IsSuccess ? apptResult.Success! : [];
        }
        else
        {
            var apptResult = await appointmentRepo.GetPorEstablishmentNoPeriodoAsync(request.EstablishmentId, dayStart, dayEnd, ct);
            existing = apptResult.IsSuccess ? apptResult.Success! : [];
        }

        // 5. Gera slots a cada `serviceDuration` minutos dentro do horário de funcionamento
        var slots = new List<AvailableTimeDto>();
        var cursor = open.Value;
        while (cursor.Add(TimeSpan.FromMinutes(serviceDuration)) <= close.Value)
        {
            var slotStart = request.Date.ToDateTime(cursor);
            var slotEnd   = slotStart.AddMinutes(serviceDuration);

            bool occupied = existing.Any(a =>
            {
                var aStart = a.DateTime;
                // duração do serviço já agendado (pode ser diferente do solicitado)
                var aEnd = a.DateTime.AddMinutes(serviceDuration);
                // sobreposição: os intervalos se intersectam
                return slotStart < aEnd && slotEnd > aStart;
            });

            slots.Add(new AvailableTimeDto
            {
                Time      = cursor,
                Available = !occupied
            });

            cursor = cursor.Add(TimeSpan.FromMinutes(serviceDuration));
        }

        return slots;
    }

    // -----------------------------------------------------------------------
    // Utilitário: faz parse do campo BusinessHours (string JSON ou string simples)
    // Suporta dois formatos:
    //   1. JSON array: [{ "dayOfWeek": 1, "start": "09:00", "end": "18:00" }, ...]
    //   2. String legível: "Seg-Sex 09:00-18:00" (fallback: retorna 09:00-18:00)
    // -----------------------------------------------------------------------
    private static (TimeOnly? open, TimeOnly? close) ParseBusinessHoursForDay(string businessHours, DayOfWeek day)
    {
        if (string.IsNullOrWhiteSpace(businessHours))
            return DefaultWindow();

        try
        {
            var json = JsonSerializer.Deserialize<JsonElement>(businessHours);
            if (json.ValueKind == JsonValueKind.Array)
            {
                foreach (var entry in json.EnumerateArray())
                {
                    int? dow = null;
                    string? start = null, end = null;

                    if (entry.TryGetProperty("dayOfWeek", out var dowEl))
                        dow = dowEl.GetInt32();
                    else if (entry.TryGetProperty("DayOfWeek", out var dowEl2))
                        dow = dowEl2.GetInt32();

                    if (entry.TryGetProperty("start", out var startEl))
                        start = startEl.GetString();
                    else if (entry.TryGetProperty("Start", out var startEl2))
                        start = startEl2.GetString();

                    if (entry.TryGetProperty("end", out var endEl))
                        end = endEl.GetString();
                    else if (entry.TryGetProperty("End", out var endEl2))
                        end = endEl2.GetString();

                    if (dow.HasValue && (int)day == dow.Value && start is not null && end is not null)
                    {
                        return (
                            TimeOnly.TryParse(start, out var o) ? o : null,
                            TimeOnly.TryParse(end,   out var c) ? c : null
                        );
                    }
                }
                return (null, null); // dia não encontrado no array → fechado
            }
        }
        catch (JsonException) { /* não é JSON válido, usa fallback */ }

        // Fallback: assume horário padrão comercial se não conseguir parsear
        return DefaultWindow();
    }

    private static (TimeOnly?, TimeOnly?) DefaultWindow() =>
        (new TimeOnly(9, 0), new TimeOnly(18, 0));
}

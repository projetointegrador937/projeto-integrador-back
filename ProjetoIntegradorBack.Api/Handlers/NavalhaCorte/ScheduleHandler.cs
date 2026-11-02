using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class AgendaMensalHandler(IAppointmentRepository repository, IMapper mapper) : IRequestHandler<AgendaMensalQuery, List<AppointmentDayDto>>
{
    public async Task<List<AppointmentDayDto>> Handle(AgendaMensalQuery r, CancellationToken ct)
    { var x=await repository.GetPorEmployeeNoMonthAsync(r.EmployeeId,r.Year,r.Month,ct); if(x.IsFailure)return []; return x.Success!.GroupBy(a=>DateOnly.FromDateTime(a.DateTime)).Select(g=>new AppointmentDayDto{Date=g.Key,TotalAppointments=g.Count(),Appointments=g.Select(a=>mapper.Map<Appointment,AppointmentDto>(a)).ToList()}).OrderBy(x=>x.Date).ToList(); }
}

public sealed class AgendaDateHandler(IAppointmentRepository repository, IMapper mapper) : IRequestHandler<AgendaDateQuery, List<AppointmentDto>>
{
    public async Task<List<AppointmentDto>> Handle(AgendaDateQuery r, CancellationToken ct) { var x=await repository.GetPorEmployeeNoDateAsync(r.EmployeeId,r.Date,ct); return x.IsSuccess?x.Success!.Select(a=>mapper.Map<Appointment,AppointmentDto>(a)).ToList():[]; }
}

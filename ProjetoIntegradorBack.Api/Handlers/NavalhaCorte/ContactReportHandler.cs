using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class EnviarContactHandler(IContactRepository repository, IMapper mapper) : IRequestHandler<SendContactCommand, ContactDto>
{
    public async Task<ContactDto> Handle(SendContactCommand r, CancellationToken ct)
    { var x = await repository.AddAsync(new Contact { Subject=r.Subject, Name=r.Name, LastName=r.LastName, Email=r.Email, BookingNumber=r.BookingNumber, Description=r.Description }, ct); if (x.IsFailure) throw new InvalidOperationException(x.Failure!.ToString()); return mapper.Map<Contact, ContactDto>(x.Success!); }
}

public sealed class ListContactsHandler(IContactRepository repository, IMapper mapper) : IRequestHandler<ListContactsQuery, PagedResultDto<ContactDto>>
{
    public async Task<PagedResultDto<ContactDto>> Handle(ListContactsQuery r, CancellationToken ct)
    { var x=await repository.ListAsync(r.Status,r.Page,r.Tamanho,ct); var t=await repository.ContarAsync(r.Status,ct); return new(){Data=x.IsSuccess?x.Success!.Select(c=>mapper.Map<Contact,ContactDto>(c)).ToList():[],Page=r.Page,PageSize=r.Tamanho,Total=t.IsSuccess?t.Success:0}; }
}

public sealed class UpdateStatusContactHandler(IContactRepository repository, IMapper mapper) : IRequestHandler<UpdateContactStatusCommand, ContactDto>
{
    public async Task<ContactDto> Handle(UpdateContactStatusCommand r, CancellationToken ct) { var x=await repository.UpdateStatusAsync(r.Id,r.Status,ct); if(x.IsFailure) throw new KeyNotFoundException(x.Failure!.ToString()); return mapper.Map<Contact,ContactDto>(x.Success!); }
}

public sealed class RelatorioComissaoHandler(IRelatorioRepository repository) : IRequestHandler<RelatorioComissaoQuery, CommissionReportDto>
{
    public async Task<CommissionReportDto> Handle(RelatorioComissaoQuery r, CancellationToken ct) { var x=await repository.GetCommissionsPorMonthAsync(r.EstablishmentId,r.Month,r.Year,r.EmployeeId,ct); if(x.IsFailure) throw new InvalidOperationException(x.Failure!.ToString()); var list=x.Success!.ToList(); return new(){Month=r.Month,Year=r.Year,Commissions=list,TotalRevenue=list.Sum(c=>c.FaturamentoBruto),TotalCommissions=list.Sum(c=>c.ValorComissao)}; }
}

public sealed class RelatorioAtendimentosHandler(IRelatorioRepository repository) : IRequestHandler<RelatorioAtendimentosQuery, AppointmentReportDto>
{
    public async Task<AppointmentReportDto> Handle(RelatorioAtendimentosQuery r, CancellationToken ct) { var x=await repository.GetResumoAtendimentosAsync(r.EstablishmentId,r.Start,r.End,ct); if(x.IsFailure) throw new InvalidOperationException(x.Failure!.ToString()); return new(){Start=r.Start,End=r.End,Resumo=x.Success!}; }
}

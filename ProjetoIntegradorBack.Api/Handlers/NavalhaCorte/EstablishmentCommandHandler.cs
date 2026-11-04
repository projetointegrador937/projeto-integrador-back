using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Domain.ValueObjects;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class CriarEstablishmentHandler(IEstablishmentRepository repository, IMapper mapper) : IRequestHandler<CreateEstablishmentCommand, EstablishmentDto>
{
    public async Task<EstablishmentDto> Handle(CreateEstablishmentCommand r, CancellationToken ct)
    { var x=await repository.AddAsync(new Establishment{LegalName=r.LegalName,TradeName=r.TradeName,Cnpj=r.Cnpj,StateRegistration=r.StateRegistration,Phone=r.Phone,Email=r.Email,BusinessHours=r.BusinessHours,Address=mapper.Map<AddressDto,Address>(r.Address)},ct); if(x.IsFailure)throw new InvalidOperationException(x.Failure!.ToString()); return mapper.Map<Establishment,EstablishmentDto>(x.Success!); }
}

public sealed class UpdateEstablishmentHandler(IEstablishmentRepository repository, IMapper mapper) : IRequestHandler<UpdateEstablishmentCommand, EstablishmentDto>
{
    public async Task<EstablishmentDto> Handle(UpdateEstablishmentCommand r, CancellationToken ct) { var old=await repository.GetPorIdAsync(r.Id,ct); if(old.IsFailure)throw new KeyNotFoundException("Establishment não encontrado."); var e=old.Success!; e.TradeName=r.TradeName;e.Phone=r.Phone;e.Email=r.Email;e.Description=r.Description;e.BusinessHours=r.BusinessHours;e.Address=mapper.Map<AddressDto,Address>(r.Address); var x=await repository.UpdateAsync(e,ct);if(x.IsFailure)throw new InvalidOperationException(x.Failure!.ToString());return mapper.Map<Establishment,EstablishmentDto>(x.Success!); }
}

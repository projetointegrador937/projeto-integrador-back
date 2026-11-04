using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using DomainService = ProjetoIntegradorBack.Domain.Entities.Service;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class ListServicesHandler(IServiceRepository repository, IMapper mapper) : IRequestHandler<ListServicesQuery, List<ServiceDto>>
{
    public async Task<List<ServiceDto>> Handle(ListServicesQuery request, CancellationToken ct)
    {
        var result = await repository.ListPorEstablishmentAsync(request.EstablishmentId, ct);
        return result.IsSuccess ? result.Success!.Select(x => mapper.Map<DomainService, ServiceDto>(x)).ToList() : [];
    }
}

public sealed class CriarServiceHandler(IServiceRepository repository, IMapper mapper) : IRequestHandler<CreateServiceCommand, ServiceDto>
{
    public async Task<ServiceDto> Handle(CreateServiceCommand request, CancellationToken ct)
    {
        var result = await repository.AddAsync(new DomainService { EstablishmentId = request.EstablishmentId, Name = request.Name, Description = request.Description, DurationMinutes = request.DurationMinutes, Price = request.Price }, ct);
        if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return mapper.Map<DomainService, ServiceDto>(result.Success!);
    }
}

public sealed class UpdateServiceHandler(IServiceRepository repository, IMapper mapper) : IRequestHandler<UpdateServiceCommand, ServiceDto>
{
    public async Task<ServiceDto> Handle(UpdateServiceCommand request, CancellationToken ct)
    {
        var current = await repository.GetPorIdAsync(request.Id, ct);
        if (current.IsFailure) throw new KeyNotFoundException("Serviço não encontrado.");
        var entity = current.Success!;
        entity.Name = request.Name; entity.Description = request.Description; entity.DurationMinutes = request.DurationMinutes; entity.Price = request.Price;
        var result = await repository.UpdateAsync(entity, ct);
        if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return mapper.Map<DomainService, ServiceDto>(result.Success!);
    }
}

public sealed class DeactivateServiceHandler(IServiceRepository repository)
    : IRequestHandler<DeactivateServiceCommand, string>
{
    public async Task<string> Handle(DeactivateServiceCommand request, CancellationToken ct)
    {
        var found = await repository.GetPorIdAsync(request.Id, ct);
        if (found.IsFailure) throw new KeyNotFoundException("Serviço não encontrado.");
        if (found.Success!.EstablishmentId != request.EstablishmentId)
            throw new InvalidOperationException("Serviço não pertence ao estabelecimento informado.");

        var result = await repository.DesativarAsync(request.Id, ct);
        if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return result.Success!;
    }
}

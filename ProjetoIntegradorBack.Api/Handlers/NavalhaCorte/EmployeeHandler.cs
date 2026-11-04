using ProjetoIntegradorBack.Api.Commands;
using ProjetoIntegradorBack.Api.Queries;
using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.Interfaces;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mediator.Standard.Requests;

namespace ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;

public sealed class ListEmployeesHandler(IEmployeeRepository repository, IMapper mapper)
    : IRequestHandler<ListEmployeesQuery, List<EmployeeDto>>
{
    public async Task<List<EmployeeDto>> Handle(ListEmployeesQuery request, CancellationToken ct)
    {
        var result = await repository.ListPorEstablishmentAsync(request.EstablishmentId, ct);
        return result.IsSuccess ? result.Success!.Select(x => mapper.Map<Employee, EmployeeDto>(x)).ToList() : [];
    }
}

public sealed class CreateEmployeeHandler(IEmployeeRepository repository, IMapper mapper)
    : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken ct)
    {
        var result = await repository.AddAsync(new Employee
        {
            EstablishmentId = request.EstablishmentId,
            UserId = request.UserId,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Role = request.Role,
            CommissionPercentage = request.CommissionPercentage,
            IsActive = true
        }, ct);
        if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return mapper.Map<Employee, EmployeeDto>(result.Success!);
    }
}

public sealed class UpdateEmployeeHandler(IEmployeeRepository repository, IMapper mapper)
    : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken ct)
    {
        var found = await repository.GetPorIdAsync(request.Id, ct);
        if (found.IsFailure) throw new KeyNotFoundException("Funcionário não encontrado.");
        var employee = found.Success!;
        if (employee.EstablishmentId != request.EstablishmentId)
            throw new InvalidOperationException("Funcionário não pertence ao estabelecimento informado.");

        employee.Name = request.Name;
        employee.Email = request.Email;
        employee.Phone = request.Phone;
        employee.Role = request.Role;
        employee.CommissionPercentage = request.CommissionPercentage;

        var result = await repository.UpdateAsync(employee, ct);
        if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return mapper.Map<Employee, EmployeeDto>(result.Success!);
    }
}

public sealed class UpdateEmployeeLinkHandler(IEmployeeRepository repository)
    : IRequestHandler<UpdateEmployeeLinkCommand, string>
{
    public async Task<string> Handle(UpdateEmployeeLinkCommand request, CancellationToken ct)
    {
        var found = await repository.GetPorIdAsync(request.Id, ct);
        if (found.IsFailure) throw new KeyNotFoundException("Funcionário não encontrado.");
        if (found.Success!.EstablishmentId != request.EstablishmentId)
            throw new InvalidOperationException("Funcionário não pertence ao estabelecimento informado.");

        var result = await repository.AlterarVinculoAsync(request.Id, request.IsActive, ct);
        if (result.IsFailure) throw new InvalidOperationException(result.Failure!.ToString());
        return result.Success!;
    }
}

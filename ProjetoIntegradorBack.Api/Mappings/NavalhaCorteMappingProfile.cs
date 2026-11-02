using ProjetoIntegradorBack.Domain.DTOs;
using ProjetoIntegradorBack.Domain.Entities;
using DomainService = ProjetoIntegradorBack.Domain.Entities.Service;
using ProjetoIntegradorBack.Domain.ValueObjects;
using Prime.Architecture.Mapper.Configuration;
namespace ProjetoIntegradorBack.Api.Mappings;
public class NavalhaCorteMappingProfile : MappingProfile
{
    public NavalhaCorteMappingProfile()
    {
        CreateMap<Address, AddressDto>(); CreateMap<AddressDto, Address>();
        CreateMap<Establishment, EstablishmentDto>(); CreateMap<Establishment, EstablishmentDetailsDto>();
        CreateMap<Employee, EmployeeDto>(); CreateMap<DomainService, ServiceDto>();
        CreateMap<Appointment, AppointmentDto>(); CreateMap<Contact, ContactDto>();
                CreateMap<ClientUser, UserDto>().ForMember(d => d.Name, o => o.MapFrom(s => s.Name)).ForMember(d => d.Type, o => o.MapFrom(s => s.UserType));
    }
}

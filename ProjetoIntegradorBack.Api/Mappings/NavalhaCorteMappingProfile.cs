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
        CreateMap<Appointment, AppointmentDto>()
                    .ForMember(d => d.NameClient, o => o.MapFrom(s => s.Cliente.Name))
                    .ForMember(d => d.NameEstablishment, o => o.MapFrom(s => s.Establishment.TradeName))
                    .ForMember(d => d.EstablishmentAddress, o => o.MapFrom(s => new AddressDto
                                {
                                    Street = s.Establishment.Address.Street,
                                    Number = s.Establishment.Address.Number,
                                    AdditionalInfo = s.Establishment.Address.AdditionalInfo,
                                    Neighborhood = s.Establishment.Address.Neighborhood,
                                    PostalCode = s.Establishment.Address.PostalCode,
                                    City = s.Establishment.Address.City,
                                    State = s.Establishment.Address.State,
                                    Latitude = s.Establishment.Address.Latitude,
                                    Longitude = s.Establishment.Address.Longitude
                                }))
                    .ForMember(d => d.EstablishmentPhone, o => o.MapFrom(s => s.Establishment.Phone))
                    .ForMember(d => d.EstablishmentEmail, o => o.MapFrom(s => s.Establishment.Email))
                    .ForMember(d => d.NameEmployee, o => o.MapFrom(s => s.Employee.Name))
                    .ForMember(d => d.NameService, o => o.MapFrom(s => s.Service.Name)); CreateMap<Contact, ContactDto>();
                CreateMap<ClientUser, UserDto>().ForMember(d => d.Name, o => o.MapFrom(s => s.Name)).ForMember(d => d.Type, o => o.MapFrom(s => s.UserType));
    }
}

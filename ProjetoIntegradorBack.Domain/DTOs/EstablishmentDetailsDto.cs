namespace ProjetoIntegradorBack.Domain.DTOs
{
    public class EstablishmentDetailsDto : EstablishmentDto
    {
        public List<ServiceDto> Services { get; set; } = [];
        public List<EmployeeDto> Employees { get; set; } = [];
    }
}
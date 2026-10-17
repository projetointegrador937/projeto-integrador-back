namespace ProjetoIntegradorBack.Api.Commands
{
    public class CreateServiceCommand
    {
        //public Guid BarberShopId { get; set; }
        public string Name { get; set; }        
        public int Duration { get; set; }
        public decimal Price { get; set; }
    }
}
namespace ProjetoIntegradorBack.Domain.Enums;

public enum UserType
{
    Cliente = 1,
    Barbearia = 3,
    Barbeiro = 2,
    Admin = 4,

    // Aliases mantidos para compatibilidade com o código legado.
    User = Cliente,
    Barber = Barbeiro,
    BarberShopOwner = Barbearia,
    Administrator = Admin
}

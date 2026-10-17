using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjetoIntegradorBack.Infrastructure.Context
{
    public class ProjetoIntegradorBackDbContext : DbContext
    {
        public ProjetoIntegradorBackDbContext(DbContextOptions<ProjetoIntegradorBackDbContext> options) : base(options) { }

        public DbSet<BarberShopService> Services { get; init; }
        public DbSet<Plan> Plans { get; init; }
        public DbSet<Customer> Customers { get; init; }
        public DbSet<Subscription> Subscriptions { get; init; }
        public DbSet<Barber> Barbers { get; init; }
        public DbSet<BarberShop> BarberShops { get; init; }
        public DbSet<ClientUser> Users { get; init; }
        public DbSet<Appointment> Appointments { get; init; }
        public DbSet<BarberSchedule> BarberSchedules { get; init; }
        public DbSet<BusinessHours> BusinessHours { get; init; }
        public DbSet<Estabelecimento> Estabelecimentos { get; init; }
        public DbSet<Funcionario> Funcionarios { get; init; }
        public DbSet<Servico> Servicos { get; init; }
        public DbSet<Agendamento> Agendamentos { get; init; }
        public DbSet<Contato> Contatos { get; init; }
        public DbSet<RefreshToken> RefreshTokens { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureClientUserEmail(modelBuilder.Entity<ClientUser>());
            modelBuilder.Entity<ClientUser>().HasIndex(x => x.Phone).IsUnique();
            ConfigureBarberShopEmail(modelBuilder.Entity<BarberShop>());
            modelBuilder.Entity<BarberShop>().HasIndex(x => x.Phone).IsUnique();

            modelBuilder.Entity<Appointment>(b => {
                b.Property(x => x.StartDateTime).HasColumnType("timestamp without time zone");
                b.Property(x => x.EndDateTime).HasColumnType("timestamp without time zone");
            });
            modelBuilder.Entity<Estabelecimento>(b => {
                b.HasIndex(x => x.Cnpj).IsUnique();
                b.OwnsOne(x => x.Endereco, e => {
                    e.Property(x => x.Rua).HasColumnName("Rua").HasMaxLength(200);
                    e.Property(x => x.Numero).HasColumnName("Numero").HasMaxLength(20);
                    e.Property(x => x.Complemento).HasColumnName("Complemento").HasMaxLength(100);
                    e.Property(x => x.Bairro).HasColumnName("Bairro").HasMaxLength(100);
                    e.Property(x => x.Cep).HasColumnName("Cep").HasMaxLength(8);
                    e.Property(x => x.Cidade).HasColumnName("Cidade").HasMaxLength(100);
                    e.Property(x => x.Estado).HasColumnName("Estado").HasMaxLength(2);
                    e.Property(x => x.Latitude).HasColumnName("Latitude");
                    e.Property(x => x.Longitude).HasColumnName("Longitude");
                });
                Timestamp(b, "CriadoEm"); Timestamp(b, "AtualizadoEm");
            });
            modelBuilder.Entity<Funcionario>(b => {
                b.HasOne(x => x.Estabelecimento).WithMany(x => x.Funcionarios).HasForeignKey(x => x.EstabelecimentoId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.UsuarioId).OnDelete(DeleteBehavior.Restrict);
                Timestamp(b, "CriadoEm");
            });
            modelBuilder.Entity<Servico>(b => b.HasOne(x => x.Estabelecimento).WithMany(x => x.Servicos).HasForeignKey(x => x.EstabelecimentoId).OnDelete(DeleteBehavior.Restrict));
            modelBuilder.Entity<Agendamento>(b => {
                b.HasOne(x => x.Cliente).WithMany().HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Estabelecimento).WithMany(x => x.Agendamentos).HasForeignKey(x => x.EstabelecimentoId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Funcionario).WithMany(x => x.Agendamentos).HasForeignKey(x => x.FuncionarioId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Servico).WithMany(x => x.Agendamentos).HasForeignKey(x => x.ServicoId).OnDelete(DeleteBehavior.Restrict);
                Timestamp(b, "DataHora"); Timestamp(b, "CriadoEm"); Timestamp(b, "AtualizadoEm");
            });
            Timestamp(modelBuilder.Entity<Contato>(), "CriadoEm");
            modelBuilder.Entity<RefreshToken>(b => {
                b.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.UsuarioId).OnDelete(DeleteBehavior.Cascade);
                b.HasIndex(x => x.Token).IsUnique();
                Timestamp(b, "ExpiraEm"); Timestamp(b, "CriadoEm");
            });
            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureClientUserEmail(EntityTypeBuilder<ClientUser> b)
        {
            b.OwnsOne(x => x.Email, e =>
            {
                e.Property(x => x.UserName).HasColumnName("UserName").HasMaxLength(100);
                e.Property(x => x.Domain).HasColumnName("Domain").HasMaxLength(100);
                e.HasIndex(x => new { x.UserName, x.Domain }).IsUnique();
            });
        }

        private static void ConfigureBarberShopEmail(EntityTypeBuilder<BarberShop> b)
        {
            b.OwnsOne(x => x.Email, e =>
            {
                e.Property(x => x.UserName).HasColumnName("UserName").HasMaxLength(100);
                e.Property(x => x.Domain).HasColumnName("Domain").HasMaxLength(100);
                e.HasIndex(x => new { x.UserName, x.Domain }).IsUnique();
            });
        }
        private static void Timestamp<TEntity>(EntityTypeBuilder<TEntity> b, string property) where TEntity : class => b.Property(property).HasColumnType("timestamp without time zone");
    }
}

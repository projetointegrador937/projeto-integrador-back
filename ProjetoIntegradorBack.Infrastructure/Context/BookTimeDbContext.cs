using ProjetoIntegradorBack.Domain.Entities;
using ProjetoIntegradorBack.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjetoIntegradorBack.Infrastructure.Context
{
    public class ProjetoIntegradorBackDbContext : DbContext
    {
        public ProjetoIntegradorBackDbContext(DbContextOptions<ProjetoIntegradorBackDbContext> options) : base(options) { }


        public DbSet<Plan> Plans { get; init; }
        public DbSet<Customer> Customers { get; init; }
        public DbSet<Subscription> Subscriptions { get; init; }

        public DbSet<ClientUser> Users { get; init; }

        public DbSet<Establishment> Establishments { get; init; }
        public DbSet<Employee> Employees { get; init; }
        public DbSet<Service> Services { get; init; }
        public DbSet<Appointment> Appointments { get; init; }
        public DbSet<Contact> Contacts { get; init; }
        public DbSet<RefreshToken> RefreshTokens { get; init; }
        public DbSet<AddressUser> AddresssUsers { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureClientUserEmail(modelBuilder.Entity<ClientUser>());
            modelBuilder.Entity<ClientUser>()
                .Property(x => x.UserType)
                .HasConversion<int>()
                .IsRequired();
            modelBuilder.Entity<ClientUser>()
                .ToTable("Users", table => table.HasCheckConstraint("CK_Users_UserType", "\"UserType\" IN (1, 2, 3, 4)"));
            modelBuilder.Entity<ClientUser>().HasIndex(x => x.Phone).IsUnique();
            modelBuilder.Entity<AddressUser>(b =>
            {
                b.ToTable("AddresssUsers");
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.UserId).IsUnique();
                b.Property(x => x.Street).HasMaxLength(200).IsRequired();
                b.Property(x => x.Number).HasMaxLength(20).IsRequired();
                b.Property(x => x.AdditionalInfo).HasMaxLength(100);
                b.Property(x => x.Neighborhood).HasMaxLength(100).IsRequired();
                b.Property(x => x.PostalCode).HasMaxLength(8).IsRequired();
                b.Property(x => x.City).HasMaxLength(100).IsRequired();
                b.Property(x => x.State).HasMaxLength(2).IsRequired();
                b.HasOne(x => x.User).WithOne(x => x.Address).HasForeignKey<AddressUser>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Establishment>(b => {
                b.HasIndex(x => x.Cnpj).IsUnique();
                b.OwnsOne(x => x.Address, e => {
                    e.Property(x => x.Street).HasColumnName("Street").HasMaxLength(200);
                    e.Property(x => x.Number).HasColumnName("Number").HasMaxLength(20);
                    e.Property(x => x.AdditionalInfo).HasColumnName("AdditionalInfo").HasMaxLength(100);
                    e.Property(x => x.Neighborhood).HasColumnName("Neighborhood").HasMaxLength(100);
                    e.Property(x => x.PostalCode).HasColumnName("PostalCode").HasMaxLength(8);
                    e.Property(x => x.City).HasColumnName("City").HasMaxLength(100);
                    e.Property(x => x.State).HasColumnName("State").HasMaxLength(2);
                    e.Property(x => x.Latitude).HasColumnName("Latitude");
                    e.Property(x => x.Longitude).HasColumnName("Longitude");
                });
                UtcTimestamp(b, "CreatedAt"); UtcTimestamp(b, "UpdatedAt");
            });
            modelBuilder.Entity<Employee>(b => {
                b.HasOne(x => x.Establishment).WithMany(x => x.Employees).HasForeignKey(x => x.EstablishmentId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
                UtcTimestamp(b, "CreatedAt");
            });
            modelBuilder.Entity<Service>(b => b.HasOne(x => x.Establishment).WithMany(x => x.Services).HasForeignKey(x => x.EstablishmentId).OnDelete(DeleteBehavior.Restrict));
            modelBuilder.Entity<Appointment>(b => {
                b.HasOne(x => x.Cliente).WithMany().HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Establishment).WithMany(x => x.Appointments).HasForeignKey(x => x.EstablishmentId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Employee).WithMany(x => x.Appointments).HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Service).WithMany(x => x.Appointments).HasForeignKey(x => x.ServiceId).OnDelete(DeleteBehavior.Restrict);
                UtcTimestamp(b, "DateTime"); UtcTimestamp(b, "CreatedAt"); UtcTimestamp(b, "UpdatedAt");
            });
            Timestamp(modelBuilder.Entity<Contact>(), "CreatedAt");
            modelBuilder.Entity<RefreshToken>(b => {
                b.HasOne(x => x.User).WithMany(x => x.RefreshTokens).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                b.HasIndex(x => x.Token).IsUnique();
                UtcTimestamp(b, "ExpiresAt"); UtcTimestamp(b, "CreatedAt");
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


        private static void Timestamp<TEntity>(EntityTypeBuilder<TEntity> b, string property) where TEntity : class => b.Property(property).HasColumnType("timestamp without time zone");

        private static void UtcTimestamp<TEntity>(EntityTypeBuilder<TEntity> b, string property) where TEntity : class => b.Property(property).HasColumnType("timestamp with time zone");
    }
}

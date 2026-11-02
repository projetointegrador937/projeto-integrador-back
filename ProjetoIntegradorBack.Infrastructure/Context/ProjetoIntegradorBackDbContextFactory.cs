using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DotNetEnv;

namespace ProjetoIntegradorBack.Infrastructure.Context;

public sealed class ProjetoIntegradorBackDbContextFactory : IDesignTimeDbContextFactory<ProjetoIntegradorBackDbContext>
{
    public ProjetoIntegradorBackDbContext CreateDbContext(string[] args)
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, ".env")))
        {
            directory = directory.Parent;
        }

        if (directory is not null)
        {
            Env.Load(Path.Combine(directory.FullName, ".env"));
        }

        var datebase = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "projeto_integrador";
        var username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
        var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")
            ?? throw new InvalidOperationException("POSTGRES_PASSWORD não foi configurada no ambiente ou no arquivo .env.");
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
            ?? $"Host=localhost;Port=5432;Database={datebase};Username={username};Password={password}";

        var optionsBuilder = new DbContextOptionsBuilder<ProjetoIntegradorBackDbContext>();
        optionsBuilder.UseNpgsql(connectionString, options =>
            options.MigrationsAssembly(typeof(ProjetoIntegradorBackDbContext).Assembly.FullName));

        return new ProjetoIntegradorBackDbContext(optionsBuilder.Options);
    }
}

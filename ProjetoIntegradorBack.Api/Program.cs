using ProjetoIntegradorBack.Api.Handlers;
using ProjetoIntegradorBack.Api.Handlers.NavalhaCorte;
using ProjetoIntegradorBack.Api.Mappings;
using ProjetoIntegradorBack.Domain.Interfaces;
using ProjetoIntegradorBack.Domain.Enums;
using ProjetoIntegradorBack.Infrastructure.Context;
using ProjetoIntegradorBack.Infrastructure.Repositories;

using DotNetEnv;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Prime.Architecture.Mapper.Interfaces;
using Prime.Architecture.Mapper.Configuration;
using Prime.Architecture.Mediator.Standard;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProjetoIntegradorBack.Api.Services;
using ProjetoIntegradorBack.Api.Middleware;

var envDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
while (envDirectory is not null && !File.Exists(Path.Combine(envDirectory.FullName, ".env")))
{
    envDirectory = envDirectory.Parent;
}

if (envDirectory is not null)
{
    Env.Load(Path.Combine(envDirectory.FullName, ".env"));
}

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    var datebase = Environment.GetEnvironmentVariable("POSTGRES_DB");
    var username = Environment.GetEnvironmentVariable("POSTGRES_USER");
    var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

    if (!string.IsNullOrWhiteSpace(datebase) &&
        !string.IsNullOrWhiteSpace(username) &&
        !string.IsNullOrWhiteSpace(password))
    {
        connectionString = $"Host=localhost;Port=5432;Database={datebase};Username={username};Password={password}";
    }
}

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Configure DATABASE_CONNECTION_STRING ou POSTGRES_DB, POSTGRES_USER e POSTGRES_PASSWORD no ambiente/.env.");
}
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "https://navalhaecorte.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key não configurada.");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Cliente", policy => policy.RequireRole(nameof(UserType.Cliente)));
    options.AddPolicy("Barbeiro", policy => policy.RequireRole(nameof(UserType.Barbeiro)));
    options.AddPolicy("Barbearia", policy => policy.RequireRole(nameof(UserType.Barbearia)));
    options.AddPolicy("Admin", policy => policy.RequireRole(nameof(UserType.Admin)));
        options.AddPolicy("BarbeiroOuBarbearia", policy => policy.RequireRole(nameof(UserType.Barbeiro), nameof(UserType.Barbearia)));
    options.AddPolicy("ClienteOuBarbeiroOuBarbeariaOuAdmin", policy =>
        policy.RequireRole(nameof(UserType.Cliente), nameof(UserType.Barbeiro), nameof(UserType.Barbearia), nameof(UserType.Admin)));
});
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddDbContext<ProjetoIntegradorBackDbContext>(options =>
    options.UseNpgsql(connectionString, npgsql =>
        npgsql.MigrationsAssembly(typeof(ProjetoIntegradorBackDbContext).Assembly.FullName)));
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();

builder.Services.AddScoped<UserHandler>();
builder.Services.AddScoped<UpdateUserAddressHandler>();
builder.Services.AddScoped<GetMyUserQueryHandler>();


builder.Services.AddMediatorStandard(typeof(Program).Assembly);
var mapperConfig = new MapperConfiguration();
mapperConfig.AddProfile<NavalhaCorteMappingProfile>();
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Projeto Integrador Back-end API",
        Version = "v1",
        Description = "API de agendamentos para estabelecimentos e profissionais."
    });
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProjetoIntegradorBackDbContext>();
    var startupLogger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
        .CreateLogger("DatabaseMigration");

    try
    {
        startupLogger.LogInformation("Verificando migrations pendentes do banco de dados.");
        dbContext.Database.Migrate();
        startupLogger.LogInformation("Migrations do banco de dados aplicadas com sucesso.");
    }
    catch (Exception exception)
    {
        startupLogger.LogCritical(exception,
            "Falha ao aplicar migrations do banco de dados. A API não será iniciada.");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapScalarApiReference(options => options
        .WithTitle("Projeto Integrador Back-end")
        .WithTheme(ScalarTheme.Purple)
        .WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json"));
}

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => Results.Redirect("/scalar"));
app.MapControllers();
app.Run();

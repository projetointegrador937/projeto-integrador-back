FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ProjetoIntegradorBack.Api/ProjetoIntegradorBack.Api.csproj", "ProjetoIntegradorBack.Api/"]
COPY ["ProjetoIntegradorBack.Domain/ProjetoIntegradorBack.Domain.csproj", "ProjetoIntegradorBack.Domain/"]
COPY ["ProjetoIntegradorBack.Infrastructure/ProjetoIntegradorBack.Infrastructure.csproj", "ProjetoIntegradorBack.Infrastructure/"]
COPY ["ProjetoIntegradorBack.Service/ProjetoIntegradorBack.Service.csproj", "ProjetoIntegradorBack.Service/"]
COPY ["ProjetoIntegradorBack.Shared/ProjetoIntegradorBack.Shared.csproj", "ProjetoIntegradorBack.Shared/"]
RUN dotnet restore "ProjetoIntegradorBack.Api/ProjetoIntegradorBack.Api.csproj"

COPY . .
RUN dotnet publish "ProjetoIntegradorBack.Api/ProjetoIntegradorBack.Api.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProjetoIntegradorBack.Api.dll"]

# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT=Development

COPY ["RentalMotorcycle/RentalMotorcycle.csproj", "RentalMotorcycle/"]
COPY ["RentalMotorcycle.Application/RentalMotorcycle.Application.csproj", "RentalMotorcycle.Application/"]
COPY ["RentalMotorcycle.Domain/RentalMotorcycle.Domain.csproj", "RentalMotorcycle.Domain/"]
COPY ["RentalMotorcycle.Infrastructure/RentalMotorcycle.Infrastructure.csproj", "RentalMotorcycle.Infrastructure/"]
RUN dotnet restore "RentalMotorcycle/RentalMotorcycle.csproj"

COPY . .
WORKDIR "/src/RentalMotorcycle"
RUN dotnet build "RentalMotorcycle.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet publish "RentalMotorcycle.csproj" -c Development -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalMotorcycle.dll"]

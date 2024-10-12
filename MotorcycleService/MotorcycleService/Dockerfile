# Use a imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /src

# Copie os arquivos .csproj e restaure as dependências
COPY ["MotorcycleService/MotorcycleService.csproj", "MotorcycleService/"]
COPY ["MotorcycleService.Application/MotorcycleService.Application.csproj", "MotorcycleService.Application/"]
COPY ["MotorcycleService.Domain/MotorcycleService.Domain.csproj", "MotorcycleService.Domain/"]
COPY ["MotorcycleService.Infrastructure/MotorcycleService.Infrastructure.csproj", "MotorcycleService.Infrastructure/"]
COPY ["MotorcycleService.Tests/MotorcycleService.Tests.csproj", "MotorcycleService.Tests/"]

RUN dotnet restore "MotorcycleService/MotorcycleService.csproj"

# Copie o restante dos arquivos e compile o projeto
COPY . .
WORKDIR "/src/MotorcycleService"
RUN dotnet build "MotorcycleService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MotorcycleService.csproj" -c Release -o /app/publish

# Use a imagem base do .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotorcycleService.dll"]
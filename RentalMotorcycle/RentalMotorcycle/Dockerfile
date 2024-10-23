FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT=Development

COPY ["RentalMotorcycle/RentalMotorcycle.csproj", "RentalMotorcycle/"]
COPY ["RentalMotorcycle.Application/RentalMotorcycle.Application.csproj", "RentalMotorcycle.Application/"]
COPY ["RentalMotorcycle.Domain/RentalMotorcycle.Domain.csproj", "RentalMotorcycle.Domain/"]
COPY ["RentalMotorcycle.Infrastructure/RentalMotorcycle.Infrastructure.csproj", "RentalMotorcycle.Infrastructure/"]
COPY ["RentalMotorcycle.Tests/RentalMotorcycle.Tests.csproj", "RentalMotorcycle.Tests/"]

RUN dotnet restore "RentalMotorcycle/RentalMotorcycle.csproj"

COPY . .
WORKDIR "/src/RentalMotorcycle"
RUN dotnet build "RentalMotorcycle.csproj" -c Development -o /app/build

FROM build AS testrunner
WORKDIR /src
RUN dotnet test "MotorcycleService.Tests/MotorcycleService.Tests.csproj" --no-build --verbosity normal

FROM build AS publish
RUN dotnet publish "RentalMotorcycle.csproj" -c Development -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "RentalMotorcycle.dll"]

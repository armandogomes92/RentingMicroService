FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT=Development

COPY ["MotorcycleService/MotorcycleService.csproj", "MotorcycleService/"]
COPY ["MotorcycleService.Application/MotorcycleService.Application.csproj", "MotorcycleService.Application/"]
COPY ["MotorcycleService.Domain/MotorcycleService.Domain.csproj", "MotorcycleService.Domain/"]
COPY ["MotorcycleService.Infrastructure/MotorcycleService.Infrastructure.csproj", "MotorcycleService.Infrastructure/"]
COPY ["MotorcycleService.Tests/MotorcycleService.Tests.csproj", "MotorcycleService.Tests/"]

RUN dotnet restore "MotorcycleService/MotorcycleService.csproj"

COPY . .
WORKDIR "/src/MotorcycleService"
RUN dotnet build "MotorcycleService.csproj" -c Development -o /app/build

FROM build AS testrunner
WORKDIR /src
RUN dotnet test "MotorcycleService.Tests/MotorcycleService.Tests.csproj" --no-build --verbosity normal

FROM build AS publish
RUN dotnet publish "MotorcycleService.csproj" -c Development -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "MotorcycleService.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT=Development

COPY ["DeliveryPilots/DeliveryPilots.csproj", "DeliveryPilots/"]
COPY ["DeliveryPilots.Application/DeliveryPilots.Application.csproj", "DeliveryPilots.Application/"]
COPY ["DeliveryPilots.Domain/DeliveryPilots.Domain.csproj", "DeliveryPilots.Domain/"]
COPY ["DeliveryPilots.Infrastructure/DeliveryPilots.Infrastructure.csproj", "DeliveryPilots.Infrastructure/"]
COPY ["DeliveryPilots.Tests/DeliveryPilots.Tests.csproj", "DeliveryPilots.Tests/"]

RUN dotnet restore "DeliveryPilots/DeliveryPilots.csproj"

COPY . .
WORKDIR "/src/DeliveryPilots"
RUN dotnet build "DeliveryPilots.csproj" -c Development -o /app/build

FROM build AS testrunner
WORKDIR /src
RUN dotnet test "DeliveryPilots.Tests/DeliveryPilots.Tests.csproj" --no-build --verbosity normal

FROM build AS publish
RUN dotnet publish "DeliveryPilots.csproj" -c Development -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "DeliveryPilots.dll"]

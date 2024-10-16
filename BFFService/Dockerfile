FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT=Development

COPY *.sln ./
COPY *.csproj ./

RUN dotnet restore

COPY . .
WORKDIR /src
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "BFFService.dll"]

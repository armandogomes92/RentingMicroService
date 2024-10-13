# Use a imagem base do SDK do .NET 8 para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie o arquivo de solução e os arquivos de projeto
COPY *.sln ./
COPY *.csproj ./

# Restaure as dependências do projeto
RUN dotnet restore

# Copie o restante dos arquivos e compile o projeto
COPY . .
WORKDIR /src
RUN dotnet publish -c Release -o /app/publish

# Use a imagem base do runtime do .NET 8 para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponha a porta que a aplicação irá rodar
EXPOSE 80

# Defina o comando de entrada para rodar a aplicação
ENTRYPOINT ["dotnet", "BFFService.dll"]

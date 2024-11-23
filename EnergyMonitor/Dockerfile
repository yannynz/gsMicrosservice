# Use a imagem do SDK .NET para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie os arquivos de projeto e restaure dependências
COPY ./EnergyMonitor.csproj ./
RUN dotnet restore

# Copie o restante do código e construa o projeto
COPY . ./
RUN dotnet publish -c Release -o out

# Use a imagem do Runtime .NET para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Exponha a porta do serviço
EXPOSE 5000

# Defina o comando para executar o aplicativo
ENTRYPOINT ["dotnet", "EnergyMonitor.dll"]


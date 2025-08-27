# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GitHubMonitor.API/GitHubMonitor.API.csproj", "GitHubMonitor.API/"]
COPY ["GitHubMonitor.Domain/GitHubMonitor.Domain.csproj", "GitHubMonitor.Domain/"]
COPY ["GitHubMonitor.Infra.Data/GitHubMonitor.Infra.Data.csproj", "GitHubMonitor.Infra.Data/"]
RUN dotnet restore "GitHubMonitor.API/GitHubMonitor.API.csproj"

COPY . .
WORKDIR "/src/GitHubMonitor.API"
RUN dotnet build "GitHubMonitor.API.csproj" -c Release -o /app/build

# Estágio de publicação
FROM build AS publish
RUN dotnet publish "GitHubMonitor.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GitHubMonitor.API.dll"]
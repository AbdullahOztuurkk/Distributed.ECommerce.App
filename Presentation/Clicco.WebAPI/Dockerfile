# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

# Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

# Copy csproj files for all projects
COPY ["Presentation/Clicco.WebAPI/Clicco.WebAPI.csproj", "Presentation/Clicco.WebAPI/"]
COPY ["Core/Application/Clicco.Application/Clicco.Application.csproj", "Core/Application/Clicco.Application/"]
COPY ["Core/Domain/Clicco.Domain.Model/Clicco.Domain.Model.csproj", "Core/Domain/Clicco.Domain.Model/"]
COPY ["Core/Domain/Clicco.Domain.Core/Clicco.Domain.Core.csproj", "Core/Domain/Clicco.Domain.Core/"]
COPY ["Core/Domain/Clicco.Domain.Shared/Clicco.Domain.Shared.csproj", "Core/Domain/Clicco.Domain.Shared/"]
COPY ["Infrastructure/Clicco.Infrastructure/Clicco.Infrastructure.csproj", "Infrastructure/Clicco.Infrastructure/"]
COPY ["Infrastructure/Clicco.Persistence/Clicco.Persistence.csproj", "Infrastructure/Clicco.Persistence/"]

# Restore all projects in a single command
RUN dotnet restore "Presentation/Clicco.WebAPI/Clicco.WebAPI.csproj"

# Copy the source code and build the application
COPY . .
WORKDIR "/src/Presentation/Clicco.WebAPI"
RUN dotnet build "Clicco.WebAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Clicco.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clicco.WebAPI.dll"]

# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

# Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

# Copy csproj files and restore as distinct layers
COPY ["Presentation/Services/Clicco.AuthAPI/Clicco.AuthServiceAPI.csproj", "Presentation/Services/Clicco.AuthAPI/"]
COPY ["Core/Domain/Clicco.Domain.Shared/Clicco.Domain.Shared.csproj", "Core/Domain/Clicco.Domain.Shared/"]
RUN dotnet restore "Presentation/Services/Clicco.AuthAPI/Clicco.AuthServiceAPI.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Presentation/Services/Clicco.AuthAPI"
RUN dotnet build "Clicco.AuthServiceAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Clicco.AuthServiceAPI.csproj" -c Release -o /app/publish 

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clicco.AuthServiceAPI.dll"]

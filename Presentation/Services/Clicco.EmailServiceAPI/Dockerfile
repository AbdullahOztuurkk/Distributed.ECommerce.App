FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
# Copy csproj files separately to fetch dependencies
COPY ["Presentation/Services/Clicco.EmailServiceAPI/Clicco.EmailServiceAPI.csproj", "Presentation/Services/Clicco.EmailServiceAPI/"]
COPY ["Core/Domain/Clicco.Domain.Shared/Clicco.Domain.Shared.csproj", "Core/Domain/Clicco.Domain.Shared/"]
# Restore the specific projects
RUN dotnet restore "Presentation/Services/Clicco.EmailServiceAPI/Clicco.EmailServiceAPI.csproj"

# Copy the source code and build the application
COPY . .
WORKDIR "/src/Presentation/Services/Clicco.EmailServiceAPI"
RUN dotnet build "Clicco.EmailServiceAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Clicco.EmailServiceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clicco.EmailServiceAPI.dll"]

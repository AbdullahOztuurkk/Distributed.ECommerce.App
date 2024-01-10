# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
# Copy csproj files separately to cache restore step
COPY ["Presentation/Services/Clicco.InvoiceServiceAPI/Clicco.InvoiceServiceAPI.csproj", "Presentation/Services/Clicco.InvoiceServiceAPI/"]
COPY ["Core/Domain/Clicco.Domain.Shared/Clicco.Domain.Shared.csproj", "Core/Domain/Clicco.Domain.Shared/"]
# Restore the specific project
RUN dotnet restore "Presentation/Services/Clicco.InvoiceServiceAPI/Clicco.InvoiceServiceAPI.csproj"

# Copy the source code and build the application
COPY . .
WORKDIR "/src/Presentation/Services/Clicco.InvoiceServiceAPI"
RUN dotnet build "Clicco.InvoiceServiceAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Clicco.InvoiceServiceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clicco.InvoiceServiceAPI.dll"]
